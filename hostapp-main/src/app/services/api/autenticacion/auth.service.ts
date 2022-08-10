import { HttpBackend, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'projects/autenticacion/src/environments/environment';
import { Observable } from 'rxjs';
import { finalize, map } from 'rxjs/operators';
import { InformacionTerminal } from 'src/app/models/info-terminal';
import { MensajeOperativo } from 'src/app/models/mensaje-operativo';
import { Menu } from 'src/app/models/menu';
import { Respuesta } from 'src/app/models/respuesta';
import { LocalStorageService } from '../../common/local-storage/local-storage.service';

@Injectable({
	providedIn: 'root'
})
export class AuthService {

	private readonly url: string = environment.seguridad;
	private http!: HttpClient;
	infoTerminal: InformacionTerminal = {
		sistemaOperativo: '',
		ipPublica: ''
	};

	constructor(
		private _http: HttpClient,
		private _httpBackend: HttpBackend,
		private _storage: LocalStorageService,
		private _router: Router,
	) {
		this.ColocarSO();
		this.ColocarIpPublica();
	}

	ObtenerMensajes(): Observable<Respuesta<MensajeOperativo[]>>
	{
		return this._http.get<Respuesta<MensajeOperativo[]>>(`${this.url}/mensajes`);
	}

	ObtenerOpcionesMenu(): Observable<Respuesta<Menu[]>>
	{
		return this._http.get<Respuesta<Menu[]>>(`${this.url}/opciones-sistema`);
	}

	RefrescarSesion(): Observable<string>
	{
		return this._http.get<Respuesta<{token: string}>>(`${this.url}/refresh-token`)
		.pipe(map(x => x.resultado.token));
	}

	CerrarSesion(): void
	{
		this._http.get(`${this.url}/cerrar-sesion`)
		.pipe(finalize(() => {
			this._storage.clear('sesion')
			this._router.navigate(['autenticacion'])
		}))
		.subscribe()
	}

	private ColocarSO()
	{
		this.infoTerminal.sistemaOperativo = navigator?.platform

		if(navigator.appVersion) {
			if (navigator.appVersion.indexOf("Win") != -1) this.infoTerminal.sistemaOperativo = "Windows OS";
			if (navigator.appVersion.indexOf("Mac") != -1) this.infoTerminal.sistemaOperativo = "MacOS";
			if (navigator.appVersion.indexOf("X11") != -1) this.infoTerminal.sistemaOperativo = "UNIX OS";
			if (navigator.appVersion.indexOf("Linux") != -1) this.infoTerminal.sistemaOperativo = "Linux OS";
		}
	}

	private ColocarIpPublica() {
		this.http = new HttpClient(this._httpBackend);
		this.http.get<{ip: string;}>('https://api.ipify.org/?format=json')
		.subscribe(resp => {
			this.infoTerminal.ipPublica = resp.ip;
		})
	}

}
