import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuLateralComponent } from './menu-lateral.component';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { RouterModule } from '@angular/router';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';

@NgModule({
	declarations: [
		MenuLateralComponent,
	],
	imports: [
		CommonModule,
		NzMenuModule,
		RouterModule,
		NzSkeletonModule,
	],
	exports: [
		MenuLateralComponent,
	]
})
export class MenuLateralModule { }
