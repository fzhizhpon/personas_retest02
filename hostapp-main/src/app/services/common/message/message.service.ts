import { Injectable } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';

@Injectable({
	providedIn: 'root'
})
export class MessageService {

	constructor(
		private message: NzMessageService,
	) { }

	show(type: string, content: string, duration: number): void {
		this.message.create(type, content, {
			nzDuration: duration
		});
	}

	showError(content: string, duration = 2500): void {
		this.show('error', content, duration);
	}

	showSuccess(content: string, duration = 2500): void {
		this.show('success', content, duration);
	}

	showInfo(content: string, duration = 2500): void {
		this.show('info', content, duration);
	}

	showLoading(content: string): string {
		const id = this.message.loading(content, { nzDuration: 0 }).messageId;
		return id;
	}

	removeLoading(id: string): void {
		try {
			this.message.remove(id)
		} catch (ex) { console.error(`[EXCEPCION ELIMINANDO LOADING]: ${ex}`) }
	}

}
