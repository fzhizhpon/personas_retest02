import {Component, forwardRef, QueryList, ViewChildren} from '@angular/core';
import {ComponenteBasePersona} from '../base/componente-base-persona';
import {concat, Observable} from 'rxjs';
import {finalize} from 'rxjs/operators';
import { RegisteredComponent } from 'src/app/components/common/registered.component';

@Component({
	selector: 'pers-bienes',
	templateUrl: './bienes.component.html',
	styleUrls: ['./bienes.component.scss'],
	providers: [{
		provide: ComponenteBasePersona,
		useExisting: forwardRef(() => BienesComponent)
	}]
})
export class BienesComponent extends ComponenteBasePersona implements RegisteredComponent {

	readonly codigoComponente = 7;
	mostrarListar = true;

	nombreComponente = 'Bienes';
	tabSeleccionado = 0;
	indSeleccionado = -1;
	tieneCambios = true;

	@ViewChildren(ComponenteBasePersona) hojasDeTrabajo!: QueryList<ComponenteBasePersona>;

	constructor() {
		super();
	}

	GuardarCambios(): Observable<any> {
		this.cargando.guardar = true;
		const operaciones: Observable<unknown>[] = [];

		this.hojasDeTrabajo.forEach((hoja, i) => {
			operaciones.push(hoja.GuardarCambios())
		})

		return concat(...operaciones)
			.pipe(finalize(() => {
				this.cargando.guardar = false;
			}));
	}

	ObtenerTodo() {
		return;
	}
}
