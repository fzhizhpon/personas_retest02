import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LanguageDirectiveModule } from 'src/app/components/ui/directives/language/language.directive.module';
import { InformacionAdicionalComponent } from './informacion-adicional.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzStepsModule } from 'ng-zorro-antd/steps';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzPopoverModule } from 'ng-zorro-antd/popover';
import { VsTableModule } from 'src/app/components/ui/table/table.module';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzSpinModule } from 'ng-zorro-antd/spin';

const NzMoodules = [
	NzFormModule,
	NzToolTipModule,
	NzIconModule,
	NzStepsModule,
	NzInputModule,
	NzButtonModule,
	NzSelectModule,
	NzTabsModule,
	NzPopoverModule,
	NzCheckboxModule,
	NzPopconfirmModule,
	NzTabsModule,
	NzSpinModule,
	LanguageDirectiveModule
]


@NgModule({
	declarations: [InformacionAdicionalComponent],
	exports: [InformacionAdicionalComponent],
	imports: [
		CommonModule,
		FormsModule,
		NzMoodules,
		ReactiveFormsModule,
		LanguageDirectiveModule,
		VsTableModule,
	]
})
export class PersInformacionAdicionalModule {

}
