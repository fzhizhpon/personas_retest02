import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FamiliaresComponent } from './familiares.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { VsSelectParentescoModule } from 'src/app/components/catalogos/select-parentesco/vs-select-parentesco.module';
import { VsSelectPersonaModule } from '../select-persona/select-persona.module';
import { NzFormModule } from 'ng-zorro-antd/form';
import { VsTableModule } from 'src/app/components/ui/table/table.module';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import {LanguageDirectiveModule} from 'src/app/components/ui/directives/language/language.directive.module';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';

@NgModule({
	declarations: [FamiliaresComponent],
	exports: [FamiliaresComponent],
	imports: [
		CommonModule,
		FormsModule,
		NzFormModule,
		ReactiveFormsModule,
		VsSelectParentescoModule,
		VsSelectPersonaModule,
		VsTableModule,
		NzButtonModule,
		NzCheckboxModule,
		NzInputModule,
		NzButtonModule,
		NzInputModule,
		NzIconModule,
		NzToolTipModule,
		NzPopconfirmModule,
		LanguageDirectiveModule
	]
})
export class PersFamiliaresModule { }
