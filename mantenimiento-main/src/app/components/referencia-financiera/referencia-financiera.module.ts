import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ReferenciaFinancieraComponent} from './referencia-financiera.component';

import {NzButtonModule} from 'ng-zorro-antd/button';
import {NzSelectModule} from 'ng-zorro-antd/select';
import {NzInputModule} from 'ng-zorro-antd/input';
import {NzInputNumberModule} from 'ng-zorro-antd/input-number';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {NzFormModule} from 'ng-zorro-antd/form';
import {NzTableModule} from 'ng-zorro-antd/table';
import {NzIconModule} from 'ng-zorro-antd/icon';
import {
	VsSelectTipoCuentaFinanModule
} from 'src/app/components/catalogos/select-tipo-cuenta-finan/vs-select-tipo-cuenta-finan.module';
import {
	VsSelectTipoInstitucionFinancieraModule
} from 'src/app/components/catalogos/select-tipo-institucion-financiera/vs-select-tipo-institucion-financiera.module';
import {
	VsSelectInstitucionFinancieraModule
} from 'src/app/components/catalogos/select-institucion-financiera/vs-select-institucion-financiera.module';

import {VsTableModule} from 'src/app/components/ui/table/table.module';
import {NzToolTipModule} from 'ng-zorro-antd/tooltip';
import {NzDatePickerModule} from 'ng-zorro-antd/date-picker';
import {LanguageDirectiveModule} from 'src/app/components/ui/directives/language/language.directive.module';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';


const NzModules = [
	NzButtonModule,
	NzInputModule,
	NzSelectModule,
	NzFormModule,
	NzTableModule,
	NzIconModule,
	NzToolTipModule,
	NzDatePickerModule,
	NzInputNumberModule,
	NzPopconfirmModule,
	LanguageDirectiveModule
];

@NgModule({
	declarations: [
		ReferenciaFinancieraComponent
	],
	exports: [ReferenciaFinancieraComponent],
	imports: [
		CommonModule,
		NzModules,
		FormsModule,
		ReactiveFormsModule,
		VsSelectTipoCuentaFinanModule,
		VsSelectTipoInstitucionFinancieraModule,
		VsSelectInstitucionFinancieraModule,
		VsTableModule,
	]
})
export class PersReferenciaFinancieraModule {
}
