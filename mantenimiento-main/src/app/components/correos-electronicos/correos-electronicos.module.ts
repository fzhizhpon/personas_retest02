import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CorreosElectronicosComponent } from './correos-electronicos.component';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzSwitchModule } from 'ng-zorro-antd/switch';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import {LanguageDirectiveModule} from 'src/app/components/ui/directives/language/language.directive.module';
import {TrimDirectiveModule} from 'src/app/components/ui/directives/trim/trim.directive.module';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzTableModule } from 'ng-zorro-antd/table';


@NgModule({
	declarations: [CorreosElectronicosComponent],
	exports: [CorreosElectronicosComponent],
	imports: [
		CommonModule,
		FormsModule,
		ReactiveFormsModule,
		NzButtonModule,
		NzInputModule,
		NzSelectModule,
		NzFormModule,
		NzIconModule,
		NzToolTipModule,
		NzSwitchModule,
		NzTagModule,
		NzCheckboxModule,
		LanguageDirectiveModule,
		NzPopconfirmModule,
		TrimDirectiveModule,
		NzTableModule,
	]
})
export class PersCorreosElectronicosModule { }
