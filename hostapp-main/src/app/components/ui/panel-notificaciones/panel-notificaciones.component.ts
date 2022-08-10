import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { MensajeOperativo } from 'src/app/models/mensaje-operativo';
import { AuthService } from 'src/app/services/api/autenticacion/auth.service';

@Component({
	selector: 'vs-panel-notificaciones',
	templateUrl: './panel-notificaciones.component.html',
	styleUrls: ['./panel-notificaciones.component.scss']
})
export class VsPanelNotificacionesComponent implements OnInit {

	mensajes: MensajeOperativo[] = [];
	mostrarCargando = true;

	constructor(
		private _authService: AuthService,
	) { }

	ngOnInit(): void {
		this.obtenerMensajesOperativos();
	}

	obtenerMensajesOperativos(): void {
		this.mostrarCargando = true

		this._authService.ObtenerMensajes()
		.pipe(finalize(() => this.mostrarCargando = false))
		.subscribe(data => {
			this.mensajes = data.resultado
		})
	}

}
