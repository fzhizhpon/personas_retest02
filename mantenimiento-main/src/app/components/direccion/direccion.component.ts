import {Component, EventEmitter, forwardRef, Output} from '@angular/core';
import {ComponenteBasePersona} from '../base/componente-base-persona';
import {direccion} from '../../models/ingreso-socios.model';
import {DireccionesService} from '../../services/direcciones.service';
import {finalize} from 'rxjs/operators';
import {concat, Observable, of} from 'rxjs';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {MessageService} from 'src/app/services/common/message/message.service';
import {ElementoFormulario} from 'src/app/models/elemento-formulario';
import {DireccionMinResponse, DireccionResponse} from '../../models/direccion';
import {AccionesFormulario} from 'src/app/enums/acciones-formulario.enum';
import {DomSanitizer} from '@angular/platform-browser';
import {BienInmuebleMinResponse} from '../../models/bien-inmueble';
import {BienesInmueblesService} from '../../services/bienes-inmuebles.service';
import {RegisteredComponent} from 'src/app/components/common/registered.component';
import {EdadService} from 'src/app/services/edad/edad.service';
import {NzModalService} from 'ng-zorro-antd/modal';
import {differenceInCalendarDays} from 'date-fns';
import {VsMapComponent} from 'src/app/components/ui/vs-map/vs-map.component';
import {Catalogo} from 'src/app/components/catalogos/catalogo';
import {CatalogoService} from 'src/app/services/api/catalogos/catalogo.service';
import {charToBool} from 'src/app/helpers/common.helper';
import {RelacionInstitucionService} from '../../services/relacion-institucion.service';

@Component({
	selector: 'pers-direccion',
	templateUrl: './direccion.component.html',
	styleUrls: ['./direccion.component.scss'],
	providers: [{
		provide: ComponenteBasePersona,
		useExisting: forwardRef(() => DireccionComponent)
	}]
})
export class DireccionComponent extends ComponenteBasePersona implements RegisteredComponent {

	readonly codigoComponente = 2;
	codigoPersona: number | null = null;

	@Output('codigoPersonaChange') emitterPersona: EventEmitter<number> = new EventEmitter<number>();

	formatterDollar = (value: number): string => `$ ${value ? value : ''}`;
	parserDollar = (value: string): string => value.replace('$ ', '');
	mostrarGuardar = true;
	tiposResidencias: Catalogo<number>[] = []

	cargando: {
		listar: boolean;
		listarCompleto: boolean;
		guardar: boolean;
	} = {
		listar: false,
		listarCompleto: false,
		guardar: false
	};

	nombreComponente = 'Direcciones';
	form: FormGroup = this.IniciarForm();
	_accion = 'Ingresar';
	indSeleccionado = -1;
	listaDirecciones: ElementoFormulario<any>[] = [];
	listaBienesInmuebles: ElementoFormulario<BienInmuebleMinResponse>[] = [];

	dateFormat = "dd/MM/yyyy";
	edadCalculada = "";

	disabledDate = (current: Date): boolean => differenceInCalendarDays(current, new Date()) > 0;

	constructor(
		private direccionesService: DireccionesService,
		private _messageService: MessageService,
		private _dom: DomSanitizer,
		private _calcularEdadService: EdadService,
		private _bienesInmueblesService: BienesInmueblesService,
		private _modalService: NzModalService,
		private _catalogoService: CatalogoService,
		private _relacionInst: RelacionInstitucionService
	) {
		super();
		this._catalogoService.obtenerPorGet<number>('TiposResidencias')
			.subscribe(api => this.tiposResidencias = api.resultado)
	}

	ObtenerTodo() {
		this.tieneCambios = false;

		if (this.codigoPersona != null) {

			this.listaDirecciones = [];
			this.LimpiarCampos();
			const ObtenerDirecciones = {
				codigoPersona: this.codigoPersona,
				paginacion: {
					indiceInicial: 0,
					numeroRegistros: 50
				}
			}

			this.cargando.listar = true;
			this.direccionesService.obtenerDirecciones(ObtenerDirecciones)
				.pipe(finalize(() => this.cargando.listar = false))
				.subscribe(
					data => {
						data.resultado.forEach(async (ref) => {
							const parroquia = await this.ObtenerParroquia({
								codigoPais: ref.codigoPais,
								codigoProvincia: ref.codigoProvincia,
								codigoCiudad: ref.codigoCiudad,
								codigoParroquia: ref.codigoParroquia
							});

							const data = {
								...ref,
								nombreParroquia: parroquia.resultado[0].parroquia
							};
							// this.nombreLugar = api.resultado[0].descripcionLugar
							this.listaDirecciones = [...this.listaDirecciones, {
								objeto: data,
								accion: AccionesFormulario.Leer
							}];
						})
						this.ResetearVista();
					},
					error => {
						this._messageService.showError(error.mensaje || error)
					}
				);

			const ObtenerBienesInmuebles = {
				codigoPersona: this.codigoPersona,
				paginacion: {
					indiceInicial: 0,
					numeroRegistros: 50
				}
			}
			this._bienesInmueblesService.obtenerBienesInmuebles(ObtenerBienesInmuebles)
				.pipe(finalize(() => this.cargando.listar = false))
				.subscribe(
					data => {
						data.resultado.forEach((ref) => {
							this.listaBienesInmuebles.push({objeto: ref, accion: AccionesFormulario.Leer});
						})
						this.ResetearVista();
					},
					error => {
						this._messageService.showError(error.mensaje || error)
					}
				);
		}
	}


	mostrarMapa(data: any) {
		if (data) {
			this.abrirModalVerMapa(data.objeto.longitud, data.objeto.latitud);
		}
	}


	async IngresarFila() {
		const formTelefonoFijo = this.form.get('telefonoFijo') as FormGroup;
		if (this.form.invalid && formTelefonoFijo!.invalid) {
			this._messageService.showError('Los campos marcados son obligatorios')
			Object.values(this.form.controls).forEach(control => {
				if (control.invalid) {
					control.markAsDirty();
					control.updateValueAndValidity({onlySelf: true});
				}
			});
			Object.values(formTelefonoFijo.controls).forEach(control => {
				if (control.invalid) {
					control.markAsDirty();
					control.updateValueAndValidity({onlySelf: true});
				}
			});

			return;
		}

		const direccion = this.form.getRawValue();

		if (this.listaDirecciones.length == 0) direccion.principal = true

		direccion.codigoPais = direccion.lugar.codigoPais;
		direccion.codigoProvincia = direccion.lugar.codigoProvincia;
		direccion.codigoCiudad = direccion.lugar.codigoCiudad;
		direccion.codigoParroquia = direccion.lugar.codigoParroquia;
		direccion.numeroTelFijo = direccion.telefonoFijo.numero;

		//  * obtenemos la informacion del lugar
		const parroquia = await this.ObtenerParroquia({
			codigoPais: direccion.codigoPais,
			codigoProvincia: direccion.codigoProvincia,
			codigoCiudad: direccion.codigoCiudad,
			codigoParroquia: direccion.codigoParroquia
		});

		const data2 = {
			...direccion,
			nombreParroquia: parroquia.resultado[0].parroquia
		};

		if (this.indSeleccionado == -1) {
			this.listaDirecciones.push({objeto: data2, accion: AccionesFormulario.Insertar});
		} else {
			this.listaDirecciones[this.indSeleccionado] = {
				objeto: data2,
				accion: AccionesFormulario.Actualizar
			};
		}
		this.actualizarTablaInterfaz();
		this.tieneCambios = true;
		this.ResetearVista();
	}

	EditarFila(elemento: ElementoFormulario<DireccionMinResponse>, index: number) {
		this.edadCalculada = this._calcularEdadService.CalcularEdad(elemento.objeto.fechaInicialResidencia);
		this._accion = 'Actualizar';

		if (elemento.accion == AccionesFormulario.Insertar) {

			this.form.patchValue(elemento.objeto);

			this.form.get('lugar')?.setValue({
				codigoPais: elemento.objeto.codigoPais,
				codigoProvincia: elemento.objeto.codigoProvincia,
				codigoCiudad: elemento.objeto.codigoCiudad,
				codigoParroquia: elemento.objeto.codigoParroquia,
			})
			this.listaDirecciones.splice(index, 1);
		} else {
			this.indSeleccionado = index;
			this.direccionesService.obtenerDireccion(this.codigoPersona ?? 0, elemento.objeto.numeroRegistro)
				.pipe(finalize(() => null))
				.subscribe(
					async (data) => {
						const parroquia = await this.ObtenerParroquia({
							codigoPais: data.resultado.direccion.codigoPais,
							codigoProvincia: data.resultado.direccion.codigoProvincia,
							codigoCiudad: data.resultado.direccion.codigoCiudad,
							codigoParroquia: data.resultado.direccion.codigoParroquia
						});
						data.resultado.direccion.telefonoFijo = data.resultado.telefonoFijo;
						const data2 = {
							...data.resultado.direccion,
							nombreParroquia: parroquia.resultado[0].parroquia
						};
						const dataFinal = charToBool(data2, ['principal', 'esMarginal']);
						this.listaDirecciones[index].objeto = <DireccionResponse>(dataFinal);
						this.listaDirecciones[index].objeto.numeroTelFijo = dataFinal.telefonoFijo.numero;
						this.form.patchValue(dataFinal);
						this.form.get('lugar')?.setValue({
							codigoPais: elemento.objeto.codigoPais,
							codigoProvincia: elemento.objeto.codigoProvincia,
							codigoCiudad: elemento.objeto.codigoCiudad,
							codigoParroquia: elemento.objeto.codigoParroquia,
						})
					},
					error => {
						this._messageService.showError(error.mensaje || error)
					}
				);
		}
	}

	EliminarFila(index: number) {
		this.ResetearVista();
		const referencia = this.listaDirecciones[index]

		if (referencia.accion == AccionesFormulario.Insertar) {
			this.listaDirecciones.splice(index, 1);
		} else {
			referencia.accion = AccionesFormulario.Eliminar;
		}
		this.actualizarTablaInterfaz();
		this.tieneCambios = true;
	}

	GuardarBase() {
		const message = this._messageService.showLoading(`Guardando información: DIRECCIONES - Codigo persona: ${this.codigoPersona}`)

		this.GuardarCambios()
			.pipe(finalize(() => this._messageService.removeLoading(message)))
			.subscribe()
	}

	GuardarCambios(): Observable<any> {
		if (!this.tieneCambios) return of(0);

		this.cargando.guardar = true;

		const operaciones: Observable<unknown>[] = [];

		this.listaDirecciones.forEach((elemento) => {
			// * recuperamos los elementos
			const direccion = elemento.objeto;

			if (this.codigoPersona) direccion.codigoPersona = parseInt(this.codigoPersona.toString());

			const {numeroRegistro, lugar, bienInmueble, ...resto} = direccion;

			switch (elemento.accion) {
				case AccionesFormulario.Insertar:
					const {telefonoFijo, ...resto2} = resto;
					const {numeroRegistro, codigoPersona, ...restoTelefono} = telefonoFijo;
					operaciones.push(this.direccionesService.guardarDirecciones(<direccion>(<unknown>({
						...resto2,
						telefonoFijo: {...restoTelefono}
					}))))
					break;

				case AccionesFormulario.Actualizar:
					// * en el actualizar necesitamos agregar la referencia al codigoPersona
					direccion.telefonoFijo.codigoPersona = parseInt(this.codigoPersona!.toString());
					operaciones.push(this.direccionesService.actualizaDirecciones(<direccion>(<unknown>(direccion))))
					break;

				case AccionesFormulario.Eliminar:
					operaciones.push(this.direccionesService.eliminaDirecciones(<direccion>(<unknown>(direccion))))
					break;
			}
		});

		return concat(...operaciones)
			.pipe(finalize(() => {
				this.cargando.guardar = false
				this.ObtenerTodo();
			}));
	}

	IniciarForm() {
		return new FormGroup({
			codigoPersona: new FormControl(this.codigoPersona),
			numeroRegistro: new FormControl(null),

			lugar: new FormControl(null, [Validators.required]),
			codigoPais: new FormControl(null, []),
			codigoProvincia: new FormControl(null, []),
			codigoCiudad: new FormControl(null, []),
			codigoParroquia: new FormControl(null, []),

			fechaInicialResidencia: new FormControl(null, [Validators.required]),

			valorArriendo: new FormControl(0, []),

			callePrincipal: new FormControl(null, [Validators.required]),
			calleSecundaria: new FormControl(null, [Validators.required]),
			numeroCasa: new FormControl(null, [Validators.required]),

			sector: new FormControl(null, [Validators.required]),
			esMarginal: new FormControl(false, []),
			codigoPostal: new FormControl(null, [Validators.required]),
			latitud: new FormControl(null, [Validators.required]),
			longitud: new FormControl(null, [Validators.required]),
			codigoTipoResidencia: new FormControl(null, []),
			principal: new FormControl(false, []),
			comunidad: new FormControl(null, []),
			referencia: new FormControl(null, [Validators.required]),
			tipoSector: new FormControl('U', [Validators.required]),

			codigoRegistroCivil: new FormControl(null, []),

			telefonoFijo: new FormGroup({
				numeroRegistro: new FormControl(),
				codigoPersona: new FormControl(this.codigoPersona),
				numero: new FormControl(null, [Validators.required, Validators.maxLength(20)]),
				codigoOperadora: new FormControl(null, [Validators.required]),
				observaciones: new FormControl(null)
			}),

			bienInmueble: new FormControl(null, []),

		});
	}

	desactivarCamposBienes() {
		this.form.get('lugar')?.disable();
		this.form.get('sector')?.disable()
		this.form.get('callePrincipal')?.disable()
		this.form.get('calleSecundaria')?.disable()
		this.form.get('numeroCasa')?.disable()
		this.form.get('codigoPostal')?.disable()
		this.form.get('tipoSector')?.disable()
		this.form.get('comunidad')?.disable()
		this.form.get('longitud')?.disable()
		this.form.get('latitud')?.disable()
		this.form.get('referencia')?.disable()
		this.form.get('esMarginal')?.disable()
	}

	habilitarCamposBienes() {
		this.form.get('lugar')?.enable();
		this.form.get('sector')?.enable()
		this.form.get('callePrincipal')?.enable()
		this.form.get('calleSecundaria')?.enable()
		this.form.get('numeroCasa')?.enable()
		this.form.get('codigoPostal')?.enable()
		this.form.get('tipoSector')?.enable()
		this.form.get('comunidad')?.enable()
		this.form.get('longitud')?.enable()
		this.form.get('latitud')?.enable()
		this.form.get('referencia')?.enable()
		this.form.get('esMarginal')?.enable()
	}

	LimpiarCampos() {
		this.form.reset();
		this.habilitarCamposBienes();
	}

	ResetearVista(): void {
		this.LimpiarCampos();
		this._accion = 'Insertar';
		this.indSeleccionado = -1;
	}

	tipoResidencia(codigo: any) {
		if (codigo) {
			if (codigo === 1 || codigo === 2) {
				this.ObtenerBienesInmuebles();
				this.form.patchValue({
					lugar: null,
					sector: null,
					callePrincipal: null,
					calleSecundaria: null,
					numeroCasa: null,
					codigoPostal: null,
					tipoSector: null,
					comunidad: null,
					longitud: null,
					latitud: null,
					referencia: null,
					esMarginal: null,
				});
				this.desactivarCamposBienes();
			} else {
				this.form.patchValue({
					lugar: null,
					sector: null,
					callePrincipal: null,
					calleSecundaria: null,
					numeroCasa: null,
					codigoPostal: null,
					tipoSector: null,
					comunidad: null,
					longitud: null,
					latitud: null,
					referencia: null,
					esMarginal: null,
				});
				this.habilitarCamposBienes();
			}
			this.form.updateValueAndValidity({onlySelf: true});
		}
	}

	async ObtenerBienesInmuebles() {
		if (this.codigoPersona != null) {
			const ObtenerDirecciones = {
				codigoPersona: this.codigoPersona,
				paginacion: {
					indiceInicial: 0,
					numeroRegistros: 50
				}
			}
			const bienesInmubeles = await this._bienesInmueblesService.obtenerBienesInmuebles(ObtenerDirecciones).toPromise();
			this.listaBienesInmuebles = [];
			bienesInmubeles.resultado.forEach(element => {
				this.listaBienesInmuebles.push({objeto: element, accion: AccionesFormulario.Leer});

			});
		}
	}

	calcularEdad(fecha: any, esTabla: boolean) {
		if (fecha) {
			const fechaConvertida = new Date(fecha);
			const anio = fechaConvertida.getFullYear();
			const mes = fechaConvertida.getMonth();
			const dia = fechaConvertida.getDate();
			if (esTabla) {
				return this._calcularEdadService.CalcularEdad(anio + "-" + mes + "-" + dia);
			} else {
				this.edadCalculada = this._calcularEdadService.CalcularEdad(anio + "-" + mes + "-" + dia);
			}
		}

	}

	async tipoBienInmueble(e: any) {

		if (this.codigoPersona !== null) {
			const bienInmueble = await this._bienesInmueblesService.obtenerBienInmueble(this.codigoPersona, e).toPromise();
			const datosFormulario = bienInmueble.resultado;
			const datosFormularioCompletos = {
				...datosFormulario,
				lugar: {
					codigoPais: datosFormulario.codigoPais,
					codigoProvincia: datosFormulario.codigoProvincia,
					codigoCiudad: datosFormulario.codigoCiudad,
					codigoParroquia: datosFormulario.codigoParroquia,
				},
				numeroCasa: datosFormulario.numero
			};

			this.form.patchValue(datosFormularioCompletos);

		}
	}

	isVisible = false;

	showModal(): void {
		this.isVisible = true;
	}

	handleOk1(): void {
		this.isVisible = false;
	}

	handleCancel(): void {
		this.isVisible = false;
	}

	bienInmuebleCapture(value: any) {
		this.form.get('lugar')?.enable();
		const datosFormularioCompletos = {
			...value,
			lugar: {
				codigoPais: value.codigoPais,
				codigoProvincia: value.codigoProvincia,
				codigoCiudad: value.codigoCiudad,
				codigoParroquia: value.codigoParroquia,
			},
			numeroCasa: value.numero
		};
		this.form.patchValue(datosFormularioCompletos);
		setTimeout(() => {
			this.form.get('lugar')?.disable();
		}, 1000)

	}

	abrirModalVerMapa(longitud: number, latitud: number) {
		this._modalService.create({
			nzContent: VsMapComponent,
			nzWidth: '50rem',
			nzTitle: 'Mapa',
			nzComponentParams: {
				longitud: longitud,
				latitud: latitud
			},
		});

	}

	ObtenerNombreTpoResidenciaPorCodigo(codigo: number) {
		return this.tiposResidencias.find(p => p.codigo === codigo)?.descripcion || codigo
	}


	ObtenerParroquia(lugar: any) {
		return this._catalogoService.obtenerLugares(lugar).toPromise();
	}


	// * metodo para actualizar tabla de ng-zorro

	actualizarTablaInterfaz() {
		this.listaDirecciones = [...this.listaDirecciones];
	}

	// ! fin metodo para actualizar tabla de ng-zorro


}
