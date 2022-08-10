import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainLayoutRoutingModule } from './main-layout-routing.module';
import { MainLayoutComponent } from './main-layout.component';
import { MenuLateralModule } from 'src/app/components/ui/menu-lateral/menu-lateral.module';
import { CabeceraModule } from 'src/app/components/ui/cabecera/cabecera.module';


@NgModule({
	declarations: [
		MainLayoutComponent
	],
	imports: [
		CommonModule,
		MainLayoutRoutingModule,
		MenuLateralModule,
		CabeceraModule,
	]
})
export class MainLayoutModule { }
