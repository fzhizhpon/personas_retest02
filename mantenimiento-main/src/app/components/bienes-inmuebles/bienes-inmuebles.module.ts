import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BienesInmueblesComponent } from './bienes-inmuebles.component';

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
import { VsSelectPaisModule } from 'src/app/components/catalogos/select-pais/vs-select-pais.module';
import { VsSelectParroquiaModule } from 'src/app/components/catalogos/select-parroquia/vs-select-parroquia.module';
import { VsSelectProvinciaModule } from 'src/app/components/catalogos/select-provincia/vs-select-provincia.module';
import { VsSelectCiudadModule } from 'src/app/components/catalogos/select-ciudad/vs-select-ciudad.module';
import { VsSelectLugarModule } from 'src/app/components/catalogos/select-lugar/select-lugar.module';
import {
	VsSelectOperadoraFijaModule
} from 'src/app/components/catalogos/select-operadora-fija/vs-select-operadora-fija.module';
import {
	VsSelectTipoResidenciaModule
} from 'src/app/components/catalogos/select-tipo-residencia/vs-select-tipo-residencia.module';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import {
	VsSelectBienInmuebleModule
} from 'src/app/components/catalogos/select-bien-inmueble/select-bien-inmueble.module';
import { LanguageDirectiveModule } from 'src/app/components/ui/directives/language/language.directive.module';
import { VsMapModule } from 'src/app/components/ui/vs-map/vs-map.module';
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
	NzDatePickerModule,
	NzToolTipModule,
	NzTagModule,
	NzInputNumberModule,
	NzModalModule,
	NzPopconfirmModule,
	TrimDirectiveModule,
];

@NgModule({
	declarations: [BienesInmueblesComponent],
	exports: [BienesInmueblesComponent],
	imports: [
		CommonModule,
		NzModules,
		FormsModule,
		ReactiveFormsModule,
		PanelAccionesModule,
		VsSelectPaisModule,
		VsSelectProvinciaModule,
		VsSelectCiudadModule,
		VsSelectParroquiaModule,
		VsSelectLugarModule,
		VsSelectOperadoraFijaModule,
		VsSelectTipoResidenciaModule,
		VsSelectBienInmuebleModule,
		VsMapModule,
		LanguageDirectiveModule,
	]
})
export class PersBienesInmueblesModule {
}
