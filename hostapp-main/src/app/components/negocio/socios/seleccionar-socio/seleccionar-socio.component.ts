import { Component, forwardRef, Input } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { NzModalService } from 'ng-zorro-antd/modal';
import { finalize } from 'rxjs/operators';
import { DataBinder } from 'src/app/components/common/DataBinder';

@Component({
	selector: 'app-seleccionar-socio',
	templateUrl: './seleccionar-socio.component.html',
	styleUrls: ['./seleccionar-socio.component.scss'],
	providers: [{
		provide: NG_VALUE_ACCESSOR,
		multi: true,
		useExisting: forwardRef(() => SeleccionarSocioComponent)
	}]
})
export class SeleccionarSocioComponent extends DataBinder<string> {

	@Input('mostrarBusqueda') mostrarBusqueda = true;

	loading = false;
	// eslint-disable-next-line @typescript-eslint/no-explicit-any
	modalBusqueda!: any;

	constructor(
		private _modalService: NzModalService,
	) {
		super();
	}

	async abrirModalBusqueda() {
		this.loading = true

		if(this.modalBusqueda == null) {
			console.log('Importing module...')
			const { ModalBuscarSocioComponent } = await import('../modal-buscar-socio/modal-buscar-socio.component');
			this.modalBusqueda = ModalBuscarSocioComponent
		}

		const modal = this._modalService.create({
			nzContent: this.modalBusqueda,
			nzStyle: {
				'width': '100%',
				'max-width': '100rem',
			},
		})

		modal.afterClose
		.pipe(finalize(() => this.loading = false))
		.subscribe(codigo => {
			if(codigo != null && codigo != undefined) {
				this.change(codigo)
			}
		})
	}

}
