import {Injectable} from '@angular/core';
import {HttpClient, HttpBackend} from '@angular/common/http';
import {HttpHeaders} from '@angular/common/http';

@Injectable({
	providedIn: 'root'
})
export class ReportesService {

	headers = new HttpHeaders().set('Content-Type', 'text/plain; charset=utf-8');

	constructor(
		private _http: HttpClient,
		private _httpBackend: HttpBackend,
	) {
	}

	reportesPersonas(codigoPersona: number, codigoTipoPersona: number) {
		return this._http.post<any>('reportes/personas',
			{
				codigoPersona,
				codigoTipoPersona
			});
	}

	// * reporteria con carbone

	reportesAperturaCuenta(dataReporte: any) {
		return this._http.post<any>('reportesCarbone/contAperturaCta', dataReporte);
	}
}
