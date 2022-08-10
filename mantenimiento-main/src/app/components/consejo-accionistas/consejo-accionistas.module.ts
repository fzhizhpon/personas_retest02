import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConsejoAccionistasComponent } from './consejo-accionistas.component';

import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzInputModule } from 'ng-zorro-antd/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { VsTableModule } from 'src/app/components/ui/table/table.module';


const NzModules = [
	NzButtonModule,
	NzInputModule,
	NzSelectModule,
	NzFormModule,
	NzTableModule,
	NzIconModule
];

@NgModule({
	declarations: [
		ConsejoAccionistasComponent
	],
	exports: [ConsejoAccionistasComponent],
	imports: [
		CommonModule,
		NzModules,
		FormsModule,
		ReactiveFormsModule,
		VsTableModule,
	]
})
export class ConsejoAccionistasModule { }
