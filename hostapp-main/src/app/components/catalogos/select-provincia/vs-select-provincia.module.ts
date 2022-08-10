import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { VsSelectProvinciaComponent } from './vs-select-provincia.component';
import { FormsModule } from '@angular/forms';
import { NzMessageModule } from 'ng-zorro-antd/message';


@NgModule({
	declarations: [VsSelectProvinciaComponent],
	exports: [VsSelectProvinciaComponent],
	imports: [
		CommonModule,
		FormsModule,
		NzSelectModule,
		NzMessageModule,
	]
})
export class VsSelectProvinciaModule { }
