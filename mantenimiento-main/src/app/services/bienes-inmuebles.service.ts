import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Respuesta } from 'src/app/models/respuesta';
import { bienInmueble } from '../models/ingreso-socios.model';
import { ObtenerBienesInmuebles } from '../models/referencias-financieras.model';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';
import { boolToChar, charToBool } from 'src/app/helpers/common.helper';
import { BienInmuebleMinResponse } from '../models/bien-inmueble';

@Injectable({
	providedIn: 'root'
})
export class BienesInmueblesService {

	constructor(private http: HttpClient)
	{

	}

	obtenerBienesInmuebles(body: ObtenerBienesInmuebles): Observable<Respuesta<BienInmuebleMinResponse[]>>
	{
		return this.http.get(`${environment.endpointPersonas}/BienesInmuebles/${body.codigoPersona}`)
		.pipe(map((resp) => {
			const x: any = resp;
			const newResp: Respuesta<BienInmuebleMinResponse[]> = {
				codigo: x.codigo,
				mensaje: x.mensaje,
				resultado: []
			}
			x?.resultado?.forEach((res: any) => {
				newResp.resultado.push(res);
			})

			return newResp;
		}));
	}

	obtenerBienInmueble(codigoPersona: number, numeroRegistro: number): Observable<Respuesta<any>>
	{
		return this.http.get(`${environment.endpointPersonas}/BienesInmuebles/${codigoPersona}/${numeroRegistro}`)
		.pipe(map((data: any) => {
			const x = data;
			const resp: Respuesta<any> = {
				codigo: x.codigo,
				mensaje: x.mensaje,
				resultado: x.resultado,
			}

			return resp;
		}));
	}

	guardarBienesInmuebles(body: bienInmueble): Observable<Respuesta<bienInmueble[]>>
	{
		return this.http.post<Respuesta<bienInmueble[]>>(`${environment.endpointPersonas}/BienesInmuebles`, body);
	}

	eliminaBienesInmuebles(body: bienInmueble): Observable<Respuesta<bienInmueble[]>>
	{
		console.log(body);
		return this.http.put<Respuesta<bienInmueble[]>>(`${environment.endpointPersonas}/BienesInmuebles/estado`,body);
	}

	actualizaBienesInmuebles(body: bienInmueble): Observable<Respuesta<bienInmueble[]>>
	{
		console.log(body);
		return this.http.put<Respuesta<bienInmueble[]>>(`${environment.endpointPersonas}/BienesInmuebles`, body);
	}

	buscarBienesInmuebles(codigoPersona:number,filtro: string, busqueda: string | number)
	{
		const url = `${environment.endpointPersonas}/bienesinmuebles?codigoPersona=${codigoPersona}&${filtro}=${busqueda}`;
		return this.http.get<Respuesta<BienInmuebleMinResponse[]>>(url);
	}

}
