import { Component, forwardRef } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { concat, Observable, of } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { RegisteredComponent } from 'src/app/components/common/registered.component';
import { AccionesFormulario } from 'src/app/enums/acciones-formulario.enum';
import { ElementoFormulario } from 'src/app/models/elemento-formulario';
import { MessageService } from 'src/app/services/common/message/message.service';
import { referenciaComercial } from '../../models/ingreso-socios.model';
import { ReferenciaComercialService } from '../../services/referencia-comercial.service';
import { ComponenteBasePersona } from '../base/componente-base-persona';
import { differenceInCalendarDays } from 'date-fns';

@Component({
	selector: 'pers-referencia-comercial',
	templateUrl: './referencia-comercial.component.html',
	styleUrls: ['./referencia-comercial.component.scss'],
	providers: [{
		provide: ComponenteBasePersona,
		useExisting: forwardRef(() => ReferenciaComercialComponent)
	}]
})
export class ReferenciaComercialComponent extends ComponenteBasePersona implements RegisteredComponent {

	readonly codigoComponente = 12;

	nombreComponente = 'Referencias Comerciales';
	form: FormGroup = this.IniciarForm();

	_accion = 'Ingresar';
	indSeleccionado = -1;

	listaReferenciaComercial: ElementoFormulario<referenciaComercial>[] = [];

	dateFormat = "dd/MM/yyyy";


	formatterDollar = (value: number): string => `$ ${value ? value : ''}`;
	parserDollar = (value: string): string => value.replace('$ ', '');
	disabledDate = (current: Date): boolean => differenceInCalendarDays(current, new Date()) > 0 || differenceInCalendarDays(current, new Date()) <-37380;


	constructor(
		private referenciaComercialService: ReferenciaComercialService,
		private _messageService: MessageService,
	) {
		super();
	}

	ObtenerTodo() {
		this.tieneCambios = false;

		if (this.codigoPersona != null) {
			this.cargando.listar = true;

			this.listaReferenciaComercial = [];
			this.LimpiarCampos();

			const filtros = {
				codigoPersona: this.codigoPersona,
				paginacion: {
					indiceInicial: 0,
					numeroRegistros: 50
				}
			}

			this.cargando.listar = true
			this.referenciaComercialService.obtenerReferenciasComerciales(filtros)
				.pipe(finalize(() => this.cargando.listar = false))
				.subscribe(
					data => {
						data.resultado.map((ref) => {
							this.listaReferenciaComercial.push({ objeto: ref, accion: AccionesFormulario.Leer });
						});

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
					control.updateValueAndValidity({ onlySelf: true });
				}
			});
			return;
		}

		const referencia = this.form.getRawValue();

		referencia.codigoPersona = this.codigoPersona;
		referencia.codigoPais = referencia.lugar.codigoPais;
		referencia.codigoProvincia = referencia.lugar.codigoProvincia;
		referencia.codigoCiudad = referencia.lugar.codigoCiudad;
		referencia.codigoParroquia = referencia.lugar.codigoParroquia;

		if (this.indSeleccionado == -1) {
			this.listaReferenciaComercial.push({ objeto: referencia, accion: AccionesFormulario.Insertar });
		} else {
			this.listaReferenciaComercial[this.indSeleccionado] = {
				objeto: referencia,
				accion: AccionesFormulario.Actualizar
			};
		}

		this.tieneCambios = true;
		this.ResetearVista();
		this.form.get('establecimiento')?.enable();
		this.form.get('fechaRelacion')?.enable();
	}

	EditarFila(elemento: ElementoFormulario<referenciaComercial>, index: number) {
		this._accion = 'Actualizar';

		if (elemento.accion == AccionesFormulario.Insertar) {
			this.form.patchValue(elemento.objeto);
			this.form.get('lugar')?.setValue({
				codigoPais: elemento.objeto.codigoPais,
				codigoProvincia: elemento.objeto.codigoProvincia,
				codigoCiudad: elemento.objeto.codigoCiudad,
				codigoParroquia: elemento.objeto.codigoParroquia,
			})
			this.listaReferenciaComercial.splice(index, 1);
		} else {
			this.indSeleccionado = index;
			this.referenciaComercialService.obtenerReferenciaComercial(elemento.objeto)
				.pipe(finalize(() => null))
				.subscribe(
					data => {
						this.listaReferenciaComercial[index].objeto = data.resultado;

						this.form.patchValue(data.resultado);
						this.form.get('lugar')?.setValue({
							codigoPais: data.resultado.codigoPais,
							codigoProvincia: data.resultado.codigoProvincia,
							codigoCiudad: data.resultado.codigoCiudad,
							codigoParroquia: data.resultado.codigoParroquia,
						});
						this.form.get('establecimiento')?.disable();
						this.form.get('fechaRelacion')?.disable();
					},
					error => {
						this._messageService.showError(error.mensaje || error)
					}
				);
		}
	}

	EliminarFila(index: number) {
		this.ResetearVista();

		const referencia = this.listaReferenciaComercial[index]

		if (referencia.accion == AccionesFormulario.Insertar) {
			this.listaReferenciaComercial.splice(index, 1);
		} else {
			referencia.accion = AccionesFormulario.Eliminar;
		}

		this.tieneCambios = true;
	}

	GuardarBase() {
		const message = this._messageService.showLoading(`Guardando información: REFERENCIAS COMERCIALES - Codigo persona: ${this.codigoPersona}`)

		this.GuardarCambios()
			.pipe(finalize(() => this._messageService.removeLoading(message)))
			.subscribe((resp) => {
				this._messageService.showSuccess('Datos guardados correctamente');
			}, (err) => {
				this._messageService.showError(err.mensaje || err);
			});
	}

	GuardarCambios(): Observable<unknown> {
		if (!this.tieneCambios) return of(0);

		this.cargando.guardar = true;

		const operaciones: Observable<unknown>[] = [];

		this.listaReferenciaComercial.forEach((elemento) => {
			const referencia = elemento.objeto;

			if (this.codigoPersona) referencia.codigoPersona = parseInt(this.codigoPersona.toString());

			const { numeroRegistro, lugar, ...resto } = referencia;
			switch (elemento.accion) {
				case AccionesFormulario.Insertar:

					operaciones.push(this.referenciaComercialService.guardarReferenciasComerciales(<referenciaComercial>(<unknown>resto)))
					break;

				case AccionesFormulario.Actualizar:

					operaciones.push(this.referenciaComercialService.actualizaReferenciasComerciales(referencia))
					break;

				case AccionesFormulario.Eliminar:
					const {codigoPersona, numeroRegistro}=referencia
					operaciones.push(this.referenciaComercialService.eliminaReferenciasComerciales({codigoPersona, numeroRegistro}))
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
			//codigoReferenciaComercial: new FormControl(null),
			numeroRegistro: new FormControl(null),
			lugar: new FormControl(null, [Validators.required]),
			codigoPais: new FormControl(null),
			codigoProvincia: new FormControl(null),
			codigoCiudad: new FormControl(null),
			codigoParroquia: new FormControl(null),
			establecimiento: new FormControl(null, [Validators.required]),
			fechaRelacion: new FormControl(null, [Validators.required]),
			montoCredito: new FormControl(null, [Validators.required]),
			plazo: new FormControl(null, [Validators.required]),
			codigoTipoTiempo: new FormControl(null, [Validators.required]),
			telefono: new FormControl(null,[Validators.required,Validators.minLength(7), Validators.maxLength(20),])
		});
	}

	LimpiarCampos() {
		this.form.reset();
	}

	ResetearVista(): void {
		this.LimpiarCampos();
		this._accion = 'Insertar';
		this.indSeleccionado = -1;
		this.form.get('establecimiento')?.enable();
		this.form.get('fechaRelacion')?.enable();
	}
}
