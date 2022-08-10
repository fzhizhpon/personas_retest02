import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ReferenciaPersonalComponent} from './referencia-personal.component';

import {NzButtonModule} from 'ng-zorro-antd/button';
import {NzSelectModule} from 'ng-zorro-antd/select';
import {NzInputModule} from 'ng-zorro-antd/input';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {NzFormModule} from 'ng-zorro-antd/form';
import {NzTableModule} from 'ng-zorro-antd/table';
import {NzIconModule} from 'ng-zorro-antd/icon';
import {VsTableModule} from 'src/app/components/ui/table/table.module';
import {NzDatePickerModule} from 'ng-zorro-antd/date-picker';
import {NzModalModule} from 'ng-zorro-antd/modal';
import {LanguageDirectiveModule} from 'src/app/components/ui/directives/language/language.directive.module';
import {NzToolTipModule} from 'ng-zorro-antd/tooltip';
import {VsSelectPersonaModule} from "../select-persona/select-persona.module";
import {TrimDirectiveModule} from 'src/app/components/ui/directives/trim/trim.directive.module';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';


const NzModules = [
	NzButtonModule,
	NzInputModule,
	NzSelectModule,
	NzFormModule,
	NzTableModule,
	NzIconModule,
	NzDatePickerModule,
	NzModalModule,
	LanguageDirectiveModule,
	NzToolTipModule,
	NzPopconfirmModule,
];

@NgModule({
	declarations: [
		ReferenciaPersonalComponent
	],
	exports: [ReferenciaPersonalComponent],
	imports: [
		CommonModule,
		NzModules,
		FormsModule,
		ReactiveFormsModule,
		VsTableModule,
		VsSelectPersonaModule,
		TrimDirectiveModule
	]
})
export class PersReferenciaPersonalModule {
}
