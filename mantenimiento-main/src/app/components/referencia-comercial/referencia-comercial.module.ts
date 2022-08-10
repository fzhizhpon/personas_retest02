import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ReferenciaComercialComponent} from './referencia-comercial.component';

import {NzButtonModule} from 'ng-zorro-antd/button';
import {NzSelectModule} from 'ng-zorro-antd/select';
import {NzInputModule} from 'ng-zorro-antd/input';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {NzFormModule} from 'ng-zorro-antd/form';
import {NzIconModule} from 'ng-zorro-antd/icon';
import {VsSelectTipoTiempoModule} from 'src/app/components/catalogos/select-tipo-tiempo/vs-select-tipo-tiempo.module';
import {VsTableModule} from 'src/app/components/ui/table/table.module';
import {NzDatePickerModule} from 'ng-zorro-antd/date-picker';
import {VsSelectLugarModule} from 'src/app/components/catalogos/select-lugar/select-lugar.module';
import {LanguageDirectiveModule} from 'src/app/components/ui/directives/language/language.directive.module';
import {NzToolTipModule} from 'ng-zorro-antd/tooltip';
import {TrimDirectiveModule} from 'src/app/components/ui/directives/trim/trim.directive.module';
import {NzInputNumberModule} from 'ng-zorro-antd/input-number';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';

const NzModules = [
	NzButtonModule,
	NzInputModule,
	NzSelectModule,
	NzFormModule,
	NzIconModule,
	NzDatePickerModule,
	LanguageDirectiveModule,
	NzToolTipModule,
	TrimDirectiveModule,
	NzInputNumberModule,
	NzPopconfirmModule
];

@NgModule({
	declarations: [
		ReferenciaComercialComponent
	],
	exports: [ReferenciaComercialComponent],
	imports: [
		CommonModule,
		NzModules,
		FormsModule,
		ReactiveFormsModule,
		VsSelectTipoTiempoModule,
		VsSelectLugarModule,
		VsTableModule,
		TrimDirectiveModule
	]
})
export class PersReferenciaComercialModule {
}
