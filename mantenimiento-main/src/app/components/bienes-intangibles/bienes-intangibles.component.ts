import {Component, forwardRef} from '@angular/core';
import {ComponenteBasePersona} from '../base/componente-base-persona';

import {FormGroup} from '@angular/forms';
import {Observable, concat, of} from 'rxjs';
import {finalize} from 'rxjs/operators';
import {AccionesFormulario} from 'src/app/enums/acciones-formulario.enum';
import {ElementoFormulario} from 'src/app/models/elemento-formulario';
import {MessageService} from 'src/app/services/common/message/message.service';
import {bienIntangible} from '../../models/ingreso-socios.model';
import {FormControl, Validators} from '@angular/forms';
import {
	BienIntangibleMinResponse,
	BienIntangibleResponse,
	EliminarBienIntangibleRequest,
	InsertarBienIntangible
} from '../../models/bien-intangible';
import {BienesIntangiblesService} from '../../services/bienes-intangibles.service';
import {RegisteredComponent} from 'src/app/components/common/registered.component';
import {Catalogo} from 'src/app/components/catalogos/catalogo';
import {CatalogoService} from 'src/app/services/api/catalogos/catalogo.service';

@Component({
	selector: 'pers-bienes-intangibles',
	templateUrl: './bienes-intangibles.component.html',
	styleUrls: ['./bienes-intangibles.component.scss'],
	providers: [{
		provide: ComponenteBasePersona,
		useExisting: forwardRef(() => BienesIntangiblesComponent)
	}]
})
export class BienesIntangiblesComponent extends ComponenteBasePersona implements RegisteredComponent {

	readonly codigoComponente = 15;

	nombreComponente = 'Bienes Intangibles';
	form: FormGroup = this.IniciarForm();
	_accion = 'Ingresar';
	indSeleccionado = -1;
	listaBienesIntangibles: ElementoFormulario<BienIntangibleMinResponse>[] = [];
	formatterDollar = (value: number): string => `$ ${value ? value : ''}`;
	parserDollar = (value: string): string => value.replace('$ ', '');

	tiposBienesIntangibles: Catalogo<number>[] = []

	constructor(
		private _bienesIntangiblesService: BienesIntangiblesService,
		private _messageService: MessageService,
		private _catalogoService: CatalogoService,
	) {
		super();
		this._catalogoService.obtenerPorGet<number>('TiposBienesIntangibles')
			.subscribe(api => this.tiposBienesIntangibles = api.resultado)
	}

	ObtenerTodo() {
		this.tieneCambios = false;

		if (this.codigoPersona != null) {
			this.listaBienesIntangibles = [];
			this.LimpiarCampos();

			this.cargando.listar = true;
			this._bienesIntangiblesService.obtenerBienesIntangibles(this.codigoPersona)
				.pipe(finalize(() => this.cargando.listar = false))
				.subscribe(
					data => {
						data.resultado.forEach((ref) =>
							this.listaBienesIntangibles.push({objeto: ref, accion: AccionesFormulario.Leer}))

						this.listaBienesIntangibles = [...this.listaBienesIntangibles]

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
		const bienIntangible = this.form.getRawValue();

		if (this.indSeleccionado == -1) {
			this.listaBienesIntangibles.push({objeto: bienIntangible, accion: AccionesFormulario.Insertar});
		} else {
			this.listaBienesIntangibles[this.indSeleccionado] = {
				objeto: bienIntangible,
				accion: AccionesFormulario.Actualizar
			};
		}
		this.actualizarTablaInterfaz();
		this.tieneCambios = true;
		this.ResetearVista();
		/*this.form.get('tipoBienIntangible')?.enable();
		this.form.get('codigoReferencia')?.enable();*/
	}

	EditarFila(elemento: ElementoFormulario<BienIntangibleMinResponse>, index: number) {
		this._accion = 'Actualizar';
		if (elemento.accion == AccionesFormulario.Insertar) {
			this.form.patchValue(elemento.objeto);
			this.listaBienesIntangibles.splice(index, 1);
		} else {
			this.indSeleccionado = index;
			this._bienesIntangiblesService.obtenerBienIntangible(this.codigoPersona ?? 0, elemento.objeto.numeroRegistro)
				.pipe(finalize(() => null))
				.subscribe(
					data => {

						this.listaBienesIntangibles[index].objeto = <BienIntangibleResponse>(data.resultado);
						this.form.patchValue(data.resultado);
						this.form.get('tipoBienIntangible')?.disable();
						this.form.get('codigoReferencia')?.disable();
					},
					error => {
						this._messageService.showError(error.mensaje || error)
					}
				);
		}


	}

	EliminarFila(index: number) {
		this.ResetearVista();

		const referencia = this.listaBienesIntangibles[index]

		if (referencia.accion == AccionesFormulario.Insertar) {
			this.listaBienesIntangibles.splice(index, 1);
		} else {
			referencia.accion = AccionesFormulario.Eliminar;
		}
		this.actualizarTablaInterfaz();
		this.tieneCambios = true;
	}

	GuardarBase() {
		const message = this._messageService.showLoading(`Guardando información: Bienes Intangibles - Codigo persona: ${this.codigoPersona}`)
		this.GuardarCambios()
			.pipe(finalize(() => this._messageService.removeLoading(message)))
			.subscribe();
	}

	GuardarCambios(): Observable<unknown> {
		if (!this.tieneCambios) return of(0);

		this.cargando.guardar = true;
		const operaciones: Observable<unknown>[] = [];
		this.listaBienesIntangibles.forEach((elemento) => {
			const bienIntangible = elemento.objeto;
			if (this.codigoPersona) bienIntangible.codigoPersona = parseInt(this.codigoPersona.toString());
			switch (elemento.accion) {
				case AccionesFormulario.Insertar:
					const {numeroRegistro, ...resto} = bienIntangible;
					operaciones.push(this._bienesIntangiblesService.guardarBienesIntangibles(<InsertarBienIntangible>(resto)))
					break;

				case AccionesFormulario.Actualizar:
					operaciones.push(this._bienesIntangiblesService.actualizaBienesIntangibles(<bienIntangible>(<unknown>(bienIntangible))))
					break;

				case AccionesFormulario.Eliminar:
					operaciones.push(this._bienesIntangiblesService.eliminaBienesIntangibles(<EliminarBienIntangibleRequest>(bienIntangible)));
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
			tipoBienIntangible: new FormControl(null, [Validators.required]),
			codigoReferencia: new FormControl(null, [Validators.required]),
			descripcion: new FormControl(null, [Validators.required]),
			avaluoComercial: new FormControl(null, [Validators.required]),
		});
	}

	LimpiarCampos() {
		this.form.reset();
	}

	ResetearVista(): void {
		this.LimpiarCampos();
		this.form.get('tipoBienIntangible')?.enable();
		this.form.get('codigoReferencia')?.enable();
		this._accion = 'Insertar';
		this.indSeleccionado = -1;
	}

	setearLista() {
		this.listaBienesIntangibles = [];
	}

	ObtenerNombreIntangiblePorCodigo(codigo: number) {
		return this.tiposBienesIntangibles.find(p => p.codigo === codigo)?.descripcion || codigo
	}

	// * metodo para actualizar tabla de ng-zorro

	actualizarTablaInterfaz() {
		this.listaBienesIntangibles = [...this.listaBienesIntangibles];
	}

	// ! fin metodo para actualizar tabla de ng-zorro

}
