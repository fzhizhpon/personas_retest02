import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { InicioRoutingModule } from './inicio-routing.module';
import { InicioComponent } from './inicio.component';
import { CabeceraModule } from 'src/app/components/ui/cabecera/cabecera.module';
import { MenuLateralModule } from 'src/app/components/ui/menu-lateral/menu-lateral.module';
import { IconDefinition } from '@ant-design/icons-angular';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { LoadingOutline } from '@ant-design/icons-angular/icons';
import { VsPanelNotificacionesModule } from 'src/app/components/ui/panel-notificaciones/panel-notificaciones.module';

const icons: IconDefinition[] = [ LoadingOutline ];


@NgModule({
	declarations: [
		InicioComponent
	],
	imports: [
		CommonModule,
		InicioRoutingModule,
		CabeceraModule,
		VsPanelNotificacionesModule,
		MenuLateralModule,
		NzIconModule.forChild(icons),
	]
})
export class InicioModule { }
