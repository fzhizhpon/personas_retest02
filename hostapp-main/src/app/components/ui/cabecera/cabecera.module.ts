import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CabeceraComponent } from './cabecera.component';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { VsPanelNotificacionesModule } from '../panel-notificaciones/panel-notificaciones.module';
import { NzPopoverModule } from 'ng-zorro-antd/popover';

@NgModule({
	declarations: [CabeceraComponent],
	exports: [CabeceraComponent],
	imports: [
		CommonModule,
		NzInputModule,
		NzIconModule,
		NzButtonModule,
		VsPanelNotificacionesModule,
		NzPopoverModule,
	],
})
export class CabeceraModule { }
