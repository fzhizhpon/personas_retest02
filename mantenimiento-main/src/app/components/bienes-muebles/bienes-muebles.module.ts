import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BienesMueblesComponent } from './bienes-muebles.component';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { PanelAccionesModule } from 'src/app/components/ui/panel-acciones/panel-acciones.module';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { VsSelectBienMuebleModule } from 'src/app/components/catalogos/select-bien-mueble/vs-select-bien-mueble.module';
import { LanguageDirectiveModule } from 'src/app/components/ui/directives/language/language.directive.module';
import { TrimDirectiveModule } from 'src/app/components/ui/directives/trim/trim.directive.module';
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
	NzModalModule,
	NzToolTipModule,
	NzTagModule,
	NzInputNumberModule,
	TrimDirectiveModule,
	NzPopconfirmModule
];

@NgModule({
	declarations: [
		BienesMueblesComponent
	],
	exports: [BienesMueblesComponent],
	imports: [
		CommonModule,
		NzModules,
		FormsModule,
		ReactiveFormsModule,
		PanelAccionesModule,
		VsSelectBienMuebleModule,
		LanguageDirectiveModule,
	]
})
export class PersBienesMueblesModule {
}
