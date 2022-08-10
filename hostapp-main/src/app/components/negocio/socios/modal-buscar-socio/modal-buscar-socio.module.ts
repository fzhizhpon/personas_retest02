import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalBuscarSocioComponent } from './modal-buscar-socio.component';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';


@NgModule({
	declarations: [ModalBuscarSocioComponent],
	exports: [ModalBuscarSocioComponent],
	imports: [
		CommonModule,
		FormsModule,
		ReactiveFormsModule,
		NzModalModule,
		NzButtonModule,
		NzInputModule,
		NzTableModule,
		NzRadioModule,
		NzIconModule,
		NzToolTipModule,
	]
})
export class ModalBuscarSocioModule { }
