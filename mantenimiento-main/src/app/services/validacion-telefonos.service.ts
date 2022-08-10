import {Injectable} from '@angular/core';
import {FormGroup} from "@angular/forms";

@Injectable({
	providedIn: 'root'
})
export class ValidacionTelefonosService {

	constructor() {
	}

	arrayIgual(nombreControl: string): any {
		return (formGroup: FormGroup) => {
			const arraySeleccionado = formGroup.get(nombreControl);
			// * vacio
			if (arraySeleccionado!.value.length === 0) {
				arraySeleccionado?.setErrors({vacio: true});
			}
			// * valores repetidos
			const valoresUnicos = [...new Set(arraySeleccionado!.value.map((item: any) => item))];
			if (arraySeleccionado!.value.length !== valoresUnicos.length) {
				arraySeleccionado?.setErrors({repetido: true});
			}
			// * elementos vacios
			if (arraySeleccionado!.value.length !== 0) {
				arraySeleccionado!.value.forEach((item: any) => {
					if (item === '') {
						arraySeleccionado?.setErrors({sinValores: true});
					}
				})
			}
		}
	}

}
