import { Component, Input, ViewEncapsulation } from '@angular/core';
import { BotonAccion } from './panel-acciones.class';

@Component({
	selector: 'app-panel-acciones',
	templateUrl: './panel-acciones.component.html',
	styleUrls: ['./panel-acciones.component.scss'],
	encapsulation: ViewEncapsulation.None,
})
export class PanelAccionesComponent {

	@Input('closable') closable = true;
	collapse = false;
	@Input('orientacion') orientacion: 'vertical' | 'horizontal' = 'horizontal';
	@Input('ancho') ancho!: string | null;
	@Input('alto') alto!: string | null;
	@Input('redondeado') redondeado = true;
	@Input('mostrarLabels') mostrarLabels = false;

	@Input('color') color = 'var(--primary)';

	@Input('botones') botones: BotonAccion[] = [];

	triggerEvent(btn: BotonAccion): void {
		if(!btn.deshabilitado) {
			if(btn.click) {
				btn.click()
			}
		}
	}

}
