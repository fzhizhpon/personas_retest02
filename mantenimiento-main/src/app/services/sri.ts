import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Respuesta } from 'src/app/models/respuesta';
import { environment } from '../../environments/environment';
import { PlacaResponse, ContribuyenteConsolidadoResponse } from '../models/sri';


@Injectable({
	providedIn: 'root'
})
export class SriService {

	constructor(
		private http: HttpClient,
	) { }


	obtenerContribuyente(numeroRuc: String): Observable<Respuesta<ContribuyenteConsolidadoResponse[]>> {
		return this.http.get<Respuesta<ContribuyenteConsolidadoResponse[]>>(`${environment.endpointPersonas}/sri/ruc/${numeroRuc}`);
	}


	obtenerInformacionPlaca(codigoPlaca: String): Observable<Respuesta<PlacaResponse>> {
		return this.http.get<Respuesta<PlacaResponse>>(`${environment.endpointPersonas}/sri/placa/${codigoPlaca}`);
	}

}
