import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Respuesta } from 'src/app/models/respuesta';
import { direccion } from '../models/ingreso-socios.model';
import { ObtenerDirecciones } from '../models/referencias-financieras.model';
import { environment } from '../../environments/environment';
import { DireccionMinResponse, DireccionResponse } from '../models/direccion';
import { map } from 'rxjs/operators';
import { boolToChar, charToBool } from 'src/app/helpers/common.helper';

@Injectable({
	providedIn: 'root'
})
export class DireccionesService {

	constructor(private http: HttpClient)
	{

	}

	obtenerDirecciones(body: ObtenerDirecciones): Observable<Respuesta<DireccionMinResponse[]>>
	{
		return this.http.get(`${environment.endpointPersonas}/Direcciones?codigoPersona=${body.codigoPersona}`)
		.pipe(map((resp) => {
			const x: any = resp;
			const newResp: Respuesta<DireccionMinResponse[]> = {
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

	obtenerDireccion(codigoPersona: number, codigoDireccion: number): Observable<Respuesta<any>>
	{
		return this.http.get(`${environment.endpointPersonas}/Direcciones/${codigoPersona}/${codigoDireccion}`)
		.pipe(map((data: any) => {
			const x = data;
			const resp: Respuesta<any> = {
				codigo: x.codigo,
				mensaje: x.mensaje,
				resultado: charToBool(x.resultado, ['principal', 'esMarginal'])
			}
			return resp;
		}));
	}

	guardarDirecciones(body: direccion): Observable<Respuesta<direccion[]>>
	{
		console.log()
		return this.http.post<Respuesta<direccion[]>>(`${environment.endpointPersonas}/Direcciones`, boolToChar(body, ['esMarginal', 'principal']));
	}

	eliminaDirecciones(body: direccion): Observable<Respuesta<direccion[]>>
	{
		return this.http.delete<Respuesta<direccion[]>>(`${environment.endpointPersonas}/Direcciones?codigoPersona=${body.codigoPersona}&numeroRegistro=${body.numeroRegistro}`);
	}

	actualizaDirecciones(body: direccion): Observable<Respuesta<direccion[]>>
	{
		return this.http.put<Respuesta<direccion[]>>(`${environment.endpointPersonas}/Direcciones`, boolToChar(body, ['esMarginal', 'principal']));
	}

}
