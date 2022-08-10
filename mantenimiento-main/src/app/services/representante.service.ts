import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {boolToChar, charToBool} from 'src/app/helpers/common.helper';
import {Respuesta} from 'src/app/models/respuesta';
import {environment} from '../../environments/environment';
import {ElimRepresentanteRequest, RepresentanteRequest, RepresentanteResponse} from '../models/representante';
import filterBuilder from 'src/app/helpers/JsonHelper';

@Injectable({
	providedIn: 'root'
})
export class RepresentanteService {

	constructor(
		private _httpClient: HttpClient,
	) {
	}

	obtenerRepresentantes(codigoPersona: number): Observable<Respuesta<RepresentanteResponse[]>> {
		return this._httpClient.get(`${environment.endpointPersonas}/representantes/${codigoPersona}`)
			.pipe(map((resp) => {
				const x: any = resp;
				const newResp: Respuesta<RepresentanteResponse[]> = {
					codigo: x.codigo,
					mensaje: x.mensaje,
					resultado: []
				}

				x?.resultado?.forEach((representante: any) => {
					newResp.resultado.push(charToBool(representante, ['principal']))
				})

				return newResp;
			}));
	}

	obtenerRepresentantesFiltros(params: unknown): Observable<Respuesta<RepresentanteResponse[]>> {
		const finalUrl = filterBuilder(`${environment.endpointPersonas}/representantes`, params ?? {});
		return this._httpClient.get(finalUrl)
			.pipe(map((resp) => {
				const x: any = resp;
				const newResp: Respuesta<RepresentanteResponse[]> = {
					codigo: x.codigo,
					mensaje: x.mensaje,
					resultado: []
				}
				x?.resultado?.forEach((representante: any) => {
					newResp.resultado.push(charToBool(representante, ['principal']))
				})
				return newResp;
			}));
	}

	insertarRepresentante(insertarRequest: RepresentanteRequest): Observable<Respuesta<any>> {
		return this._httpClient.post<Respuesta<void>>(`${environment.endpointPersonas}/representantes`, boolToChar(insertarRequest, ['principal']));
	}

	eliminarRepresentante(eliminarRequest: ElimRepresentanteRequest): Observable<Respuesta<any>> {
		return this._httpClient.put<Respuesta<void>>(`${environment.endpointPersonas}/representantes/eliminar`, eliminarRequest);
	}

}
