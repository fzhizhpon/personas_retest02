import {Component, forwardRef} from '@angular/core';
import {ComponenteBasePersona} from '../base/componente-base-persona';
import {correoElectronico, manejoDeEstado} from '../../models/ingreso-socios.model';
import {CorreosElectronicosService} from '../../services/correos-electronicos.service';
import {finalize} from 'rxjs/operators';
import {concat, Observable, of} from 'rxjs';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {MessageService} from 'src/app/services/common/message/message.service';
import {AccionesFormulario} from 'src/app/enums/acciones-formulario.enum';
import {ElementoFormulario} from 'src/app/models/elemento-formulario';
import {RegisteredComponent} from 'src/app/components/common/registered.component';

@Component({
	selector: 'pers-correos-electronicos',
	templateUrl: './correos-electronicos.component.html',
	styleUrls: ['./correos-electronicos.component.scss'],
	providers: [{
		provide: ComponenteBasePersona,
		useExisting: forwardRef(() => CorreosElectronicosComponent)
	}]
})
export class CorreosElectronicosComponent extends ComponenteBasePersona implements RegisteredComponent {

	readonly codigoComponente = 5;

	nombreComponente = 'Correos Electrónicos';
	_accion = 'Ingresar';
	_indice = -1;

	listaCorreos: ElementoFormulario<correoElectronico>[] = [];

	form: FormGroup = this.IniciarForm();

	constructor(
		private _correosService: CorreosElectronicosService,
		private _messageService: MessageService,
	) {
		super();
	}

	ObtenerTodo() {
		this.tieneCambios = false;

		if (this.codigoPersona != null) {
			this.listaCorreos = [];
			this.LimpiarCampos();

			const filtros = {
				codigoPersona: this.codigoPersona,
				paginacion: {
					indiceInicial: 0,
					numeroRegistros: 50
				}
			}

			this.cargando.listar = true
			this._correosService.obtenerCorreosElectronicos(filtros)
				.pipe(finalize(() => this.cargando.listar = false))
				.subscribe(
					data => {
						data.resultado.forEach(ref => this.listaCorreos.push({
							objeto: ref,
							accion: AccionesFormulario.Leer
						}));
						this.actualizarTablaInterfaz();
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
		const obj = this.form.getRawValue();

		if (this.listaCorreos.length == 0) obj.esPrincipal = true

		obj.codigoPersona = this.codigoPersona;
		// * correo minusculas
		obj.correoElectronico = obj.correoElectronico.toLowerCase();
		if (this._indice == -1) {
			this.listaCorreos.push({objeto: obj, accion: AccionesFormulario.Insertar});

		} else {
			this.listaCorreos[this._indice] = {
				objeto: obj,
				accion: AccionesFormulario.Actualizar
			};
		}
		this.actualizarTablaInterfaz();
		this.tieneCambios = true;
		this.ResetearVista();
	}

	EditarFila(data: manejoDeEstado<correoElectronico> | any, index: number) {
		if (data.accion == AccionesFormulario.Insertar) {
			this.form.patchValue(data.objeto);
			this.listaCorreos.splice(index, 1);
			return;
		}

		if (data.objeto.esPrincipal == '1') data.objeto.esPrincipal = true
		else data.objeto.esPrincipal = false

		this.form.patchValue(data.objeto);
		this._accion = 'Modificar';
		this.form.patchValue(data.objeto);
		this._indice = index;
		this.form.get('correoElectronico')?.disable();
	}

	EliminarFila(index: number) {
		this.ResetearVista();
		const referencia = this.listaCorreos[index]

		if (referencia.accion == AccionesFormulario.Insertar) {
			this.listaCorreos.splice(index, 1);
		} else {
			referencia.accion = AccionesFormulario.Eliminar;
		}
		this.actualizarTablaInterfaz();
		this.tieneCambios = true;
	}

	GuardarBase() {
		const message = this._messageService.showLoading(`Guardando información: CORREOS ELECTRÓNICOS - Codigo persona: ${this.codigoPersona}`)

		this.GuardarCambios()
			.pipe(finalize(() => this._messageService.removeLoading(message)))
			.subscribe(() => {
				this._messageService.showSuccess("Datos guardados correctamente");
			}, () => {
				this._messageService.showError("Datos guardados correctamente");
			});
	}

	GuardarCambios(): Observable<any> {
		if (!this.tieneCambios) return of(0);

		this.cargando.guardar = true;

		const operaciones: Observable<unknown>[] = [];

		this.listaCorreos.forEach((elemento) => {
			const correoElectronico = elemento.objeto;

			if (this.codigoPersona) correoElectronico.codigoPersona = parseInt(this.codigoPersona.toString());

			const {codigoCorreoElectronico, ...resto} = correoElectronico;
			switch (elemento.accion) {
				case AccionesFormulario.Insertar:
					operaciones.push(this._correosService.guardarCorreosElectronicos(<correoElectronico>(<unknown>(resto))))
					break;

				case AccionesFormulario.Actualizar:
					operaciones.push(this._correosService.actualizaCorreosElectronicos(<correoElectronico>(<unknown>(correoElectronico))))
					break;

				case AccionesFormulario.Eliminar:
					const {codigoPersona, codigoCorreoElectronico} = correoElectronico
					operaciones.push(this._correosService.eliminaCorreosElectronicos(<correoElectronico>(<unknown>({
						codigoPersona,
						codigoCorreoElectronico
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
			codigoPersona: new FormControl(null),
			codigoCorreoElectronico: new FormControl(null),
			correoElectronico: new FormControl(null, [Validators.required, Validators.pattern("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4}$")]),
			observaciones: new FormControl(null, [Validators.required]),
			esPrincipal: new FormControl(false)
		});
	}

	LimpiarCampos() {
		this.form.reset();
	}

	ResetearVista(): void {
		this.LimpiarCampos();
		this._accion = 'Insertar';
		this._indice = -1;

		this.form.get('correoElectronico')?.enable();
	}


	// * metodo para actualizar tabla de ng-zorro

	actualizarTablaInterfaz() {
		this.listaCorreos = [...this.listaCorreos];
	}

	// ! fin metodo para actualizar tabla de ng-zorro

}
