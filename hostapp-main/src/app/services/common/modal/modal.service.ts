import { Injectable } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ModalOpts } from './ModalOpts';

@Injectable({
	providedIn: 'root'
})
export class ModalService {

	constructor(
		private modalService: NzModalService,
	) { }

	openModal(Options: ModalOpts): void {
		this.modalService.create({
			nzContent: Options.Component,
			nzComponentParams: {
				Data: Options.Data,
				Callback: Options.Callback
			},
			nzWidth: Options.Width ?? '100%',
			nzClosable: Options.Closable ?? false,
			nzMaskClosable: Options.Closable ?? false,
			nzStyle: {
				'top': '2.5rem',
				'max-width': 'calc(100% - 5rem)',
				'maz-height': 'calc(100vh - 5rem)'
			},
			nzOnOk: () => {
				if(Options.Callback) {
					Options.Callback()
				}
			}
		})
	}

}
