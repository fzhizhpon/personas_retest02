/* eslint-disable @typescript-eslint/ban-types */
/* eslint-disable @typescript-eslint/no-empty-function */
import { Component, Injectable } from '@angular/core';
import { NzDrawerRef, NzDrawerService } from 'ng-zorro-antd/drawer';
import { DrawerOpts } from './DrawerOpts';

@Injectable({
	providedIn: 'root'
})
export class DrawerService {

	private drawer: NzDrawerRef | null = null;
	private lastComponent: Component | null = null;
	private callback: Function = () => {};

	constructor(
		private drawerService: NzDrawerService,
	) { }

	openDrawer(Options: DrawerOpts): void {
		this.callback = Options.Callback ?? (() => {});

		if(this.drawer)
		{
			this.drawer.close()
		}

		if(this.lastComponent === Options.Component)
		{
			this.lastComponent = null
			return
		}

		this.lastComponent = Options.Component

		if(!Options.Data) Options.Data = {}

		Options.Data['okCallback'] = this.callback;

		this.drawer = this.drawerService.create<typeof Options.Component, { value: string }, string>(
		{
			nzContent: Options.Component,
			nzContentParams: Options.Data,
			nzWidth: Options.Width ?? '60rem',
			nzHeight: Options.Height ?? '100%',
			nzClosable: false,
			nzPlacement: Options.Placement ?? 'right',
		})

		this.drawer.afterClose.subscribe(() =>
		{
			if(this.lastComponent === Options.Component)
			{
				this.lastComponent = null
			}
		})
	}

}
