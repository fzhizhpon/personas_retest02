import { HttpClient, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environmentHost } from 'src//environments/environment';
import { Observable } from 'rxjs';
import { Respuesta } from 'src/app/models/respuesta';
import { referenciaFinanciera, referenciaFinancieraInsert, referenciaFinancieraUpdate } from '../models/ingreso-socios.model';
import { ObtenerReferenciasFinancieras } from '../models/referencias-financieras.model';
import { environment } from '../../environments/environment';


@Injectable({
	providedIn: 'root'
})
export class ReferenciaFinancieraService {

	readonly url: string = environmentHost.gateway;

	constructor(
		private http: HttpClient,
	) {
	}

	obtenerReferenciaFinaniera(body: referenciaFinanciera): Observable<Respuesta<referenciaFinanciera>>
	{
		return this.http.post<Respuesta<referenciaFinanciera>>(`${environment.endpointPersonas}/ReferenciasFinancieras/obtener-referencia-financiera`, body);
	}

	obtenerReferenciasFinancieras(body: ObtenerReferenciasFinancieras): Observable<Respuesta<referenciaFinanciera[]>>
	{
		return this.http.post<Respuesta<referenciaFinanciera[]>>(`${environment.endpointPersonas}/ReferenciasFinancieras/obtener-referencias-financieras`, body);
	}


	guardarReferenciasFinancieras(body: referenciaFinancieraInsert): Observable<Respuesta<referenciaFinanciera[]>>
	{

		return this.http.post<Respuesta<referenciaFinanciera[]>>(`${environment.endpointPersonas}/ReferenciasFinancieras/guardar-referencia-financiera`, body);
	}


	eliminaReferenciasFinancieras(body: referenciaFinanciera): Observable<Respuesta<referenciaFinanciera[]>>
	{
		const {codigoPersona, numeroRegistro}=body
		return this.http.post<Respuesta<referenciaFinanciera[]>>(`${environment.endpointPersonas}/ReferenciasFinancieras/eliminar-referencia-financiera`, {codigoPersona, numeroRegistro});
	}


	actualizaReferenciasFinancieras(body: referenciaFinancieraUpdate): Observable<Respuesta<referenciaFinanciera[]>>
	{

		const {cifras, codigoPersona, numeroRegistro, saldo, saldoObligacion, obligacionMensual, observaciones}=body
		return this.http.post<Respuesta<referenciaFinanciera[]>>(`${environment.endpointPersonas}/ReferenciasFinancieras/actualizar-referencia-financiera`, {cifras, codigoPersona, numeroRegistro, saldo, saldoObligacion, obligacionMensual,observaciones});
	}

}
