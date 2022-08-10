import {Component, forwardRef, OnInit} from '@angular/core';
import {ComponenteBasePersona} from '../base/componente-base-persona';
import {ElementoFormulario} from 'src/app/models/elemento-formulario';
import {finalize} from 'rxjs/operators';
import {concat, Observable, of} from 'rxjs';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {referenciaPersonal} from '../../models/ingreso-socios.model';
import {MessageService} from 'src/app/services/common/message/message.service';
import {ModalBuscarPersonaService} from '../modal-buscar-persona/modal-buscar-persona.service';
import {PersonaService} from '../../services/persona.service';
import {ReferenciaPersonalService} from '../../services/referencia-personal.service';
import {AccionesFormulario} from 'src/app/enums/acciones-formulario.enum';
import {RegisteredComponent} from 'src/app/components/common/registered.component';
import {EdadService} from 'src/app/services/edad/edad.service';
import {DatosGeneralesComponent} from "../datos-generales/datos-generales.component";
import {NzModalService} from 'ng-zorro-antd/modal';
import {environmentHost} from 'src/environments/environment';
import {differenceInCalendarDays} from 'date-fns';

@Component({
	selector: 'pers-referencia-personal',
	templateUrl: './referencia-personal.component.html',
	styleUrls: ['./referencia-personal.component.scss'],
	providers: [{
		provide: ComponenteBasePersona,
		useExisting: forwardRef(() => ReferenciaPersonalComponent)
	}]
})
export class ReferenciaPersonalComponent extends ComponenteBasePersona implements OnInit, RegisteredComponent {

	readonly codigoComponente = 3;

	nombreComponente = 'Referencias Personales';
	form: FormGroup = this.IniciarForm();
	_accion = 'Insertar';

	indSeleccionado = -1;

	listaReferenciaPersonal: ElementoFormulario<referenciaPersonal>[] = [];

	dateFormat = "dd/MM/yyyy";

	tiempoRelacion = "";

	disabledDate = (current: Date): boolean => differenceInCalendarDays(current, new Date()) > 0 || differenceInCalendarDays(current, new Date()) <-37380;

	constructor(
		private _referenciaPersonalService: ReferenciaPersonalService,
		private _personaService: PersonaService,
		private _modalService: ModalBuscarPersonaService,
		private _messageService: MessageService,
		private _calcularEdadService: EdadService,
		private _modalServiceVisualizar: NzModalService,
	) {
		super();
	}

	ngOnInit(): void {
		this.form.controls['codigoPersonaReferida'].valueChanges.subscribe(() => {
			this.form.controls['numeroIdentificacion'].setValue('');
			this.form.controls['nombres'].setValue('');
			this.form.controls['apellidoPaterno'].setValue('');
			this.form.controls['apellidoMaterno'].setValue('');
			this.ObtenerPersona();
		});
	}

	ObtenerTodo() {
		this.tieneCambios = false;

		if (this.codigoPersona != null) {
			this.listaReferenciaPersonal = [];
			this.form.controls['numeroIdentificacion'].setValue('');
			this.form.controls['nombres'].setValue('');
			this.form.controls['apellidoPaterno'].setValue('');
			this.form.controls['apellidoMaterno'].setValue('');
			this.form.reset();
			const ObtenerReferenciaPersonales = {
				codigoPersona: this.codigoPersona,
				paginacion: {
					indiceInicial: 0,
					numeroRegistros: 50
				}
			}

			this.cargando.listar = true
			this._referenciaPersonalService.obtenerReferenciaPersonales(ObtenerReferenciaPersonales)
				.pipe(finalize(() => this.cargando.listar = false))
				.subscribe(
					data => {
						data.resultado.map((ref) => {
							this.listaReferenciaPersonal.push({objeto: ref, accion: AccionesFormulario.Leer});
						});
					},
					error => {
						this._messageService.showError(error.mensaje || error)
					}
				);
		}
	}

	ObtenerPersona() {
		if (this.form.controls['codigoPersonaReferida'].value != null) {
			this._personaService.ObtenerInfoPersona(this.form.controls['codigoPersonaReferida'].value)
				.subscribe(data => {
					if (data) {
						this.form.patchValue(data.resultado);
					}
				}, (error) => {
					this._messageService.showError(error.mensaje || error)
				})
		}
	}

	IniciarForm() {
		return new FormGroup({
			codigoPersona: new FormControl(this.codigoPersona),
			codigoPersonaReferida: new FormControl(null, [Validators.required]),
			numeroIdentificacion: new FormControl(null,[]),
			nombres: new FormControl({value: null, disabled: true}),
			apellidoPaterno: new FormControl({value: null, disabled: true}),
			apellidoMaterno: new FormControl({value: null, disabled: true}),
			fechaConoce: new FormControl(null, [Validators.required]),
			observaciones: new FormControl(null, [Validators.required]),
		});
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

		const referencia = this.form.getRawValue();

		if (this.indSeleccionado == -1) {
			this.listaReferenciaPersonal.push({objeto: referencia, accion: AccionesFormulario.Insertar});
		} else {
			this.listaReferenciaPersonal[this.indSeleccionado] = {
				objeto: referencia,
				accion: AccionesFormulario.Actualizar
			};
		}

		this.tieneCambios = true;
		this.ResetearVista();
	}

	EditarFila(elemento: ElementoFormulario<referenciaPersonal>, index: number) {
		this._accion = 'Actualizar';

		if (elemento.accion == AccionesFormulario.Insertar) {
			this.form.patchValue(elemento.objeto);
			this.listaReferenciaPersonal.splice(index, 1);
		} else {
			this.indSeleccionado = index;
			this._referenciaPersonalService.obtenerReferenciaPersonal(elemento.objeto)
				.pipe(finalize(() => null))
				.subscribe(
					data => {
						this.listaReferenciaPersonal[index].objeto = data.resultado;
						this.form.patchValue(data.resultado);

						this.ResetearVista();
					},
					error => {
						this._messageService.showError(error.mensaje || error)
					}
				);
		}
	}

	EliminarFila(index: number) {
		const referencia = this.listaReferenciaPersonal[index]

		if (referencia.accion == AccionesFormulario.Insertar) {
			this.listaReferenciaPersonal.splice(index, 1);
		} else {
			referencia.accion = AccionesFormulario.Eliminar;
		}

		this.tieneCambios = true;
	}

	GuardarBase() {
		const message = this._messageService.showLoading(`Guardando información: REFERENCIAS PERSONALES - Codigo persona: ${this.codigoPersona}`)

		this.GuardarCambios()
			.pipe(finalize(() => this._messageService.removeLoading(message)))
			.subscribe(resp => {
				this._messageService.showSuccess('Datos guardados correctamente');
			}, (err) => {
				this._messageService.showError(err.mensaje || err);
			});
	}

	GuardarCambios(): Observable<unknown> {
		if (!this.tieneCambios) return of(0);

		this.cargando.guardar = true;

		const operaciones: Observable<unknown>[] = [];

		this.listaReferenciaPersonal.forEach((elemento) => {
			const referencia = elemento.objeto;
			if (this.codigoPersona) referencia.codigoPersona = parseInt(this.codigoPersona.toString());
			switch (elemento.accion) {
				case AccionesFormulario.Insertar:

					const {apellidoMaterno, apellidoPaterno,nombres, numeroRegistro,identificacion,...resto}=referencia

					operaciones.push(this._referenciaPersonalService.guardarReferenciaPersonales(resto))
					break;

				case AccionesFormulario.Eliminar:
					const {codigoPersona, codigoPersonaReferida}=referencia
					operaciones.push(this._referenciaPersonalService.eliminaReferenciaPersonales({codigoPersona, codigoPersonaReferida}))
					break;
			}
		});

		return concat(...operaciones)
			.pipe(finalize(() => {
				this.cargando.guardar = false
				this.ObtenerTodo();
			}));
	}

	async abrirBusquedaPersona() {
		const persona = await this._modalService.abrirBusqueda();
		if (persona)
			this.form.controls['codigoPersonaReferida'].setValue(persona.codigoPersona);
	}

	LimpiarCampos() {
		this.form.reset();
	}

	ResetearVista(): void {
		this.LimpiarCampos();
		this._accion = 'Insertar';
		this.indSeleccionado = -1;
	}


	calcularTiempoRelacion(fecha: any, esTabla: boolean) {
		if (fecha) {
			const fechaConvertida = new Date(fecha);
			const anio = fechaConvertida.getFullYear();
			const mes = fechaConvertida.getMonth();
			const dia = fechaConvertida.getDate();
			if (esTabla) {
				// tabla
				return this._calcularEdadService.CalcularEdad(anio + "-" + mes + "-" + dia);
			} else {
				this.tiempoRelacion = this._calcularEdadService.CalcularEdad(anio + "-" + mes + "-" + dia);
			}
		}
	}

	VerInformacionPersona(codigoPersona: number) {
		this._modalServiceVisualizar.create({
			nzContent: DatosGeneralesComponent,
			nzWidth: '140rem',
			nzTitle: 'Datos generales',
			nzComponentParams: {
				mostrarGuardar: false,
				_codigoPersona: codigoPersona
			},
			nzOkText: 'Más información',
			nzCancelText: 'Cerrar ventana',
			nzOnOk: () => {
				window.open(`${environmentHost.baseAppUrl}/mantenimiento/ingresosocios-mock?codigo=${codigoPersona}`);
			}
		});
	}
}
