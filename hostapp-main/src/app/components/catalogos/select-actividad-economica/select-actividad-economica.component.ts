import { CatalogoService } from 'src/app/services/api/catalogos/catalogo.service';
import { Component, forwardRef, Input } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { DataBinder } from '../../common/DataBinder';
import { finalize } from 'rxjs/operators';
import { ModalActividadEconomicaComponent } from './modal-actividad-economica/modal-actividad-economica.component';
import { NzModalService } from 'ng-zorro-antd/modal';

@Component({
	selector: 'vs-select-actividad-economica',
	templateUrl: './select-actividad-economica.component.html',
	styleUrls: ['./select-actividad-economica.component.scss'],
	providers: [{
		provide: NG_VALUE_ACCESSOR,
		multi: true,
		useExisting: forwardRef(() => VsSelectActividadEconomicaComponent)
	}]
})
export class VsSelectActividadEconomicaComponent extends DataBinder<string> implements ControlValueAccessor {

	@Input('placeholder') placeholder = "Seleccione una opción";

	nombreActividad: string | null = '';

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
			nzContent: ModalActividadEconomicaComponent,
			nzCentered: true,
			nzFooter: null,
			nzWidth: '85rem',
			nzStyle: {
				'max-width': 'calc(100% - 5rem)',
				'maz-height': 'calc(100vh - 5rem)'
			}
		})

		modal.afterClose.subscribe((actividad: any) => {
			if(actividad != null) {
				this.nombreActividad = actividad.descripcion;

				this.writeValue(actividad.codigo)
			}
		})
	}

	changeCallback(): void {
		if (this.value != null) {
			this.isLoading = true;

			this._catalogoService.obtenerPorGet<number>('ActividadesEconomicas', {codigo: this.value})
			.pipe(finalize(() => this.isLoading = false))
			.subscribe(api => {
				if (api.resultado.length == 0) {
					this.nombreActividad = null
				} else {
					this.nombreActividad = api.resultado[0].descripcion
				}
			})
		}
	}

}
