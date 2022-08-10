import { Injectable } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';
import { BienInmuebleResponse } from '../../models/bien-inmueble';
import { ModalBuscarBienInmuebleComponent } from './modal-buscar-bien-inmueble.component';

@Injectable({
	providedIn: 'root'
})
export class ModalBuscarBienInmuebleService {

	constructor(
		private _modalService: NzModalService,
	) { }

	abrirBusqueda(codigoPersona:number): Promise<BienInmuebleResponse> {
		return new Promise((resolve, reject) => {
			const modal = this._modalService.create({
				nzContent: ModalBuscarBienInmuebleComponent,
				nzCentered: true,
				nzFooter: null,
				nzWidth: '85rem',
				nzComponentParams: {
					codigoPersona 
				  },
				nzStyle: {
					'max-width': 'calc(100% - 5rem)',
					'maz-height': 'calc(100vh - 5rem)'
				}
			})

			modal.afterClose.subscribe((data: any) => {
				resolve(<BienInmuebleResponse>(data))
			})

		});
	}

}
