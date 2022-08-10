import {Component, forwardRef} from '@angular/core';
import {ComponenteBasePersona} from '../base/componente-base-persona';
import {FormGroup} from '@angular/forms';
import {Observable, concat, of} from 'rxjs';
import {finalize} from 'rxjs/operators';
import {AccionesFormulario} from 'src/app/enums/acciones-formulario.enum';
import {ElementoFormulario} from 'src/app/models/elemento-formulario';
import {MessageService} from 'src/app/services/common/message/message.service';
import {
	BienMuebleMinResponse,
	BienMuebleResponse,
	EliminarBienMuebleRequest,
	InsertarBienMueble
} from '../../models/bien-mueble';
import {BienesMueblesService} from '../../services/bienes-muebles.service';
import {bienMueble} from '../../models/ingreso-socios.model';
import {FormControl, Validators} from '@angular/forms';
import {RegisteredComponent} from 'src/app/components/common/registered.component';
import {SriService} from '../../services/sri';
import {Catalogo} from 'src/app/components/catalogos/catalogo';
import {CatalogoService} from 'src/app/services/api/catalogos/catalogo.service';

@Component({
	selector: 'pers-bienes-muebles',
	templateUrl: './bienes-muebles.component.html',
	styleUrls: ['./bienes-muebles.component.scss'],
	providers: [{
		provide: ComponenteBasePersona,
		useExisting: forwardRef(() => BienesMueblesComponent)
	}]
})
export class BienesMueblesComponent extends ComponenteBasePersona implements RegisteredComponent {

	readonly codigoComponente = 14;

	nombreComponente = 'Bienes Muebles';
	form: FormGroup = this.IniciarForm();
	_accion = 'Ingresar';
	indSeleccionado = -1;
	listaBienesMuebles: ElementoFormulario<BienMuebleMinResponse>[] = [];
	formatterDollar = (value: number): string => `$ ${value ? value : ''}`;
	parserDollar = (value: string): string => value.replace('$ ', '');

	tiposBienesMuebles: Catalogo<number>[] = []

	constructor(
		private _bienesMueblesService: BienesMueblesService,
		private sriService: SriService,
		private _messageService: MessageService,
		private _catalogoService: CatalogoService,
	) {
		super();
		this._catalogoService.obtenerPorGet<number>('TiposBienesMuebles')
			.subscribe(api => this.tiposBienesMuebles = api.resultado)
	}

	ObtenerTodo() {
		this.tieneCambios = false;

		if (this.codigoPersona !== null) {
			this.listaBienesMuebles = [];
			this.LimpiarCampos();

			this.cargando.listar = true;

			this._bienesMueblesService.obtenerBienesMuebles(this.codigoPersona)
				.pipe(finalize(() => this.cargando.listar = false))
				.subscribe(
					data => {
						data.resultado.forEach((ref) =>
							this.listaBienesMuebles.push({objeto: ref, accion: AccionesFormulario.Leer}))

						this.listaBienesMuebles = [...this.listaBienesMuebles]
						this.ResetearVista();
					},
					error => {
						this._messageService.showError(error.mensaje || error)
					}
				);
		}
	}

	IngresarFila() {
		if (this.form.invalid) {
			this._messageService.showError('Los campos marcados son obligatorios')
			Object.values(this.form.controls).forEach(control => {
				if (control.invalid) {
					control.markAsDirty();
					control.updateValueAndValidity({onlySelf: true});
				}
			});
			return;
		}

		const bienMueble = this.form.getRawValue();

		if (this.indSeleccionado == -1) {
			this.listaBienesMuebles.push({objeto: bienMueble, accion: AccionesFormulario.Insertar});
		} else {
			this.listaBienesMuebles[this.indSeleccionado] = {
				objeto: bienMueble,
				accion: AccionesFormulario.Actualizar
			};
		}
		this.actualizarTablaInterfaz();
		this.tieneCambios = true;
		this.ResetearVista();
	}

	EditarFila(elemento: ElementoFormulario<BienMuebleMinResponse>, index: number) {
		this._accion = 'Actualizar';
		if (elemento.accion == AccionesFormulario.Insertar) {
			this.form.patchValue(elemento.objeto);
			this.listaBienesMuebles.splice(index, 1);
		} else {
			this.indSeleccionado = index;
			this._bienesMueblesService.obtenerBienMueble(this.codigoPersona ?? 0, elemento.objeto.numeroRegistro)
				.pipe(finalize(() => null))
				.subscribe(
					data => {

						this.listaBienesMuebles[index].objeto = <BienMuebleResponse>(data.resultado);
						this.form.patchValue(data.resultado);
					},
					error => {
						this._messageService.showError(error.mensaje || error)
					}
				);
		}
	}

	EliminarFila(index: number) {
		this.ResetearVista();

		const referencia = this.listaBienesMuebles[index]

		if (referencia.accion == AccionesFormulario.Insertar) {
			this.listaBienesMuebles.splice(index, 1);
		} else {
			referencia.accion = AccionesFormulario.Eliminar;
		}
		this.actualizarTablaInterfaz();
		this.tieneCambios = true;
	}

	GuardarBase() {
		const message = this._messageService.showLoading(`Guardando información: Bienes Muebles - Codigo persona: ${this.codigoPersona}`)
		this.GuardarCambios()
			.pipe(finalize(() => this._messageService.removeLoading(message)))
			.subscribe();
	}

	GuardarCambios(): Observable<unknown> {
		if (!this.tieneCambios) return of(0);

		this.cargando.guardar = true;
		const operaciones: Observable<unknown>[] = [];
		this.listaBienesMuebles.forEach((elemento) => {
			const bienMueble = elemento.objeto;
			if (this.codigoPersona) bienMueble.codigoPersona = parseInt(this.codigoPersona.toString());
			switch (elemento.accion) {
				case AccionesFormulario.Insertar:
					const {numeroRegistro, ...resto} = bienMueble;
					operaciones.push(this._bienesMueblesService.guardarBienesMuebles(<InsertarBienMueble>(resto)))
					break;
				case AccionesFormulario.Actualizar:
					operaciones.push(this._bienesMueblesService.actualizaBienesMuebles(<bienMueble>(<unknown>(bienMueble))))
					break;
				case AccionesFormulario.Eliminar:
					operaciones.push(this._bienesMueblesService.eliminaBienesMuebles(<EliminarBienMuebleRequest>(bienMueble)));
					break;
			}
		});

		return concat(...operaciones)
			.pipe(finalize(() => {
				this.cargando.guardar = false;
				this.ObtenerTodo();
			}));
	}

	IniciarForm() {
		return new FormGroup({
			codigoPersona: new FormControl(this.codigoPersona),
			numeroRegistro: new FormControl(null),
			tipoBienMueble: new FormControl(null, [Validators.required]),
			codigoReferencia: new FormControl(null, [Validators.required, Validators.maxLength(20)]),
			descripcion: new FormControl(null, [Validators.required, Validators.maxLength(200)]),
			avaluoComercial: new FormControl(null, [Validators.required]),
		});
	}

	LimpiarCampos() {
		this.form.reset();
	}

	setearLista() {
		this.listaBienesMuebles = [];
	}

	ResetearVista(): void {
		this.LimpiarCampos();
		this._accion = 'Insertar';
		this.indSeleccionado = -1;
	}


	setearCodigoReferencia() {
		if (this.form.get('codigoReferencia')?.value !== null) {
			this.form.get('codigoReferencia')?.setValue('');
			this.form.get('descripcion')?.setValue('');
			this.form.get('avaluoComercial')?.setValue('');
		}
	}


	validaPlaca(event: any) {
		const palabra = event;
		const bienMueble = this.form.getRawValue();

		if (palabra !== null && palabra.length === 7 && bienMueble.tipoBienMueble === 1) {
			if (bienMueble.tipoBienMueble == 1 && bienMueble.codigoReferencia.match('^\\D{3}\\d{4}$')) {
				this.ObtenerInformacionPlacaSri(bienMueble.codigoReferencia);
			} else {
				this._messageService.showError('La placa: ' + bienMueble.codigoReferencia + ' es incorrecta.');
			}
		} else {
			this.form.get('descripcion')?.setValue('');
		}
	}


	ObtenerInformacionPlacaSri(placa: String) {
		this.sriService.obtenerInformacionPlaca(placa.toUpperCase())
			.subscribe(data => {
				if (data.resultado.numeroPlaca !== null) {
					let detalle = "Bin: " + data.resultado.numeroCamvCpn +
						", Color: " + data.resultado.colorVehiculo1 +
						", Cilindraje" + data.resultado.cilindraje +
						", Tipo: " + data.resultado.nombreClase +
						", Marca: " + data.resultado.descripcionMarca +
						", Modelo: " + data.resultado.descripcionModelo +
						", Año: " + data.resultado.anioAuto +
						", Ultimo año pagado: " + data.resultado.ultimoAnioPagado;

					this.form.get('descripcion')?.setValue(detalle);
				}
			});
	}

	ObtenerNombreMueblePorCodigo(codigo: number) {
		return this.tiposBienesMuebles.find(p => p.codigo === codigo)?.descripcion || codigo
	}

	// * metodo para actualizar tabla de ng-zorro

	actualizarTablaInterfaz() {
		this.listaBienesMuebles = [...this.listaBienesMuebles];
	}

	// ! fin metodo para actualizar tabla de ng-zorro
}
