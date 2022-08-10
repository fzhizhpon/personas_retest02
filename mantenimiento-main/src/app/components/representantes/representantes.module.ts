import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RepresentantesComponent } from './representantes.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { VsSelectPersonaModule } from '../select-persona/select-persona.module';
import { VsTableModule } from 'src/app/components/ui/table/table.module';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { VsSelectTipoRepresentanteModule } from 'src/app/components/catalogos/select-tipo-representante/vs-select-tipo-representante.module';
import {LanguageDirectiveModule} from 'src/app/components/ui/directives/language/language.directive.module';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';


@NgModule({
	declarations: [RepresentantesComponent],
	exports: [RepresentantesComponent],
	imports: [
		CommonModule,
		FormsModule,
		ReactiveFormsModule,
		VsSelectPersonaModule,
		VsTableModule,
		NzFormModule,
		NzIconModule,
		NzToolTipModule,
		NzButtonModule,
		NzTagModule,
		NzInputModule,
		NzCheckboxModule,
		NzPopconfirmModule,
		VsSelectTipoRepresentanteModule,
		LanguageDirectiveModule
	]
})
export class PersRepresentantesModule { }
