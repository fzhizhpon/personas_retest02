import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VsSelectLugarComponent } from './select-lugar.component';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { ModalLugarModule } from './modal-lugar/modal-lugar.module';
import { NzIconModule } from 'ng-zorro-antd/icon';

@NgModule({
	declarations: [VsSelectLugarComponent],
	exports: [VsSelectLugarComponent],
	imports: [
		CommonModule,
		NzInputModule,
		NzButtonModule,
		ModalLugarModule,
		NzIconModule,
	],
})
export class VsSelectLugarModule { }
