import {Component, forwardRef} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {DomSanitizer} from '@angular/platform-browser';
import {concat, Observable, of} from 'rxjs';
import {finalize} from 'rxjs/operators';
import {AccionesFormulario} from 'src/app/enums/acciones-formulario.enum';
import {ElementoFormulario} from 'src/app/models/elemento-formulario';
import {MessageService} from 'src/app/services/common/message/message.service';
import {ComponenteBasePersona} from '../base/componente-base-persona';
import {bienInmueble} from '../../models/ingreso-socios.model';
import {BienInmuebleMinResponse, BienInmuebleResponse} from '../../models/bien-inmueble';
import {BienesInmueblesService} from '../../services/bienes-inmuebles.service';
import {EdadService} from 'src/app/services/edad/edad.service';
import {RegisteredComponent} from 'src/app/components/common/registered.component';
import {NzModalService} from 'ng-zorro-antd/modal';
import {VsMapComponent} from 'src/app/components/ui/vs-map/vs-map.component';
import {CatalogoService} from 'src/app/services/api/catalogos/catalogo.service';
import {differenceInCalendarDays} from 'date-fns';

@Component({
	selector: 'pers-bienes-inmuebles',
	templateUrl: './bienes-inmuebles.component.html',
	styleUrls: ['./bienes-inmuebles.component.scss'],
	providers: [{
		provide: ComponenteBasePersona,
		useExisting: forwardRef(() => BienesInmueblesComponent)
	}]
})

export class BienesInmueblesComponent extends ComponenteBasePersona implements RegisteredComponent {

	readonly codigoComponente = 13;
	nombreComponente = 'Bienes Inmuebles';
	form: FormGroup = this.IniciarForm();
	_accion = 'Ingresar';
	indSeleccionado = -1;
	listaBienesInmuebles: ElementoFormulario<BienInmuebleMinResponse>[] = [];

	dateFormat = "dd/MM/yyyy";
	edadCalculada = "";
	formatterDollar = (value: number): string => `$ ${value ? value : ''}`;
	parserDollar = (value: string): string => value.replace('$ ', '');

	disabledDate = (current: Date): boolean => differenceInCalendarDays(current, new Date()) > 0;


	constructor(
		private _bienesInmueblesService: BienesInmueblesService,
		private _messageService: MessageService,
		private _dom: DomSanitizer,
		private _calcularEdadService: EdadService,
		private _modalService: NzModalService,
		private _catalogoService: CatalogoService,
	) {
		super();
	}

	ObtenerTodo() {
		this.tieneCambios = false;

		if (this.codigoPersona != null) {
			this.listaBienesInmuebles = [];
			this.LimpiarCampos();
			const ObtenerBienesMuebles = {
				codigoPersona: this.codigoPersona,
				paginacion: {
					indiceInicial: 0,
					numeroRegistros: 50
				}
			}

			this.cargando.listar = true;
			this._bienesInmueblesService.obtenerBienesInmuebles(ObtenerBienesMuebles)
				.pipe(finalize(() => this.cargando.listar = false))
				.subscribe(
					data => {
						data.resultado.map(async (ref) => {
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
							this.listaBienesInmuebles.push({objeto: data, accion: AccionesFormulario.Leer});

							this.listaBienesInmuebles = [...this.listaBienesInmuebles]
						})

						this.ResetearVista();
						this.form.get('tipoBienInmueble')?.setValue(1);
					},
					error => {
						this._messageService.showError(error.mensaje || error)
					}
				);
		}
	}

	mostrarMapa(data: any) {
		if (data) {
			this._bienesInmueblesService.obtenerBienInmueble(this.codigoPersona ?? 0, data.objeto.numeroRegistro)
				.pipe(finalize(() => null))
				.subscribe(
					data => {
						this.abrirModalVerMapa(data.resultado.longitud, data.resultado.longitud);
					},
					error => {
						this._messageService.showError(error.mensaje || error)
					}
				);

		}
	}

	async IngresarFila() {
		if (this.form.invalid) {
			this.form.markAllAsTouched();
			this._messageService.showError('Los campos marcados son obligatorios')
			Object.values(this.form.controls).forEach(control => {
				if (control.invalid) {
					control.markAsDirty();
					control.updateValueAndValidity({onlySelf: true});
				}
			});
			return;
		}

		const direccion = this.form.getRawValue();
		direccion.codigoPais = direccion.lugar.codigoPais;
		direccion.codigoProvincia = direccion.lugar.codigoProvincia;
		direccion.codigoCiudad = direccion.lugar.codigoCiudad;
		direccion.codigoParroquia = direccion.lugar.codigoParroquia;
		direccion.esMarginal = direccion.esMarginal ? '1' : '0';

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
			this.listaBienesInmuebles.push({objeto: data2, accion: AccionesFormulario.Insertar});
		} else {
			this.listaBienesInmuebles[this.indSeleccionado] = {
				objeto: data2,
				accion: AccionesFormulario.Actualizar
			};
		}
		this.actualizarTablaInterfaz();
		this.tieneCambios = true;
		this.ResetearVista();
	}

	EditarFila(elemento: ElementoFormulario<BienInmuebleMinResponse>, index: number) {

		this.edadCalculada = this._calcularEdadService.CalcularEdad(elemento.objeto.fechaConstruccion);
		this._accion = 'Actualizar';
		if (elemento.accion == AccionesFormulario.Insertar) {
			this.form.patchValue(elemento.objeto);
			this.form.get('lugar')?.setValue({
				codigoPais: elemento.objeto.codigoPais,
				codigoProvincia: elemento.objeto.codigoProvincia,
				codigoCiudad: elemento.objeto.codigoCiudad,
				codigoParroquia: elemento.objeto.codigoParroquia,
			})
			this.listaBienesInmuebles.splice(index, 1);
		} else {
			this.indSeleccionado = index;
			this._bienesInmueblesService.obtenerBienInmueble(this.codigoPersona ?? 0, elemento.objeto.numeroRegistro)
				.pipe(finalize(() => null))
				.subscribe(
					async (data) => {

						const parroquia = await this.ObtenerParroquia({
							codigoPais: data.resultado.codigoPais,
							codigoProvincia: data.resultado.codigoProvincia,
							codigoCiudad: data.resultado.codigoCiudad,
							codigoParroquia: data.resultado.codigoParroquia
						});

						const data2 = {
							...data.resultado,
							nombreParroquia: parroquia.resultado[0].parroquia
						};

						this.listaBienesInmuebles[index].objeto = <BienInmuebleResponse>(data2);
						this.form.patchValue(data2);
						this.form.get('lugar')?.setValue({
							codigoPais: elemento.objeto.codigoPais,
							codigoProvincia: elemento.objeto.codigoProvincia,
							codigoCiudad: elemento.objeto.codigoCiudad,
							codigoParroquia: elemento.objeto.codigoParroquia,
						})
						this.form.get('lugar')?.disable();
						this.form.get('sector')?.disable();
						this.form.get('numero')?.disable();
						this.form.get('codigoPostal')?.disable();
						this.form.get('tipoSector')?.disable();
						this.form.get('latitud')?.disable();
						this.form.get('longitud')?.disable();
						this.form.get('esMarginal')?.disable();
						this.form.get('areaTerreno')?.disable();
					},
					error => {
						this._messageService.showError(error.mensaje || error)
					}
				);
		}
	}

	EliminarFila(index: number) {
		this.ResetearVista();
		const referencia = this.listaBienesInmuebles[index]

		if (referencia.accion == AccionesFormulario.Insertar) {
			this.listaBienesInmuebles.splice(index, 1);
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

		this.listaBienesInmuebles.forEach((elemento) => {
			const bienInmueble = elemento.objeto;
			console.log(bienInmueble);

			if (this.codigoPersona) bienInmueble.codigoPersona = parseInt(this.codigoPersona.toString());
			switch (elemento.accion) {
				case AccionesFormulario.Insertar:
					operaciones.push(this._bienesInmueblesService.guardarBienesInmuebles(<bienInmueble>(<unknown>(bienInmueble))))
					break;

				case AccionesFormulario.Actualizar:
					operaciones.push(this._bienesInmueblesService.actualizaBienesInmuebles(<bienInmueble>(<unknown>(bienInmueble))))
					break;

				case AccionesFormulario.Eliminar:
					const {codigoPersona, numeroRegistro} = bienInmueble;
					operaciones.push(this._bienesInmueblesService.eliminaBienesInmuebles(<bienInmueble>(<unknown>({
						codigoPersona,
						numeroRegistro
					}))))
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
			numero: new FormControl(null, [Validators.required, Validators.maxLength(10)]),
			sector: new FormControl(null, [Validators.required, Validators.maxLength(50)]),
			codigoPostal: new FormControl(null, [Validators.required, Validators.maxLength(10)]),
			latitud: new FormControl(null, [Validators.required]),
			longitud: new FormControl(null, [Validators.required]),
			estado: new FormControl(null),
			comunidad: new FormControl(null, [Validators.maxLength(100)]),
			referencia: new FormControl(null, [Validators.maxLength(200)]),
			tipoSector: new FormControl('U', [Validators.required]),
			avaluoComercial: new FormControl(null, [Validators.required]),
			avaluoCatastral: new FormControl(null, [Validators.required]),
			areaTerreno: new FormControl(null, [Validators.required]),
			areaConstruccion: new FormControl(null, [Validators.required]),
			valorTerrenoMetrosCuadrados: new FormControl(null, [Validators.required]),
			descripcion: new FormControl(null, [Validators.maxLength(100)]),
			lugar: new FormControl(null, [Validators.required]),
			codigoPais: new FormControl(null),
			codigoProvincia: new FormControl(null),
			codigoCiudad: new FormControl(null),
			codigoParroquia: new FormControl(null),
			fechaConstruccion: new FormControl(null, [Validators.required]),
			callePrincipal: new FormControl(null, [Validators.required, Validators.maxLength(200)]),
			calleSecundaria: new FormControl(null, [Validators.required, Validators.maxLength(200)]),
			esMarginal: new FormControl(false),
			codigoTipoResidencia: new FormControl(null),
			tipoBienInmueble: new FormControl(null, [Validators.required]),
		});
	}

	LimpiarCampos() {
		this.form.reset();
		// * eliminamos la obligatoriedad de comunidad en el caso de existir
		this.form.controls['comunidad'].removeValidators([Validators.required]);
		this.form.controls['comunidad'].updateValueAndValidity();
	}

	ResetearVista(): void {
		this.LimpiarCampos();
		this._accion = 'Insertar';
		this.indSeleccionado = -1;
		this.form.get('lugar')?.enable();
		this.form.get('sector')?.enable();
		this.form.get('numero')?.enable();
		this.form.get('codigoPostal')?.enable();
		this.form.get('tipoSector')?.enable();
		this.form.get('latitud')?.enable();
		this.form.get('longitud')?.enable();
		this.form.get('esMarginal')?.enable();
		this.form.get('areaTerreno')?.enable();
	}

	calcularTiempoConstruccion(date: Date) {

		if (date) {
			const anio = new Date(date).getFullYear();
			const mes = new Date(date).getMonth() + 1;
			const dia = new Date(date).getDate();
			this.edadCalculada = this._calcularEdadService.CalcularEdad(anio + "-" + mes + "-" + dia);
		}

	}

	abrirModalVerMapa(longitud: number, latitud: number) {
		const modal = this._modalService.create({
			nzContent: VsMapComponent,
			nzWidth: '140rem',
			nzTitle: 'Mapa',
			nzComponentParams: {
				longitud: longitud,
				latitud: latitud
			},
		});
	}

	ObtenerParroquia(lugar: any) {
		return this._catalogoService.obtenerLugares(lugar).toPromise();
	}

	async GuardarNuevoRegistro() {
		await this.IngresarFila();

		await this.GuardarCambios().toPromise()
	}


	// * props tipo de sector

	cambioTipoSector(event: any) {
		if (event === 'R') {
			// * vamos a colocar como obligatorio a la comunidad
			this.form.controls['comunidad'].addValidators([Validators.required]);
			// * actualizamos valor y validadores
			this.form.controls['comunidad'].updateValueAndValidity();
		} else {
			// * vamos a eliminar la validacion de obligatorio a comunidad
			this.form.get('comunidad')?.setValue(null);
			this.form.controls['comunidad'].removeValidators([Validators.required]);
			this.form.controls['comunidad'].updateValueAndValidity();
		}
	}

	// ! fin metodos tipo de sector


	// * metodo para actualizar tabla de ng-zorro

	actualizarTablaInterfaz() {
		this.listaBienesInmuebles = [...this.listaBienesInmuebles];
	}

	// ! fin metodo para actualizar tabla de ng-zorro

}
