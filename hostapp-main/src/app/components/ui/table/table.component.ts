import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
	selector: 'vs-table',
	templateUrl: './table.component.html',
	styleUrls: ['./table.component.scss'],
})
export class TableComponent {

	@Input('showPagination') showPagination = true;
	@Input('loading') showLoading = false;
	@Input('data') data!: any[];

	@Input('index') index = 0;
	@Input('totalPages') totalPages = 0;
	@Output('indexChange') indexEmitter: EventEmitter<number> = new EventEmitter<number>();

	// constructor() { }

	changeIndex(index: number) {
		if(index != this.index) {
			this.index = index
			this.indexEmitter.emit(index)
		}
	}

}
