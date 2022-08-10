import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { VsSelectPaisComponent } from './vs-select-pais.component';
import { FormsModule } from '@angular/forms';
import { NzMessageModule } from 'ng-zorro-antd/message';


@NgModule({
	declarations: [VsSelectPaisComponent],
	exports: [VsSelectPaisComponent],
	imports: [
		CommonModule,
		FormsModule,
		NzSelectModule,
		NzMessageModule,
	]
})
export class VsSelectPaisModule { }
