import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DireccionComponent } from './direccion.component';

import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { NzInputModule } from 'ng-zorro-antd/input';
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
import { VsSelectOperadoraFijaModule } from 'src/app/components/catalogos/select-operadora-fija/vs-select-operadora-fija.module';
import { VsSelectTipoResidenciaModule } from 'src/app/components/catalogos/select-tipo-residencia/vs-select-tipo-residencia.module';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { VsSelectBienInmuebleModule } from '../select-bien-inmueble/select-bien-inmueble.module';
import { PersBienesInmueblesModule } from '../bienes-inmuebles/bienes-inmuebles.module';
import { LanguageDirectiveModule } from 'src/app/components/ui/directives/language/language.directive.module';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { TrimDirectiveModule } from 'src/app/components/ui/directives/trim/trim.directive.module';
import { VsMapModule } from 'src/app/components/ui/vs-map/vs-map.module';
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
	NzDatePickerModule,
	NzInputNumberModule,
	NzToolTipModule,
	NzTagModule,
	NzSelectModule,
	NzSelectModule,
	NzPopconfirmModule,
];

const VsModules = [
	VsSelectPaisModule,
	VsSelectProvinciaModule,
	VsSelectCiudadModule,
	VsSelectParroquiaModule,
	VsSelectLugarModule,
	VsSelectOperadoraFijaModule,
	VsSelectTipoResidenciaModule,
	VsMapModule,
	VsSelectOperadoraFijaModule,
	VsSelectBienInmuebleModule,
];

@NgModule({
	declarations: [
		DireccionComponent
	],
	exports: [DireccionComponent],
	imports: [
		CommonModule,
		NzModules,
		VsModules,
		FormsModule,
		ReactiveFormsModule,
		PanelAccionesModule,
		FormsModule,
		PersBienesInmueblesModule,
		LanguageDirectiveModule,
		TrimDirectiveModule,
	]
})
export class PersDireccionModule {
}
