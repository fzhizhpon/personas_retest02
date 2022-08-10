import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Respuesta } from 'src/app/models/respuesta';
import { environment } from '../../environments/environment';
import { ActualizarEstadoFinancieroRequest, EstadoFinancieroResponse, InsertarEstadoFinancieroRequest } from '../models/estado-financiero';

@Injectable({
	providedIn: 'root'
})
export class EstadoFinancieroService {

	constructor(
		private http: HttpClient,
	) { }

	obtenerEstadoFinanciero(codigoPersona: number,estado:string): Observable<Respuesta<EstadoFinancieroResponse[]>> {
		return this.http.get<Respuesta<EstadoFinancieroResponse[]>>(`${environment.endpointPersonas}/EstadosFinancieros/${codigoPersona}/${estado}`);
		
	}

	insertarEstadoFinanciero(insertarRequest: InsertarEstadoFinancieroRequest): Observable<Respuesta<InsertarEstadoFinancieroRequest[]>> {

		
		return this.http.post<Respuesta<InsertarEstadoFinancieroRequest[]>>(`${environment.endpointPersonas}/EstadosFinancieros`,insertarRequest);
	}

	actualizarEstadoFinanciero(actualizarRequest: ActualizarEstadoFinancieroRequest): Observable<Respuesta<void>> {
		return this.http.put<Respuesta<void>>(`${environment.endpointPersonas}/EstadosFinancieros`,actualizarRequest);
	}

}
