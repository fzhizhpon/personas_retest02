import {Component, forwardRef} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {NzModalService} from 'ng-zorro-antd/modal';
import {concat, Observable, of} from 'rxjs';
import {finalize} from 'rxjs/operators';
import {Catalogo} from 'src/app/components/catalogos/catalogo';
import {RegisteredComponent} from 'src/app/components/common/registered.component';
import {AccionesFormulario} from 'src/app/enums/acciones-formulario.enum';
import {ElementoFormulario} from 'src/app/models/elemento-formulario';
import {CatalogoService} from 'src/app/services/api/catalogos/catalogo.service';
import {MessageService} from 'src/app/services/common/message/message.service';
import {environmentHost} from 'src/environments/environment';
import {ElimRepresentanteRequest, RepresentanteRequest, RepresentanteResponse} from '../../models/representante';
import {PersonaService} from '../../services/persona.service';
import {RepresentanteService} from '../../services/representante.service';
import {ComponenteBasePersona} from '../base/componente-base-persona';
import {DatosGeneralesComponent} from '../datos-generales/datos-generales.component';

@Component({
	selector: 'pers-representantes',
	templateUrl: './representantes.component.html',
	styleUrls: ['./representantes.component.scss'],
	providers: [{
		provide: ComponenteBasePersona,
		useExisting: forwardRef(() => RepresentantesComponent)
	}]
})
export class RepresentantesComponent extends ComponenteBasePersona implements RegisteredComponent {

	readonly codigoComponente = 6;

	nombreComponente = 'Representante / Apoderado / Interdicto';
	form: FormGroup;
	listaRepresentantes: ElementoFormulario<RepresentanteResponse>[] = [];
	_accion = 'Ingresar';
	indSeleccionado = -1;

	tiposRelaciones: Catalogo<number>[] = []

	constructor(
		private _messageService: MessageService,
		private _repreService: RepresentanteService,
		private _personaService: PersonaService,
		private _catalogoService: CatalogoService,
		private _modalService: NzModalService,
	) {
		super();

		this.form = this.obtenerFormulario()
		this._catalogoService.obtenerPorGet<number>('TiposRepresentantes')
			.subscribe(api => this.tiposRelaciones = api.resultado)
	}

	obtenerFormulario(): FormGroup {
		return new FormGroup({
			codigoPersona: new FormControl(this.codigoPersona),
			codigoRepresentante: new FormControl(null, [Validators.required]),
			codigoTipoRepresentante: new FormControl(null, [Validators.required]),
			principal: new FormControl(false),
		});
	}

	ObtenerTodo(): void {
		this.tieneCambios = false;

		if (this.codigoPersona == null) return;

		this._repreService.obtenerRepresentantes(this.codigoPersona)
			.subscribe(data => {
				this.listaRepresentantes = [];

				data.resultado.map((representante) => {
					this.listaRepresentantes.push({
						objeto: representante,
						accion: AccionesFormulario.Leer
					});
				});

				this.ResetearVista();
			})
	}

	GuardarBase() {
		const message = this._messageService.showLoading(`Guardando información: CELULARES - Codigo persona: ${this.codigoPersona}`)

		this.GuardarCambios()
			.pipe(finalize(() => this._messageService.removeLoading(message)))
			.subscribe(() => {
				this._messageService.showSuccess("Datos guardados correctamente");
			}, (error) => {
				this._messageService.showError(error.mensaje || error);
			});
	}

	GuardarCambios(): Observable<unknown> {
		if (!this.tieneCambios) return of(0);

		this.cargando.guardar = true;

		const operaciones: Observable<unknown>[] = [];

		this.listaRepresentantes.forEach((elemento) => {
			const representante = elemento.objeto;

			if (this.codigoPersona) representante.codigoPersona = parseInt(this.codigoPersona.toString());


			switch (elemento.accion) {
				case AccionesFormulario.Insertar:
					const {apellidoMaterno, apellidoPaterno, nombres, numeroIdentificacion, ...resto} = representante;
					operaciones.push(this._repreService.insertarRepresentante(<RepresentanteRequest>(resto)))

					//operaciones.push(this._repreService.insertarRepresentante(<RepresentanteRequest>(representante)))
					break;

				case AccionesFormulario.Eliminar:
					const {codigoPersona, codigoRepresentante} = representante
					operaciones.push(this._repreService.eliminarRepresentante(<ElimRepresentanteRequest>({
						codigoPersona,
						codigoRepresentante
					})))

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

		const representante = this.form.getRawValue();

		if (representante.codigoRepresentante == this.codigoPersona) {
			this._messageService.showError('Error, la persona no puede representarse a si mismo')
		} else {
			if (this.listaRepresentantes.length === 0) representante.principal = true

			if (representante.principal) {
				this.listaRepresentantes.forEach(cel => cel.objeto.principal = false)
			}

			if (this.indSeleccionado == -1) {
				this.ObtenerRepresentante(representante);
			} else {
				this.listaRepresentantes[this.indSeleccionado].objeto = representante;
			}
		}
		this.actualizarTablaInterfaz();
		this.tieneCambios = true;
		this.ResetearVista();
	}

	EliminarFila(index: number) {
		this.ResetearVista();

		const representante = this.listaRepresentantes[index]

		if (representante.accion == AccionesFormulario.Insertar) {
			this.listaRepresentantes.splice(index, 1);
		} else {
			representante.accion = AccionesFormulario.Eliminar;
		}
		this.actualizarTablaInterfaz();
		this.tieneCambios = true;
	}

	LimpiarCampos() {
		this.form.reset();
	}

	ResetearVista(): void {
		this.LimpiarCampos();
		this._accion = 'Insertar';
		this.indSeleccionado = -1;
		this.cargando.actualizar = false;
	}

	ObtenerRepresentante(representante: any) {
		const codigoRepresentante = this.form.get('codigoRepresentante')?.value;

		if (!codigoRepresentante) return;

		this.cargando.actualizar = true

		this._personaService.ObtenerInfoPersona(codigoRepresentante)
			.pipe(finalize(() => this.cargando.actualizar = false))
			.subscribe(data => {
				if (data) {
					representante.nombres = data.resultado.nombres
					representante.apellidoPaterno = data.resultado.apellidoPaterno
					representante.apellidoMaterno = data.resultado.apellidoPaterno
					representante.numeroIdentificacion = data.resultado.numeroIdentificacion
					this.listaRepresentantes.push({objeto: representante, accion: AccionesFormulario.Insertar});
				}
			}, (error) => {
				this._messageService.showError(error.mensaje || error);
			})
	}

	ObtenerTipoRelacionPorCodigo(codigo: number) {
		return this.tiposRelaciones.find(p => p.codigo === codigo)?.descripcion || codigo
	}

	VerInformacionPersona(codigoPersona: number) {
		this._modalService.create({
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


	// * metodo para actualizar tabla de ng-zorro

	actualizarTablaInterfaz() {
		this.listaRepresentantes = [...this.listaRepresentantes];
	}

	// ! fin metodo para actualizar tabla de ng-zorro
}
