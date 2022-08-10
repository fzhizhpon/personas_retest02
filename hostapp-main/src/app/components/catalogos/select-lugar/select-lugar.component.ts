import { Component, forwardRef, Input } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { NzModalService } from 'ng-zorro-antd/modal';
import { finalize } from 'rxjs/operators';
import { CatalogoService } from 'src/app/services/api/catalogos/catalogo.service';
import { DataBinder } from '../../common/DataBinder';
import { Lugar } from '../catalogo';
import { ModalLugarComponent } from './modal-lugar/modal-lugar.component';

@Component({
	selector: 'vs-select-lugar',
	templateUrl: './select-lugar.component.html',
	styleUrls: ['./select-lugar.component.scss'],
	providers: [{
		provide: NG_VALUE_ACCESSOR,
		multi: true,
		useExisting: forwardRef(() => VsSelectLugarComponent)
	}]
})
export class VsSelectLugarComponent extends DataBinder<LugarParam> implements ControlValueAccessor {

	@Input('placeholder') placeholder = "Seleccione una opción";

	nombreLugar: string | null = '';

	constructor(
		private _modalService: NzModalService,
		private _catalogoService: CatalogoService,
	) {
		super();

		this.isLoading = false;
	}

	abrirModalBusqueda() {
		if (this.isDisabled) return;

		const modal = this._modalService.create({
			nzContent: ModalLugarComponent,
			nzCentered: true,
			nzFooter: null,
			nzWidth: '85rem',
			nzStyle: {
				'max-width': 'calc(100% - 5rem)',
				'maz-height': 'calc(100vh - 5rem)'
			}
		})

		modal.afterClose.subscribe((lugar: Lugar) => {
			if(lugar != null) {
				this.nombreLugar = lugar.descripcionLugar;

				this.writeValue({
					codigoPais: lugar.codigoPais,
					codigoProvincia: lugar.codigoProvincia,
					codigoCiudad: lugar.codigoCiudad,
					codigoParroquia: lugar.codigoParroquia
				})
			}
		})
	}

	changeCallback(): void {
		if (this.value != null) {
			this.isLoading = true;

			this._catalogoService.obtenerLugares(this.value)
			.pipe(finalize(() => this.isLoading = false))
			.subscribe(api => {
				if (api.resultado.length == 0) {
					this.nombreLugar = null
				} else {
					this.nombreLugar = api.resultado[0].descripcionLugar
				}
			})
		}
	}

}

export interface LugarParam {
	codigoPais: number;
	codigoProvincia: number;
	codigoCiudad: number;
	codigoParroquia: number;
}
