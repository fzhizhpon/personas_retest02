import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { VsSelectTipoRepresentanteComponent } from './vs-select-tipo-representante.component';
import { FormsModule } from '@angular/forms';
import { NzMessageModule } from 'ng-zorro-antd/message';


@NgModule({
	declarations: [VsSelectTipoRepresentanteComponent],
	exports: [VsSelectTipoRepresentanteComponent],
	imports: [
		CommonModule,
		FormsModule,
		NzSelectModule,
		NzMessageModule,
	]
})
export class VsSelectTipoRepresentanteModule { }
