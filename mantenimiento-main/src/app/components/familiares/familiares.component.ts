import {AccionesFormulario} from 'src/app/enums/acciones-formulario.enum';
import {Component, forwardRef} from '@angular/core';
import {ComponenteBasePersona} from '../base/componente-base-persona';
import {ElementoFormulario} from 'src/app/models/elemento-formulario';
import {FamiliaresService} from '../../services/familiares.service';
import {
	EliminarFamiliarRequest,
	FamiliarResponse,
	InsertarFamiliarRequest,
	ActualizarFamiliarRequest
} from '../../models/familiar';
import {finalize} from 'rxjs/operators';
import {concat, Observable, of} from 'rxjs';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {MessageService} from 'src/app/services/common/message/message.service';
import {PersonaService} from '../../services/persona.service';
import {Parentesco} from 'src/app/components/catalogos/catalogo';
import {CatalogoService} from 'src/app/services/api/catalogos/catalogo.service';
import {RegisteredComponent} from 'src/app/components/common/registered.component';

@Component({
	selector: 'pers-familiares',
	templateUrl: './familiares.component.html',
	styleUrls: ['./familiares.component.scss'],
	providers: [{
		provide: ComponenteBasePersona,
		useExisting: forwardRef(() => FamiliaresComponent)
	}]
})
export class FamiliaresComponent extends ComponenteBasePersona implements RegisteredComponent {

	readonly codigoComponente = 8;

	nombreComponente = 'Familiares';
	form: FormGroup;
	listaFamiliares: ElementoFormulario<FamiliarResponse>[] = [];
	_accion = 'Insertar';
	indSeleccionado = -1;
	indFamiliarSeleccionado = -1;

	parentescos: Parentesco[] = []

	constructor(
		private _messageService: MessageService,
		private _familiarService: FamiliaresService,
		private _personaService: PersonaService,
		private _catalogoService: CatalogoService,
	) {
		super();

		this.form = this.obtenerFormulario()
		this._catalogoService.obtenerParentescos()
			.subscribe(api => this.parentescos = api.resultado)
	}

	obtenerFormulario(): FormGroup {
		return new FormGroup({
			codigoPersona: new FormControl(this.codigoPersona),
			codigoPersonaFamiliar: new FormControl(null, [Validators.required]),
			codigoParentesco: new FormControl(null, [Validators.required]),
			observacion: new FormControl(null, [Validators.required]),
			esCargaFamiliar: new FormControl(false),
			nombres: new FormControl(null),
			apellidoPaterno: new FormControl(null),
			apellidoMaterno: new FormControl(null)

		});

	}

	ObtenerTodo(): void {
		this.tieneCambios = false;

		if (this.codigoPersona == null) return;

		this._familiarService.obtenerFamiliares(this.codigoPersona)
			.subscribe(data => {
				this.listaFamiliares = [];
				data.resultado.map((familiar) => {
					this.listaFamiliares.push({
						objeto: familiar,
						accion: AccionesFormulario.Leer
					})

					this.ResetearVista();
				});
			}, (error) => {
				this._messageService.showError(error.mensaje || error)
			})
	}

	GuardarBase() {
		const message = this._messageService.showLoading(`Guardando información: Familiares - Codigo persona: ${this.codigoPersona}`)
		this.GuardarCambios()
			.pipe(finalize(() => this._messageService.removeLoading(message)))
			.subscribe(() => {
				this._messageService.showSuccess('Datos guardados correctamente');
			}, (error) => {
				this._messageService.showError(error.mensaje || error);
			});
	}

	GuardarCambios(): Observable<unknown> {
		if (!this.tieneCambios) return of(0);

		this.cargando.guardar = true;
		const operaciones: Observable<unknown>[] = [];

		this.listaFamiliares.forEach((elemento) => {
			const familiar = elemento.objeto;
			if (this.codigoPersona) familiar.codigoPersona = parseInt(this.codigoPersona.toString());


			switch (elemento.accion) {
				case AccionesFormulario.Insertar:

					const {
						codigoParentesco,
						codigoPersona,
						codigoPersonaFamiliar,
						esCargaFamiliar,
						observacion
					} = familiar
					operaciones.push(this._familiarService.insertarFamiliar(<InsertarFamiliarRequest>({
						codigoParentesco,
						codigoPersona,
						codigoPersonaFamiliar,
						esCargaFamiliar,
						observacion
					})))
					break;

				case AccionesFormulario.Eliminar:
					operaciones.push(this._familiarService.eliminarFamiliar(<EliminarFamiliarRequest>(familiar)))
					break;

				case AccionesFormulario.Actualizar:

					operaciones.push(this._familiarService.actualizarFamiliar(<ActualizarFamiliarRequest>(familiar)));
					break;
			}
		});

		return concat(...operaciones)
			.pipe(finalize(() => {
				this.cargando.guardar = false
				this.ObtenerTodo();
			}));
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

		const familiar = this.form.getRawValue();

		familiar.codigoPersona = this.codigoPersona;

		if (!this.VerificarCodigoPersona(familiar.codigoPersonaFamiliar)) return;

		if (this.indSeleccionado == -1) {
			this.ObtenerPersona(familiar.codigoParentesco, familiar.observacion, familiar.esCargaFamiliar);
		} else {
			this.listaFamiliares[this.indSeleccionado].objeto = familiar;
		}

		this.tieneCambios = true;
		this.ResetearVista();
	}

	EditarFila(elemento: ElementoFormulario<FamiliarResponse>, index: number) {
		this._accion = 'Actualizar';

		if (elemento.accion == AccionesFormulario.Insertar) {
			this.form.patchValue(elemento.objeto);
			this.listaFamiliares.splice(index, 1);
		} else {
			this.indSeleccionado = index;
			elemento.accion = AccionesFormulario.Actualizar;

			this._familiarService.obtenerFamiliar(this.codigoPersona || 0, elemento.objeto.codigoPersonaFamiliar)
				.pipe(finalize(() => null))
				.subscribe(
					data => {
						data.resultado.codigoPersonaFamiliar = elemento.objeto.codigoPersonaFamiliar
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

		const familiar = this.listaFamiliares[index]

		if (familiar.accion == AccionesFormulario.Insertar) {
			this.listaFamiliares.splice(index, 1);
		} else {
			familiar.accion = AccionesFormulario.Eliminar;
		}

		this.tieneCambios = true;
	}

	LimpiarCampos() {
		this.form.reset();
	}

	ObtenerPersona(codigoParentesco: number, observacion: string, esCargaFamiliar: boolean) {
		const codigoFamiliar = this.form.get('codigoPersonaFamiliar')?.value

		if (codigoFamiliar != null) {
			this._personaService.ObtenerInfoPersona(this.form.controls['codigoPersonaFamiliar'].value)
				.subscribe(data => {
					if (data) {
						data.resultado.codigoPersonaFamiliar = codigoFamiliar
						data.resultado.codigoParentesco = codigoParentesco
						data.resultado.observacion = observacion
						data.resultado.esCargaFamiliar = esCargaFamiliar

						this.listaFamiliares.push({objeto: data.resultado, accion: AccionesFormulario.Insertar})
					}
				}, error => {
					this._messageService.showError(error.mensaje || error)
				})
		}
	}

	obtenerParentescoCodigo(codigo: number) {
		return this.parentescos.find(p => p.codigoParentesco === codigo)
	}

	ResetearVista(): void {
		this.LimpiarCampos();
		this._accion = 'Insertar';
		this.indSeleccionado = -1;
	}

	VerificarCodigoPersona(codigoPersona: number) {
		if (!codigoPersona) return true;

		if (codigoPersona == this.codigoPersona) {
			this._messageService.showError('No puede registrar como familiar a la misma persona')
			this.form.get('codigoPersonaFamiliar')?.setValue(null)
			return false;
		}

		return true;
	}

}
