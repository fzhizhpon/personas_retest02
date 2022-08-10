import {AccionesFormulario} from 'src/app/enums/acciones-formulario.enum';
import {
	ActualizarCelularRequest,
	CelularResponse,
	EliminarCelularRequest,
	InsertarCelularRequest
} from '../../models/celular';
import {CelularesService} from '../../services/celulares.service';
import {Component, forwardRef} from '@angular/core';
import {ComponenteBasePersona} from '../base/componente-base-persona';
import {ElementoFormulario} from 'src/app/models/elemento-formulario';
import {finalize} from 'rxjs/operators';
import {Observable, concat, of} from 'rxjs';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {MessageService} from 'src/app/services/common/message/message.service';
import {Respuesta} from 'src/app/models/respuesta';
import {RegisteredComponent} from 'src/app/components/common/registered.component';
import {Catalogo} from 'src/app/components/catalogos/catalogo';
import {CatalogoService} from 'src/app/services/api/catalogos/catalogo.service';

@Component({
	selector: 'pers-celulares',
	templateUrl: './celulares-socio.component.html',
	styleUrls: ['./celulares-socio.component.scss'],
	providers: [{
		provide: ComponenteBasePersona,
		useExisting: forwardRef(() => CelularesComponent)
	}]
})

export class CelularesComponent extends ComponenteBasePersona implements RegisteredComponent {

	readonly codigoComponente = 4;

	nombreComponente = 'Celulares';
	form: FormGroup = this.IniciarForm();

	_accion = 'Insertar';
	_codigoCelular = -1;

	indCelularSeleccionado = -1;

	listaCelulares: ElementoFormulario<CelularResponse>[] = [];

	tiposOperadoras: Catalogo<number>[] = []

	constructor(
		private celularesService: CelularesService,
		private _messageService: MessageService,
		private _catalogoService: CatalogoService,
	) {
		super();
		this._catalogoService.obtenerPorGet<number>('operadoras-moviles')
			.subscribe(api => this.tiposOperadoras = api.resultado);
	}

	ObtenerTodo() {
		this.tieneCambios = false;

		if (this.codigoPersona == null) return;

		this.cargando.listar = true;

		this.listaCelulares = [];
		this.LimpiarCampos();

		const filtros = {
			codigoPersona: this.codigoPersona,
			paginacion: {
				indiceInicial: 0,
				numeroRegistros: 50
			}
		}

		this.celularesService.obtenerCelulares(filtros)
			.pipe(finalize(() => this.cargando.listar = false))
			.subscribe(
				data => {
					data.resultado.map((celular) => this.listaCelulares.push({
						objeto: celular,
						accion: AccionesFormulario.Leer
					}));
					this.listaCelulares = [...this.listaCelulares]

					this.ResetearVista();
					this.form.get('codigoPais')?.setValue(61);
				},
				(error: Respuesta<void>) => {
					this._messageService.showError(error.mensaje || error)
				}
			);
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

		const celular = this.form.getRawValue();

		if (this.listaCelulares.length == 0) celular.principal = true

		if (celular.principal) {
			this.listaCelulares.forEach(cel => cel.objeto.principal = false)
		}

		if (this.indCelularSeleccionado == -1) {
			this.listaCelulares.push({objeto: celular, accion: AccionesFormulario.Insertar});
		} else {
			this.listaCelulares[this.indCelularSeleccionado] = {objeto: celular, accion: AccionesFormulario.Actualizar}
		}
		this.actualizarTablaInterfaz();
		this.tieneCambios = true;
		this.ResetearVista();
	}

	EditarFila(elemento: ElementoFormulario<CelularResponse>, index: number) {
		this.indCelularSeleccionado = index;

		this.form.patchValue(elemento.objeto);
		this.form.get('numero')?.disable();
		this.form.get('codigoPais')?.disable();

		this._accion = 'Actualizar'
	}

	EliminarFila(index: number) {

		this.ResetearVista()
		const celular = this.listaCelulares[index]

		if (celular.accion == AccionesFormulario.Insertar) {
			this.listaCelulares.splice(index, 1);
		} else {
			celular.accion = AccionesFormulario.Eliminar;
		}
		this.actualizarTablaInterfaz();
		this.tieneCambios = true;
	}

	GuardarBase() {
		if (!this.tieneCambios) return of(0);

		const message = this._messageService.showLoading(`Guardando información: CELULARES - Codigo persona: ${this.codigoPersona}`)

		this.GuardarCambios()
			.pipe(finalize(() => this._messageService.removeLoading(message)))
			.subscribe(() => {
				this._messageService.showSuccess("Datos guardados correctamente");
			}, () => {
				this._messageService.showError("Datos guardados correctamente");
			});
	}

	GuardarCambios(): Observable<unknown> {
		this.cargando.guardar = true;

		const operaciones: Observable<unknown>[] = [];

		this.listaCelulares.forEach((elemento) => {
			const celular = elemento.objeto;

			if (this.codigoPersona) celular.codigoPersona = parseInt(this.codigoPersona.toString());


			switch (elemento.accion) {
				case AccionesFormulario.Insertar:
					operaciones.push(this.celularesService.insertarCelular(<InsertarCelularRequest>(celular)))
					break;

				case AccionesFormulario.Actualizar:
					operaciones.push(this.celularesService.actualizarCelular(<ActualizarCelularRequest>(celular)))
					break;

				case AccionesFormulario.Eliminar:
					const {codigoPersona, codigoTelefonoMovil} = celular
					operaciones.push(this.celularesService.eliminarCelular(<EliminarCelularRequest>({
						codigoPersona,
						codigoTelefonoMovil
					})))
					break;
			}
		});

		return concat(...operaciones)
			.pipe(finalize(() => {
				this.ObtenerTodo();
			}));
	}

	IniciarForm() {
		return new FormGroup({
			codigoPais: new FormControl(null, [Validators.required]),
			numero: new FormControl(null, [Validators.required, Validators.minLength(10), Validators.maxLength(10), Validators.pattern(/^[0-9]\d*$/)]),
			codigoOperadora: new FormControl(null, [Validators.required]),
			observaciones: new FormControl(null, []),
			codigoTelefonoMovil: new FormControl(null),
			principal: new FormControl(false),

		});
	}

	LimpiarCampos() {
		this.form.reset();
	}

	ResetearVista(): void {
		this.LimpiarCampos();
		this._accion = 'Insertar';
		this.indCelularSeleccionado = -1;
		this.form.get('numero')?.enable();
		this.form.get('codigoPais')?.enable();
		this.form.get('codigoPais')?.setValue(61);
	}

	ObtenerNombreOperadoraPorCodigo(codigo: number) {
		return this.tiposOperadoras.find(p => p.codigo === codigo)?.descripcion || codigo
	}

	confirm() {
		console.log('eliminado correctamente');
	}

	// * metodo para actualizar tabla de ng-zorro

	actualizarTablaInterfaz() {
		this.listaCelulares = [...this.listaCelulares];
	}

	// ! fin metodo para actualizar tabla de ng-zorro
}
