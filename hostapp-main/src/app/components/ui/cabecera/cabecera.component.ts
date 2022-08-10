import { Component, EventEmitter, Input, Output, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/api/autenticacion/auth.service';
import { LocalStorageService } from 'src/app/services/common/local-storage/local-storage.service';

@Component({
	selector: 'app-cabecera',
	templateUrl: './cabecera.component.html',
	styleUrls: ['./cabecera.component.scss'],
	encapsulation: ViewEncapsulation.None,
})
export class CabeceraComponent {

	@Input('showNotifications') showNotificationsBtn = true;
	@Input('showMenuButton') showMenuButton = false;
	@Output('onMenuClick') menuButtonClick: EventEmitter<boolean> = new EventEmitter<boolean>();

	showNotifications = false;

	constructor(
		private _storage: LocalStorageService,
		private _router: Router,
		private _auth: AuthService,
	) { }

	clickMenu(): void {
		this.menuButtonClick.emit(true)
	}

	cerrarSesion(): void {
		this._auth.CerrarSesion()
	}

}
