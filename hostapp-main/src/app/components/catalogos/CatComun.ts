/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable @typescript-eslint/explicit-module-boundary-types */
import { Component, Input } from '@angular/core';
import { ControlValueAccessor } from '@angular/forms';
import { DataBinder } from '../common/DataBinder';
import { Catalogo } from './catalogo';

@Component({
	template: ''
})
export class CatComun<T> extends DataBinder<T> implements ControlValueAccessor {

	catalogos: Catalogo<T>[] = [];
	tieneError = false;
	labelError = "";
	@Input('placeholder') placeholder = "Seleccione una opción";
	@Input('allowClear') allowClear = false;

	constructor() {
		super();
	}

}
