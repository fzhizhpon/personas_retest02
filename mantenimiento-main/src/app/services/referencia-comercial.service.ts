import { HttpClient, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environmentHost } from 'src//environments/environment';
import { Observable } from 'rxjs';
import { Respuesta } from 'src/app/models/respuesta';
import { referenciaComercial, referenciaComercialEliminar } from '../models/ingreso-socios.model';
import { ObtenerReferenciasComerciales } from '../models/referencias-financieras.model';
import { environment } from '../../environments/environment';

@Injectable({
	providedIn: 'root'
})
export class ReferenciaComercialService {

	readonly url: string = environmentHost.gateway;

	constructor(
		private http: HttpClient,
	) {
	}

	obtenerReferenciaComercial(body: referenciaComercial): Observable<Respuesta<referenciaComercial>>
	{
		return this.http.post<Respuesta<referenciaComercial>>(`${environment.endpointPersonas}/ReferenciasComerciales/obtener-referencia-comercial`, body);
	}

	obtenerReferenciasComerciales(body: ObtenerReferenciasComerciales): Observable<Respuesta<referenciaComercial[]>>
	{
		return this.http.post<Respuesta<referenciaComercial[]>>(`${environment.endpointPersonas}/ReferenciasComerciales/obtener-referencias-comerciales`, body);
	}


	guardarReferenciasComerciales(body: referenciaComercial): Observable<Respuesta<referenciaComercial[]>>
	{
		return this.http.post<Respuesta<referenciaComercial[]>>(`${environment.endpointPersonas}/ReferenciasComerciales/guardar-referencia-comercial`, body);
	}


	eliminaReferenciasComerciales(body: referenciaComercialEliminar): Observable<Respuesta<referenciaComercial[]>>
	{
		return this.http.post<Respuesta<referenciaComercial[]>>(`${environment.endpointPersonas}/ReferenciasComerciales/eliminar-referencia-comercial`, body);
	}


	actualizaReferenciasComerciales(body: referenciaComercial): Observable<Respuesta<referenciaComercial[]>>
	{
		const { lugar, ...resto } = body;
		return this.http.post<Respuesta<referenciaComercial[]>>(`${environment.endpointPersonas}/ReferenciasComerciales/actualizar-referencia-comercial`, resto);
	}

}
