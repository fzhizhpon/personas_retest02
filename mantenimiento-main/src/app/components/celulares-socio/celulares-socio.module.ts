import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CelularesComponent } from './celulares-socio.component';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzInputModule } from 'ng-zorro-antd/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzIconModule } from 'ng-zorro-antd/icon';
import {
	VsSelectOperadoraMovilModule
} from 'src/app/components/catalogos/select-operadora-movil/vs-select-operadora-fija.module';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { LanguageDirectiveModule } from 'src/app/components/ui/directives/language/language.directive.module';
import { VsSelectCodigoPaisModule } from 'src/app/components/catalogos/select-codigo-pais/vs-select-codigo-pais.module';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { TrimDirectiveModule } from 'src/app/components/ui/directives/trim/trim.directive.module';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';

const NzModules = [
	NzButtonModule,
	NzInputModule,
	NzSelectModule,
	NzFormModule,
	NzTableModule,
	NzIconModule,
	NzCheckboxModule,
	NzTagModule,
	NzModalModule,
	LanguageDirectiveModule,
	VsSelectCodigoPaisModule,
	NzToolTipModule,
	NzPopconfirmModule,
];

@NgModule({
	declarations: [CelularesComponent],
	exports: [CelularesComponent],
	imports: [
		CommonModule,
		NzModules,
		FormsModule,
		ReactiveFormsModule,
		VsSelectOperadoraMovilModule,
		TrimDirectiveModule,
	]
})
export class PersCelularesModule {
}
