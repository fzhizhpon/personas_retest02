import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {NzButtonModule} from 'ng-zorro-antd/button';
import {NzCheckboxModule} from 'ng-zorro-antd/checkbox';
import {NzDatePickerModule} from 'ng-zorro-antd/date-picker';
import {NzFormModule} from 'ng-zorro-antd/form';
import {NzIconModule} from 'ng-zorro-antd/icon';
import {NzInputModule} from 'ng-zorro-antd/input';
import {NzInputNumberModule} from 'ng-zorro-antd/input-number';
import {NzRadioModule} from 'ng-zorro-antd/radio';
import {NzSelectModule} from 'ng-zorro-antd/select';
import {NzTableModule} from 'ng-zorro-antd/table';
import {NzAlertModule} from 'ng-zorro-antd/alert';
import {NzTagModule} from 'ng-zorro-antd/tag';
import {PanelAccionesModule} from 'src/app/components/ui/panel-acciones/panel-acciones.module';
import {TrabajosComponent} from './trabajos.component';
import {
	VsSelectActividadEconomicaModule
} from 'src/app/components/catalogos/select-actividad-economica/select-actividad-economica.module';
import {VsSelectTipoTiempoModule} from 'src/app/components/catalogos/select-tipo-tiempo/vs-select-tipo-tiempo.module';
import {
	VsSelectCategoriaTrabajoModule
} from 'src/app/components/catalogos/select-categoria-trabajo/vs-select-categoria-trabajo.module';
import {VsTableModule} from 'src/app/components/ui/table/table.module';
import {VsSelectLugarModule} from 'src/app/components/catalogos/select-lugar/select-lugar.module';
import {LanguageDirectiveModule} from 'src/app/components/ui/directives/language/language.directive.module';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import {TrimDirectiveModule} from 'src/app/components/ui/directives/trim/trim.directive.module';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';

const NzModules = [
	NzButtonModule,
	NzInputModule,
	NzSelectModule,
	NzRadioModule,
	NzCheckboxModule,
	NzIconModule,
	NzFormModule,
	NzTableModule,
	NzDatePickerModule,
	NzInputNumberModule,
	NzAlertModule,
	NzTagModule,
	LanguageDirectiveModule,
	NzToolTipModule,
	TrimDirectiveModule
];

@NgModule({
	declarations: [TrabajosComponent],
	exports: [TrabajosComponent],
	imports: [
		CommonModule,
		NzModules,
		FormsModule,
		ReactiveFormsModule,
		PanelAccionesModule,
		VsSelectCategoriaTrabajoModule,
		VsSelectTipoTiempoModule,
		VsTableModule,
		VsSelectActividadEconomicaModule,
		VsSelectLugarModule,
		NzPopconfirmModule,
	]
})
export class PersTrabajosModule {
}
