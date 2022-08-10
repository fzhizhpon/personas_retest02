import {Component, EventEmitter, forwardRef, Input, Output} from '@angular/core';
import {ControlValueAccessor, NG_VALUE_ACCESSOR} from '@angular/forms';
import {DataBinder} from 'src/app/components/common/DataBinder';
import {finalize} from 'rxjs/operators';
import {NzModalService} from 'ng-zorro-antd/modal';
import {BienesInmueblesComponent} from '../bienes-inmuebles/bienes-inmuebles.component';
import {MessageService} from 'src/app/services/common/message/message.service';
import {BienesInmueblesService} from '../../services/bienes-inmuebles.service';
import {ModalBuscarBienInmuebleService} from '../modal-buscar-bien-inmueble/modal-buscar-bien-inmueble.service';
import {BienInmuebleResponse} from '../../models/bien-inmueble';

@Component({
	selector: 'vs-select-bienes-inmuebles',
	templateUrl: './select-bien-inmueble.component.html',
	styleUrls: ['./select-bien-inmueble.component.scss'],
	providers: [{
		provide: NG_VALUE_ACCESSOR,
		multi: true,
		useExisting: forwardRef(() => VsSelectBienInmuebleComponent)
	}]

})
export class VsSelectBienInmuebleComponent extends DataBinder<number> implements ControlValueAccessor {

	@Input('placeholder') placeholder = "Seleccione un bien inmueble";

	_codigoPersona!: number
	@Input('codigoPersona')
	set codigoPersona(value: number | null) {
		if (value) {
			this._codigoPersona = value;
		}
	}

	@Output('bienInmuebleChange') bienInmueble = new EventEmitter<BienInmuebleResponse>();

	@Input() personaBienImueble?: any | null;

	callePrincipal: string | null = '';
	lugarBien: string | null = '';

	constructor(
		private _modalBienInmueble: ModalBuscarBienInmuebleService,
		private _bienesInmueblesService: BienesInmueblesService,
		private _modalService: NzModalService,
		private _messageService: MessageService,
	) {
		super();
		this.isLoading = false;

	}

	async abrirModalBusqueda() {
		if (this.isDisabled) return;
		const BienInmueble = await this._modalBienInmueble.abrirBusqueda(this._codigoPersona);
		this._bienesInmueblesService.obtenerBienInmueble(this._codigoPersona, BienInmueble.numeroRegistro)
			.pipe(finalize(() => null))
			.subscribe(
				data => {
					// * visualizar informaicon del bien  seleccionado
					const {callePrincipal, codigoPais, codigoProvincia, codigoCiudad, codigoParroquia} = data.resultado;
					this.callePrincipal = callePrincipal;
					this.lugarBien = `${codigoPais}-${codigoProvincia}-${codigoCiudad}-${codigoParroquia}`;

					this.bienInmueble.emit(data.resultado);
				},
				error => {
					this._messageService.showError(error.mensaje || error)
				}
			);
		this.bienInmueble.emit(BienInmueble);

	}

	changeCallback(): void {
		this.isLoading = true;
		this._bienesInmueblesService.obtenerBienInmueble(this.value, this.value)
			.pipe(finalize(() => this.isLoading = false))
			.subscribe(api => {
				if (api.resultado.nombre == null) {
					this.callePrincipal = null
				} else {
					this.callePrincipal = api.resultado.nombre
				}
			}, (error) => {
				this._messageService.showError(error.mensaje || error);
			})
	}

	nullSetCallback(): void {
		this.callePrincipal = null;
	}

	abrirModalCrearBienInmueble() {
		let parametrosComponentes = null;
		parametrosComponentes = {
			mostrarGuardar: false,
			_codigoPersona: this._codigoPersona,
			mostrarListar: false
		}
		let codigoPersona: number;
		const modal = this._modalService.create({
			nzContent: BienesInmueblesComponent,
			nzWidth: '140rem',
			nzTitle: 'Crear',
			nzComponentParams: parametrosComponentes,
			nzOkText: 'Guardar',
			nzOnOk: async (componente) => {
				modal.updateConfig({nzOkLoading: true})

				try {
					await componente.GuardarNuevoRegistro()
				} catch (error: any) {
					this._messageService.showError(error?.mensaje || error);
					return false;
				} finally {
					modal.updateConfig({nzOkLoading: false});
				}
			}
		});

		modal.afterClose.subscribe(() => {
			if (codigoPersona != null) this.writeValue(codigoPersona)
		})
	}

	abrirModalVerBienInmueble() {
		if (this.value == null) return;

		const modal = this._modalService.create({
			nzContent: BienesInmueblesComponent,
			nzWidth: '140rem',
			nzTitle: 'Bien Inmueble',
			nzComponentParams: {
				mostrarGuardar: false,
				_codigoPersona: this.value,
			},
		});

		modal.afterClose.subscribe((value) => {
			this._bienesInmueblesService.obtenerBienInmueble(value.codigoPersona, value.numeroRegistro)
				.pipe(finalize(() => null))
				.subscribe(
					data => {
						this.bienInmueble.emit(value.resultado);

					},
					error => {
						this._messageService.showError(error.mensaje || error)
					}
				);

			this.bienInmueble.emit(value);
		});
	}


}
