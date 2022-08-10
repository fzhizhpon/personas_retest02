import {Component, forwardRef} from '@angular/core';
import {ComponenteBasePersona} from '../base/componente-base-persona';
import {finalize} from 'rxjs/operators';
import {concat, Observable, of} from 'rxjs';
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {trabajo} from '../../models/ingreso-socios.model';
import {MessageService} from 'src/app/services/common/message/message.service';
import {TrabajosService} from '../../services/trabajos.service';
import {ValidacionTelefonosService} from "../../services/validacion-telefonos.service";
import {EdadService} from 'src/app/services/edad/edad.service';
import {AccionesFormulario} from 'src/app/enums/acciones-formulario.enum';
import {ElementoFormulario} from 'src/app/models/elemento-formulario';
import {RegisteredComponent} from 'src/app/components/common/registered.component';
import {boolToChar, charToBool} from 'src/app/helpers/common.helper';
import {differenceInCalendarDays} from 'date-fns';

@Component({
	selector: 'pers-trabajos',
	templateUrl: './trabajos.component.html',
	styleUrls: [
		'./trabajos.component.scss',
		'../../../../../../src/app/components/ui/table/table.component.scss'],
	providers: [{
		provide: ComponenteBasePersona,
		useExisting: forwardRef(() => TrabajosComponent)
	}]
})
export class TrabajosComponent extends ComponenteBasePersona implements RegisteredComponent {

	readonly codigoComponente = 10;

	nombreComponente = 'Trabajos';

	form: FormGroup = this.IniciarForm();
	_accion = 'Insertar';
	indSeleccionado = -1;

	listaTrabajo: ElementoFormulario<trabajo>[] = [];
	dateFormat = "dd/MM/yyyy";

	tiempoTrabajo = "";

	formatterDollar = (value: number): string => `$ ${value ? value : ''}`;
	parserDollar = (value: string): string => value.replace('$ ', '');

	disabledDate = (current: Date): boolean => differenceInCalendarDays(current, new Date()) > 0 || differenceInCalendarDays(current, new Date()) <-37380;


	constructor(
		private trabajoService: TrabajosService,
		private _messageService: MessageService,
		private _formBuilder: FormBuilder,
		private _validacionTelefonosService: ValidacionTelefonosService,
		private _calcularEdadService: EdadService,
	) {
		super();
	}

	ObtenerTodo() {
		this.tieneCambios = false;

		if (this.codigoPersona != null) {
			this.listaTrabajo = [];
			this.LimpiarCampos();
			const ObtenerTrabajos = {
				codigoPersona: this.codigoPersona,
				paginacion: {
					indiceInicial: 0,
					numeroRegistros: 50
				}
			}
			this.cargando.listar = true
			this.trabajoService.obtenerTrabajos(ObtenerTrabajos)
				.pipe(finalize(() => this.cargando.listar = false))
				.subscribe(
					data => {
						data.resultado.map((ref) => {
							this.listaTrabajo.push({
								objeto: ref,
								accion: AccionesFormulario.Leer
							});
						});
						this.ResetearVista();
					}, (error) => {
						this._messageService.showError(error.mensaje || error);
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

			return
		}

		const obj = this.form.getRawValue();

		if (this.listaTrabajo.length == 0) obj.principal = true

		obj.codigoPersona = this.codigoPersona;
		obj.principal = obj.principal ? '1' : '0';
		obj.codigoPais = obj.lugar.codigoPais;
		obj.codigoProvincia = obj.lugar.codigoProvincia;
		obj.codigoCiudad = obj.lugar.codigoCiudad;
		obj.codigoParroquia = obj.lugar.codigoParroquia;
		obj.ingresosMensuales = parseFloat(obj.ingresosMensuales.toString());

		if (this._accion == 'Insertar') {
			this.listaTrabajo.push({objeto: charToBool(obj, ['principal']), accion: AccionesFormulario.Insertar});
		} else if (this._accion == 'Actualizar') {
			this.listaTrabajo[this.indSeleccionado] = {
				objeto: charToBool(obj, ['principal']),
				accion: AccionesFormulario.Actualizar
			};
		}

		this.tieneCambios = true;
		this.ResetearVista();
	}


	EditarFila(data: ElementoFormulario<trabajo>, index: number) {
		this._accion = 'Actualizar';
		if (data.accion == AccionesFormulario.Insertar) {
			this.form.patchValue(data.objeto);
			this.listaTrabajo.splice(index, 1);
		} else {
			this.indSeleccionado = index;
			this.trabajoService.obtenerTrabajo(data.objeto)
				.pipe(finalize(() => null))
				.subscribe(
					data => {
						this.listaTrabajo[index].objeto = data.resultado;
						this.form.patchValue(data.resultado);
						this.form.get('lugar')?.setValue({
							codigoPais: data.resultado.codigoPais,
							codigoProvincia: data.resultado.codigoProvincia,
							codigoCiudad: data.resultado.codigoCiudad,
							codigoParroquia: data.resultado.codigoParroquia,
						})
					}, (error) => {
						this._messageService.showError(error.mensaje || error);
					}
				);
		}
	}

	EliminarFila(index: number) {
		this.ResetearVista();

		const referencia = this.listaTrabajo[index]

		if (referencia.accion == AccionesFormulario.Insertar) {
			this.listaTrabajo.splice(index, 1);
		} else {
			referencia.accion = AccionesFormulario.Eliminar;
		}

		this.tieneCambios = true;
	}

	GuardarBase() {

		const message = this._messageService.showLoading(`Guardando información: DIRECCIONES - Codigo persona: ${this.codigoPersona}`)

		this.GuardarCambios()
			.pipe(finalize(() => this._messageService.removeLoading(message)))
			.subscribe((resp) => {
				this._messageService.showSuccess(resp.mensaje);
			}, (err) => {
				this._messageService.showError(err.mensaje || err);
			});
	}

	GuardarCambios(): Observable<any> {
		if (!this.tieneCambios) return of(0);

		this.cargando.guardar = true;

		const operaciones: Observable<unknown>[] = [];

		this.listaTrabajo.forEach((v) => {
			const referencia = v.objeto;
			if (this.codigoPersona) referencia.codigoPersona = parseInt(this.codigoPersona.toString());

			switch (v.accion) {
				case AccionesFormulario.Insertar:
					operaciones.push(this.trabajoService.guardarTrabajo(referencia))
					break;

				case AccionesFormulario.Actualizar:
					operaciones.push(this.trabajoService.actualizaTrabajo(referencia))
					break;

				case AccionesFormulario.Eliminar:
					operaciones.push(this.trabajoService.eliminaTrabajo(referencia))
					break;
			}
		});

		return concat(...operaciones)
			.pipe(finalize(() => {
				this.cargando.guardar = false;
				this.listaTrabajo = [];
				this.ObtenerTodo();
			}))
	}

	IniciarForm() {
		return this._formBuilder.group({
				codigoTrabajo: new FormControl(null),
				codigoCategoria: new FormControl(null, [Validators.required]),
				codigoActividad: new FormControl(null, [Validators.required]),
				razonSocial: new FormControl(null, [Validators.required]),
				fechaIngreso: new FormControl(null, [Validators.required]),
				lugar: new FormControl(null, [Validators.required]),
				codigoPais: new FormControl(null, []),
				codigoProvincia: new FormControl(null, []),
				codigoCiudad: new FormControl(null, []),
				codigoParroquia: new FormControl(null, []),
				direccion: new FormControl(null, [Validators.required]),
				cargo: new FormControl(null, [Validators.required]),
				ingresosMensuales: new FormControl(null, [Validators.required]),
				principal: new FormControl(null),
				// telefonos: this._formBuilder.array([])
			},
			// {
			// 	validators: this._validacionTelefonosService.arrayIgual('telefonos')
			// }
		);
	}

	LimpiarCampos() {
		this.form.reset();
	}

	//  * LOGICA PARA LOS TELEFONOS

	get obtenerTelefonos() {
		return this.form.get('telefonos') as FormArray;
	}

	agregarTelefono() {
		this.obtenerTelefonos.push(this._formBuilder.control(''))
	}

	borrarTelefono(i: number) {
		this.obtenerTelefonos.removeAt(i);
	}

	//  ! FIN LOGICA PARA LOS TELEFONOS

	ResetearVista(): void {
		this.LimpiarCampos();
		this._accion = 'Insertar';
		this.indSeleccionado = -1;
	}

	calcularTiempoTrabajo(fecha: any, esTabla: boolean) {
		if (fecha) {
			const fechaConvertida = new Date(fecha);
			const anio = fechaConvertida.getFullYear();
			const mes = fechaConvertida.getMonth();
			const dia = fechaConvertida.getDate();
			if (esTabla) {
				// tabla
				return this._calcularEdadService.CalcularEdad(anio + "-" + mes + "-" + dia);
			} else {
				this.tiempoTrabajo = this._calcularEdadService.CalcularEdad(anio + "-" + mes + "-" + dia);
			}
		}
	}
}
