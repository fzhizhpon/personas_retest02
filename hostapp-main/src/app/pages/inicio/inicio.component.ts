import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { MensajeOperativo } from 'src/app/models/mensaje-operativo';
import { Sesion } from 'src/app/models/Sesion';
import { AuthService } from 'src/app/services/api/autenticacion/auth.service';
import { LocalStorageService } from 'src/app/services/common/local-storage/local-storage.service';

@Component({
	selector: 'app-inicio',
	templateUrl: './inicio.component.html',
	styleUrls: ['./inicio.component.scss']
})
export class InicioComponent {

	infoUsuario: Sesion;

	constructor(
		private _storageService: LocalStorageService,
	) {
		this.infoUsuario = this._storageService.getSesion();
	}

}
