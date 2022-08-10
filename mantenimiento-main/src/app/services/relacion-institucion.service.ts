import { HttpClient, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environmentHost } from 'src//environments/environment';
import { Observable } from 'rxjs';
import { Respuesta } from 'src/app/models/respuesta';
import { referenciaFinanciera, referenciaFinancieraInsert, referenciaFinancieraUpdate } from '../models/ingreso-socios.model';

import { environment } from '../../environments/environment';
import { RelacionInstitucion } from '../models/relacion-institucion';
import { map } from 'rxjs/operators';


@Injectable({
	providedIn: 'root'
})
export class RelacionInstitucionService {

	readonly url: string = environmentHost.gateway;

	constructor(
		private http: HttpClient,
	) {
	}

	obtenerRelacionInstitucion(body:number/*body: RelacionInstitucion*/): Observable<Respuesta<RelacionInstitucion[]>>
	{
		return this.http.get(`${environment.endpointPersonas}/RelacionInstitucion?codigoPersona=${body}`)
		.pipe(map((resp) => {
			const x: any = resp;
			const newResp: Respuesta<RelacionInstitucion[]> = {
				codigo: x.codigo,
				mensaje: x.mensaje,
				resultado: []
			}

			x?.resultado?.forEach((obj: any) => {
				newResp.resultado.push(obj)
			})
			return newResp;
		}));
	}




	guardarRelacionInstitucion(body: RelacionInstitucion): Observable<Respuesta<referenciaFinanciera[]>>
	{

		/*{
			codigoPersona: number;
			codigoRelacion: number;
			descripcion: String;
		} */
		return this.http.post<Respuesta<referenciaFinanciera[]>>(`${environment.endpointPersonas}/RelacionInstitucion`, body);
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
