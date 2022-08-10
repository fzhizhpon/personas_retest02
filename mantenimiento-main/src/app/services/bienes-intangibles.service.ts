import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Respuesta } from 'src/app/models/respuesta';
import { bienIntangible } from '../models/ingreso-socios.model';
import { ObtenerBienesIntangibles } from '../models/referencias-financieras.model';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';
import { BienIntangibleMinResponse, EliminarBienIntangibleRequest, InsertarBienIntangible } from '../models/bien-intangible';
@Injectable({
	providedIn: 'root'
})
export class BienesIntangiblesService {

	constructor(private http: HttpClient)
	{

	}

	obtenerBienesIntangibles(codigoPersona: number): Observable<Respuesta<BienIntangibleMinResponse[]>>
	{
		return this.http.get(`${environment.endpointPersonas}/BienesIntangibles/${codigoPersona}`)
		.pipe(map((resp) => {
			const x: any = resp;
			const newResp: Respuesta<BienIntangibleMinResponse[]> = {
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

	obtenerBienIntangible(codigoPersona: number, codigoDireccion: number): Observable<Respuesta<any>>
	{
		return this.http.get(`${environment.endpointPersonas}/BienesIntangibles/${codigoPersona}/${codigoDireccion}`)
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

	guardarBienesIntangibles(body: InsertarBienIntangible): Observable<Respuesta<void>>
	{
		console.log(body);
		return this.http.post<Respuesta<void>>(`${environment.endpointPersonas}/BienesIntangibles`, body);
	}

	eliminaBienesIntangibles(body: EliminarBienIntangibleRequest): Observable<Respuesta<bienIntangible[]>>
	{
		console.log(body);
		{body.codigoPersona, body.numeroRegistro}
		return this.http.put<Respuesta<bienIntangible[]>>(`${environment.endpointPersonas}/BienesIntangibles/estado`,body);
	}

	actualizaBienesIntangibles(body: bienIntangible): Observable<Respuesta<bienIntangible[]>>
	{
		console.log(body);
		
		const {codigoPersona,numeroRegistro, descripcion, avaluoComercial }=body
		return this.http.put<Respuesta<bienIntangible[]>>(`${environment.endpointPersonas}/BienesIntangibles`, {codigoPersona,numeroRegistro, descripcion, avaluoComercial });
	}

}
