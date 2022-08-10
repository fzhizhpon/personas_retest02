/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable @typescript-eslint/explicit-module-boundary-types */
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ControlValueAccessor } from '@angular/forms';

@Component({
	template: ''
})
export class DataBinder<T> implements ControlValueAccessor {

	@Input('placeholder') placeholder = "";
	@Input('disabled') isDisabled = false;
	@Input('readonly') isReadonly = false;
	@Input('ngModel') value!: T;
	@Output('ngModelChange') emitter: EventEmitter<T> = new EventEmitter<T>();

	isLoading = true;

	onChange = (value: T): void => { value };
	onTouch = (): void => { return; }

	inputText = "";

	change(value: T): void {
		if(this.isReadonly || this.isDisabled) {
			return;
		}

		this.value = value;
		this.emitter.emit(value);
		this.changeCallback();
		this.onChange(value);
	}

	changeCallback(): void { return; }

	writeValue(obj: any): void {
		if(obj == null) {
			this.value = obj;
			this.emitter.emit(obj);
			this.inputText = obj;
		} else {
			try {
				this.change(obj);
			}
			catch (ex) {
				console.error(ex)
			}
		}
	}

	registerOnChange(fn: any): void {
		this.onChange = fn;
	}

	registerOnTouched(fn: any): void {
		this.onTouch = fn;
	}

	setDisabledState?(isDisabled: boolean): void {
		this.isDisabled = isDisabled
	}
}
