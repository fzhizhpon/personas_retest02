import { HttpClient, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environmentHost } from 'src//environments/environment';
import { Observable } from 'rxjs';
import { Respuesta } from 'src/app/models/respuesta';
import { EliminarReferenciaPersonalRequest, GuardarReferenciaPersonal, referenciaPersonal } from '../models/ingreso-socios.model';
import { ObtenerReferenciasPersonales } from '../models/referencias-financieras.model';
import { environment } from '../../environments/environment';


@Injectable({
	providedIn: 'root'
})
export class ReferenciaPersonalService {

	readonly url: string = environmentHost.gateway;

	constructor(
		private http: HttpClient,
	) {
	}

	obtenerReferenciaPersonal(body: referenciaPersonal): Observable<Respuesta<referenciaPersonal>>
	{
		return this.http.post<Respuesta<referenciaPersonal>>(`${environment.endpointPersonas}/ReferenciasPersonales/obtener-referencia-personal`, body);
	}

	obtenerReferenciaPersonales(body: ObtenerReferenciasPersonales): Observable<Respuesta<referenciaPersonal[]>>
	{
		return this.http.post<Respuesta<referenciaPersonal[]>>(`${environment.endpointPersonas}/ReferenciasPersonales/obtener-referencias-personales`, body);
	}


	guardarReferenciaPersonales(body: GuardarReferenciaPersonal): Observable<Respuesta<referenciaPersonal[]>>
	{
		return this.http.post<Respuesta<referenciaPersonal[]>>(`${environment.endpointPersonas}/ReferenciasPersonales/guardar-referencia-personal`, body);
	}


	eliminaReferenciaPersonales(body: EliminarReferenciaPersonalRequest): Observable<Respuesta<referenciaPersonal[]>>
	{
		return this.http.post<Respuesta<referenciaPersonal[]>>(`${environment.endpointPersonas}/ReferenciasPersonales/eliminar-referencia-personal`, body);
	}

}
