/* eslint-disable @typescript-eslint/no-non-null-assertion */
import {Component, forwardRef, Input} from '@angular/core';
import {finalize} from 'rxjs/operators';
import {RegisteredComponent} from 'src/app/components/common/registered.component';
import {AccionesFormulario} from 'src/app/enums/acciones-formulario.enum';
import {ElementoFormulario} from 'src/app/models/elemento-formulario';
import {CatalogoService} from 'src/app/services/api/catalogos/catalogo.service';
import {MessageService} from 'src/app/services/common/message/message.service';
import {InformacionAdicional} from '../../models/informacion-adicional';
import {InformacionAdicionalService} from '../../services/informacion-adicional.service';
import {ComponenteBasePersona} from '../base/componente-base-persona';
import {Catalogo} from 'src/app/components/catalogos/catalogo';
import {concat, Observable, of} from 'rxjs';

@Component({
	selector: 'pers-informacion-adicional',
	templateUrl: './informacion-adicional.component.html',
	styleUrls: ['./informacion-adicional.component.scss'],
	providers: [{
		provide: ComponenteBasePersona,
		useExisting: forwardRef(() => InformacionAdicionalComponent)
	}]
})
export class InformacionAdicionalComponent extends ComponenteBasePersona implements RegisteredComponent {

	readonly codigoComponente = 13;
	nombreComponente = 'Información Adicional';
	cuentaEditar = '';
	_accion = 'Ingresar';
	tablaComunesCabeceras: Cabecera[] = []
	isLoading = false;
	tabSeleccionado = 0;
	codigoModulo!: number | null;

	@Input('codigoModulo')
	set _codigoModulo(value: number | null) {
		if (value != null) {
			this.codigoModulo = value;
		}
	}

	constructor(
		private _messageService: MessageService,
		private _informacionAdicionalService: InformacionAdicionalService,
		private _catalogoService: CatalogoService,
	) {
		super();
	}

	ObtenerTodo(): void {
		this._catalogoService.eliminarPorPost<number>('eliminar', {"key" : "TablasComunesCabaceras"})
			.pipe(finalize(() => this.cargando.listar = false))
			.subscribe();

		this.tieneCambios = false;
		this.tablaComunesCabeceras = []
		this._catalogoService.obtenerPorGet<number>(`TablasComunesCabecera?codigoModulo=${this.codigoModulo}`)
			.subscribe(api => {
				this.tablaComunesCabeceras = api.resultado
				this.ObtenerInformacionAdicional(this.tabSeleccionado)
			}, (error) => {
				this._messageService.showError(error.mensaje || error)
			})
	}

	GuardarBase() {
		const message = this._messageService.showLoading(`Guardando información: INFORMACIÓN ADICIONAL - Codigo persona: ${this.codigoPersona}`)

		this.GuardarCambios()
			.pipe(finalize(() => this._messageService.removeLoading(message)))
			.subscribe(() => {
				this._messageService.showSuccess("Datos guardados correctamente");
			}, (err) => {
				this._messageService.showError(err.mensaje || err);
			});
	}

	async ObtenerInformacionAdicional(index: number) {
		if (this.codigoPersona == null) return;

		if (!this.tablaComunesCabeceras[index]) return;

		const codigoCabecera = this.tablaComunesCabeceras[index].codigo;
		if (this.tablaComunesCabeceras[index].detalles) return;

		try {
			this.cargando.listar = true;
			const data = await this._informacionAdicionalService.obtenerInformacionAdicional(this.codigoPersona, codigoCabecera)
				.toPromise();

			this.tablaComunesCabeceras[index].detalles = []
			data.resultado.map((informacionAdicional) => {
				informacionAdicional.codigoReferencia = this.codigoPersona!;

				informacionAdicional.codigoTabla = codigoCabecera
				this.tablaComunesCabeceras[index].detalles?.push({
					objeto: informacionAdicional,
					accion: informacionAdicional.observacion == null ? AccionesFormulario.Leer : AccionesFormulario.Actualizar
				});
			});
		} catch (err: any) {
			this._messageService.showError(err.mensaje || 'Ocurrió un error inesperado al obtener la información' + JSON.stringify(err))
		} finally {
			this.cargando.listar = false;
		}
	}

	GuardarCambios(): Observable<unknown> {
		if (!this.tieneCambios) return of(0);
		this.cargando.guardar = true;
		const operaciones: Observable<unknown>[] = [];
		this.tablaComunesCabeceras.forEach((tabla) => {
			tabla.detalles?.forEach((elemento) => {
				if (this.codigoModulo){
					elemento.objeto.codigoModulo= this.codigoModulo;
				}

				const informacionAdicional = elemento.objeto;
				if (this.codigoPersona) informacionAdicional.codigoReferencia = parseInt(this.codigoPersona.toString());

				if (elemento.accion == AccionesFormulario.Leer) return;

				if(informacionAdicional.observacion===null ||  informacionAdicional.observacion.trim()===""  ){
					informacionAdicional.observacion = "S/O";
				}

				switch (elemento.accion) {
					case AccionesFormulario.Insertar:
						operaciones.push(this._informacionAdicionalService.insertarInformacionAdicional(<InformacionAdicional>(informacionAdicional)))
						break;
					case AccionesFormulario.Actualizar:
						operaciones.push(this._informacionAdicionalService.actualizarInformacionAdicional(<InformacionAdicional>(informacionAdicional)));
						break;
				}
			})
		});

		return concat(...operaciones)
			.pipe(finalize(() => {
				this.cargando.guardar = false
				this.ObtenerTodo();
			}));
	}

	cambiarEstadoDetalle(index: number, estado: boolean) {
		if (this.tablaComunesCabeceras[this.tabSeleccionado].detalles == null) return;
		const detalle = this.tablaComunesCabeceras[this.tabSeleccionado].detalles![index];

		if (!detalle) return;

		detalle.objeto.estado = estado
		this.tieneCambios = true;

		if (detalle.accion == AccionesFormulario.Leer) detalle.accion = AccionesFormulario.Insertar
	}
}

interface Cabecera extends Catalogo<number> {
	detalles?: ElementoFormulario<InformacionAdicional>[];
}
