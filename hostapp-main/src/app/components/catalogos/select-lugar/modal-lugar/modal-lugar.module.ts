import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalLugarComponent } from './modal-lugar.component';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { VsTableModule } from 'src/app/components/ui/table/table.module';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
	declarations: [ModalLugarComponent],
	exports: [ModalLugarComponent],
	imports: [
		CommonModule,
		FormsModule,
		ReactiveFormsModule,
		NzInputModule,
		NzModalModule,
		NzButtonModule,
		NzSelectModule,
		NzIconModule,
		VsTableModule,
		NzModalModule,
	]
})
export class ModalLugarModule { }
