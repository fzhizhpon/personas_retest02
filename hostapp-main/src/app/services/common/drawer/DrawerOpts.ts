import { Component } from "@angular/core";

export interface DrawerOpts {
	Component: any;
	Data?: any;
	Callback?: Function;
	Width?: string;
	Height?: string;
	Placement?: 'top' | 'right' | 'bottom' | 'left';
}
