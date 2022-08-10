import { Component, Input, OnInit } from "@angular/core";
import { NzDrawerRef } from "ng-zorro-antd/drawer";

@Component({
	template: ''
})

export default class BaseDrawer implements OnInit {

	@Input() cancelCallback: Function;
	@Input() okCallback: Function;

	isLoading: boolean = true;
	isLoadingSave: boolean = false;

	constructor(
		public drawerService: NzDrawerRef,
	) {
	}

	ngOnInit(): void {
		this.loadData()
	}

	loadData(): void {}

	closeDrawer(status: 'ok'|'cancel', data?: any): void {
		if(status == 'ok') {
			this.okCallback()
		}

		this.drawerService.close(data)
	}

	closeWindow() {
		if(this.cancelCallback) {
			this.cancelCallback();
			return;
		}

		this.closeDrawer('cancel')
	}
}
