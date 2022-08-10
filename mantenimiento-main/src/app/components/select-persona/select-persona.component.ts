import {Component, forwardRef, Input} from '@angular/core';
import {ControlValueAccessor, NG_VALUE_ACCESSOR} from '@angular/forms';
import {DataBinder} from 'src/app/components/common/DataBinder';
import {finalize} from 'rxjs/operators';
import {ModalBuscarPersonaService} from '../modal-buscar-persona/modal-buscar-persona.service';
import {PersonaService} from '../../services/persona.service';
import {NzModalService} from 'ng-zorro-antd/modal';
import {DatosGeneralesComponent} from '../datos-generales/datos-generales.component';
import {MessageService} from 'src/app/services/common/message/message.service';
import {environmentHost} from 'src/environments/environment';

@Component({
	selector: 'vs-select-persona',
	templateUrl: './select-persona.component.html',
	styleUrls: ['./select-persona.component.scss'],
	providers: [{
		provide: NG_VALUE_ACCESSOR,
		multi: true,
		useExisting: forwardRef(() => VsSelectPersonaComponent)
	}]
})
export class VsSelectPersonaComponent extends DataBinder<number> implements ControlValueAccessor {

	@Input('placeholder') placeholder = "Click para buscar persona";

	@Input() personaDatos ?: any | null;

	@Input('esConyugue') esConyugue!: boolean;

	@Input('habilitarCrearPersona') habilitarCrearPersona: boolean = true;

	nombrePersona: string | null = '';

	constructor(
		private _modalPersona: ModalBuscarPersonaService,
		private _personaService: PersonaService,
		private _modalService: NzModalService,
		private _messageService: MessageService,
	) {
		super();

		this.isLoading = false;
	}

	async abrirModalBusqueda() {
		if (this.isDisabled) return;

		const persona = await this._modalPersona.abrirBusqueda();
		this.writeValue(persona.codigoPersona)
		this.nombrePersona = persona.nombre
	}

	changeCallback(): void {
		this.isLoading = true;

		this._personaService.ObtenerPersonaMin(this.value)
			.pipe(finalize(() => this.isLoading = false))
			.subscribe(api => {
				if (api.resultado.nombre == null) {
					this.nombrePersona = null
				} else {
					this.nombrePersona = api.resultado.nombre
				}
			}, (error) => {
				this._messageService.showError(error.mensaje || error);
			})
	}

	nullSetCallback(): void {
		this.nombrePersona = null;
	}

	abrirModalCrearPersona() {
		let parametrosComponentes = null;
		if (this.personaDatos && this.esConyugue) {
			parametrosComponentes = {
				mostrarGuardar: true,
				esConyugue: this.esConyugue
			}
		} else {
			parametrosComponentes = {
				mostrarGuardar: false
			}
		}
		let codigoPersona: number;
		const modal = this._modalService.create({
			nzContent: DatosGeneralesComponent,
			nzWidth: '140rem',
			nzTitle: 'Crear nueva persona',
			nzComponentParams: parametrosComponentes,
			nzOkText: 'Crear persona',
			nzOnOk: async (componente) => {
				modal.updateConfig({nzOkLoading: true})

				try {
					const resp = await componente.GuardarCambios().toPromise()

					if (resp.resultado) {
						codigoPersona = resp.resultado
						modal.close()
					}
				} catch (error: any) {
					this._messageService.showError(error?.mensaje || error)
					return false;
				} finally {
					modal.updateConfig({nzOkLoading: false})
				}
			}
		});

		modal.afterClose.subscribe(() => {
			if (codigoPersona != null) this.writeValue(codigoPersona)
		}, (error) => {
			this._messageService.showError(error.mensaje || error);
		})
	}

	abrirModalVerPersona() {
		if (this.value == null) return;

		this._modalService.create({
			nzContent: DatosGeneralesComponent,
			nzWidth: '140rem',
			nzTitle: 'Datos generales',
			nzComponentParams: {
				mostrarGuardar: false,
				_codigoPersona: this.value
			},
			nzOkText: 'Más información',
			nzCancelText: 'Cerrar ventana',
			nzOnOk: () => {
				window.open(`${environmentHost.baseAppUrl}/mantenimiento/ingresosocios-mock?codigo=${this.value}`);
			}
		});
	}

}
