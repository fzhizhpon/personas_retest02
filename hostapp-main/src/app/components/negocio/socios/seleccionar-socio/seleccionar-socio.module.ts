import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SeleccionarSocioComponent } from './seleccionar-socio.component';
import { FormsModule } from '@angular/forms';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { ModalBuscarSocioModule } from '../modal-buscar-socio/modal-buscar-socio.module';


@NgModule({
	declarations: [SeleccionarSocioComponent],
	exports: [SeleccionarSocioComponent],
	imports: [
		CommonModule,
		FormsModule,
		NzInputModule,
		NzButtonModule,
		NzIconModule,
		ModalBuscarSocioModule,
	]
})
export class SeleccionarSocioModule { }
