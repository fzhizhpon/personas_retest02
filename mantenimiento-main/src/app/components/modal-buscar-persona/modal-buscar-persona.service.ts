import { Injectable } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';
import { PersonaBusquedaDto } from '../../models/persona';
import { ModalBuscarPersonaComponent } from './modal-buscar-persona.component';

@Injectable({
	providedIn: 'root'
})
export class ModalBuscarPersonaService {

	constructor(
		private _modalService: NzModalService,
	) { }

	abrirBusqueda(): Promise<PersonaBusquedaDto> {
		return new Promise((resolve, reject) => {
			
			const modal = this._modalService.create({
				nzContent: ModalBuscarPersonaComponent,
				nzCentered: true,
				nzFooter: null,
				nzWidth: '85rem',
				nzStyle: {
					'max-width': 'calc(100% - 5rem)',
					'maz-height': 'calc(100vh - 5rem)'
				}
			})

			modal.afterClose.subscribe((data: any) => {
				resolve(<PersonaBusquedaDto>(data))
			})

		});
	}

}
