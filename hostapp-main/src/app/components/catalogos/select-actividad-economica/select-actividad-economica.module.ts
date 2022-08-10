import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VsSelectActividadEconomicaComponent } from './select-actividad-economica.component';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { ModalActividadEconomicaModule } from './modal-actividad-economica/modal-actividad-economica.module';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';

@NgModule({
	declarations: [VsSelectActividadEconomicaComponent],
	exports: [VsSelectActividadEconomicaComponent],
	imports: [
		CommonModule,
		NzInputModule,
		NzButtonModule,
		ModalActividadEconomicaModule,
		NzIconModule,
		NzToolTipModule,
	],
})
export class VsSelectActividadEconomicaModule { }
