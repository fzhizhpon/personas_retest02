import { CommonModule } from '@angular/common';
import { DatosGeneralesComponent } from './datos-generales.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { PanelAccionesModule } from 'src/app/components/ui/panel-acciones/panel-acciones.module';
import { VsSelectEstadoCivilModule } from 'src/app/components/catalogos/select-estado-civil/vs-select-estado-civil.module';
import { VsSelectLugarModule } from 'src/app/components/catalogos/select-lugar/select-lugar.module';
import { VsSelectPersonaModule } from '../select-persona/select-persona.module';
import { VsSelectProfesionModule } from 'src/app/components/catalogos/select-profesion/vs-select-profesion.module';
import { VsSelectTipoContribuyenteModule } from 'src/app/components/catalogos/select-tipo-contribuyente/vs-select-tipo-contribuyente.module';
import { VsSelectTipoDiscapacidadModule } from 'src/app/components/catalogos/select-tipo-discapacidad/vs-select-tipo-discapacidad.module';
import { VsSelectTipoEtniaModule } from 'src/app/components/catalogos/select-tipo-etnia/vs-select-tipo-etnia.module';
import { VsSelectTipoIdentificacionModule } from 'src/app/components/catalogos/select-tipo-identificacion/vs-select-tipo-identificacion.module';
import { VsSelectTipoPersonaModule } from 'src/app/components/catalogos/select-tipo-persona/vs-select-tipo-persona.module';
import { VsSelectTipoSangreModule } from 'src/app/components/catalogos/select-tipo-sangre/vs-select-tipo-sangre.module';
import { VsSelectTipoSociedadModule } from 'src/app/components/catalogos/select-tipo-sociedad/vs-select-tipo-sociedad.module';
import { VsSelectSexoModule } from 'src/app/components/catalogos/select-sexo/vs-select-sexo.module';
import { LanguageDirectiveModule } from 'src/app/components/ui/directives/language/language.directive.module';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { TrimDirectiveModule } from 'src/app/components/ui/directives/trim/trim.directive.module';
import { VsDocumentViewerModule } from '../digitalizacion-documentos/document-viewer/document-viewer.module';


const NzModules = [
	NzButtonModule,
	NzInputModule,
	NzRadioModule,
	NzCheckboxModule,
	NzIconModule,
	NzFormModule,
	NzDatePickerModule,
	NzInputNumberModule,
	NzModalModule,
	NzSpinModule,
	NzToolTipModule,
];

@NgModule({
	declarations: [
		DatosGeneralesComponent
	],
	exports: [DatosGeneralesComponent],
	imports: [
		CommonModule,
		NzModules,
		FormsModule,
		ReactiveFormsModule,
		PanelAccionesModule,
		VsSelectTipoPersonaModule,
		VsSelectTipoIdentificacionModule,
		VsSelectTipoEtniaModule,
		VsSelectTipoSangreModule,
		VsSelectProfesionModule,
		VsSelectEstadoCivilModule,
		VsSelectSexoModule,
		VsSelectLugarModule,
		VsSelectTipoContribuyenteModule,
		VsSelectTipoDiscapacidadModule,
		VsSelectPersonaModule,
		VsSelectTipoSociedadModule,
		LanguageDirectiveModule,
		TrimDirectiveModule,
		VsDocumentViewerModule,
	]
})
export class PersDatosGeneralesModule { }
