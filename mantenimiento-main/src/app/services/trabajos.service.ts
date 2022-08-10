import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {environmentHost} from 'src//environments/environment';
import {Observable} from 'rxjs';
import {Respuesta} from 'src/app/models/respuesta';
import {trabajo, ObtenerTrabajos} from '../models/ingreso-socios.model';
import {environment} from '../../environments/environment';
import {map} from 'rxjs/operators';

import {boolToChar, charToBool} from 'src/app/helpers/common.helper';

@Injectable({
	providedIn: 'root'
})
export class TrabajosService {

	readonly url: string = environmentHost.gateway;

	constructor(
		private http: HttpClient,
	) {
	}

	obtenerTrabajo(body: trabajo): Observable<Respuesta<trabajo>> {
		return this.http.post<Respuesta<trabajo>>(`${environment.endpointPersonas}/Trabajos/obtener-trabajo`, body)
			.pipe(map((resp) => {
				const x: any = resp;
				const newResp: Respuesta<trabajo> = {
					codigo: x.codigo,
					mensaje: x.mensaje,
					resultado: charToBool(x?.resultado, ['principal'])
				}
				return newResp;
			}))
	}

	obtenerTrabajos(body: ObtenerTrabajos): Observable<Respuesta<trabajo[]>> {
		return this.http.post<Respuesta<trabajo[]>>(`${environment.endpointPersonas}/Trabajos/obtener-trabajos`, body)
			.pipe(map((resp) => {
				const x: any = resp;
				const newResp: Respuesta<trabajo[]> = {
					codigo: x.codigo,
					mensaje: x.mensaje,
					resultado: []
				}

				x?.resultado?.forEach((cel: any) => {
					newResp.resultado.push(charToBool(cel, ['principal']))
				})
				return newResp;
			}))
	}


	guardarTrabajo(body: trabajo): Observable<Respuesta<trabajo[]>> {
	
		const { numeroRegistro, lugar, ...resto } = body;
		return this.http.post<Respuesta<trabajo[]>>(`${environment.endpointPersonas}/Trabajos/guardar-trabajo`, boolToChar(resto, ['principal']));
	}


	eliminaTrabajo(body: trabajo): Observable<Respuesta<trabajo[]>> {
		{body.codigoPersona, body.numeroRegistro}
		console.log(body);
		return this.http.post<Respuesta<trabajo[]>>(`${environment.endpointPersonas}/Trabajos/eliminar-trabajo`, body);
	}


	actualizaTrabajo(body: trabajo): Observable<Respuesta<trabajo[]>> {
		console.log(body);
		return this.http.post<Respuesta<trabajo[]>>(`${environment.endpointPersonas}/Trabajos/actualizar-trabajo`, boolToChar(body, ['principal']));
	}

}
