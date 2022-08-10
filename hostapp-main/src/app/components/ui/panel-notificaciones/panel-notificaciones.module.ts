import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VsPanelNotificacionesComponent } from './panel-notificaciones.component';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';


@NgModule({
	declarations: [VsPanelNotificacionesComponent],
	exports: [VsPanelNotificacionesComponent],
	imports: [
		CommonModule,
		NzEmptyModule,
		NzButtonModule,
		NzIconModule,
		NzToolTipModule,
	]
})
export class VsPanelNotificacionesModule { }
