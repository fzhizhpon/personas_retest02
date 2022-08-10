import { ActualizarCelularRequest, CelularResponse, EliminarCelularRequest, InsertarCelularRequest } from '../models/celular';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ObtenerCelulares } from '../models/referencias-financieras.model';
import { Respuesta } from 'src/app/models/respuesta';
import { boolToChar, charToBool } from 'src/app/helpers/common.helper';
import { map } from 'rxjs/operators';


@Injectable({
	providedIn: 'root'
})
export class CelularesService {

	constructor(
		private http: HttpClient,
	) {
	}

	obtenerCelulares(body: ObtenerCelulares): Observable<Respuesta<CelularResponse[]>> {
		return this.http.get(`${environment.endpointPersonas}/telefonos-moviles?codigoPersona=${body.codigoPersona}`)
		.pipe(map((resp) => {
			const x: any = resp;
			const newResp: Respuesta<CelularResponse[]> = {
				codigo: x.codigo,
				mensaje: x.mensaje,
				resultado: []
			}

			x?.resultado?.forEach((cel: any) => {
				newResp.resultado.push(charToBool(cel, ['principal']))
			})

			return newResp;
		}));
	}

	insertarCelular(insertarRequest: InsertarCelularRequest): Observable<Respuesta<void>> {
		return this.http.post<Respuesta<void>>(`${environment.endpointPersonas}/telefonos-moviles`, boolToChar(insertarRequest, ['principal']));
	}

	eliminarCelular(body: EliminarCelularRequest): Observable<Respuesta<void>> {

		return this.http.put<Respuesta<void>>(`${environment.endpointPersonas}/telefonos-moviles/estado`, body);
	}

	actualizarCelular(actualizarRequest: ActualizarCelularRequest): Observable<Respuesta<void>> {
		return this.http.put<Respuesta<void>>(`${environment.endpointPersonas}/telefonos-moviles`, boolToChar(actualizarRequest, ['principal']));
	}

}
