import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Respuesta } from 'src/app/models/respuesta';
import { environment } from '../../environments/environment';
import { ActualizarFamiliarRequest, EliminarFamiliarRequest, FamiliarResponse, InsertarFamiliarRequest } from '../models/familiar';
import { map } from 'rxjs/operators';
import { boolToChar, charToBool } from 'src/app/helpers/common.helper';


@Injectable({
	providedIn: 'root'
})
export class FamiliaresService {

	constructor(
		private http: HttpClient,
	) { }

	obtenerFamiliares(codigoPersona: number): Observable<Respuesta<FamiliarResponse[]>> {
		return this.http.get<Respuesta<FamiliarResponse[]>>(`${environment.endpointPersonas}/familiares/${codigoPersona}`)
		.pipe(map((resp) => {
			const x: any = resp;
			const newResp: Respuesta<FamiliarResponse[]> = {
				codigo: x.codigo,
				mensaje: x.mensaje,
				resultado: []
			}

			x?.resultado?.forEach((cel: any) => {
				newResp.resultado.push(charToBool(cel, ['esCargaFamiliar']))
			})

			return newResp;
		}));
	}


	obtenerFamiliar(codigoPersona: number, codigoFamiliar: number): Observable<any> {
		return this.http.get(`${environment.endpointPersonas}/familiares/${codigoPersona}/${codigoFamiliar}`)
		.pipe(map((resp) => {
			const x: any = resp;
			const newResp: Respuesta<any> = {
				codigo: x.codigo,
				mensaje: x.mensaje,
				resultado: charToBool(x?.resultado, ['esCargaFamiliar'])
			}

			return newResp;
		}));
	}


	insertarFamiliar(familiar: InsertarFamiliarRequest): Observable<Respuesta<InsertarFamiliarRequest[]>> {

		return this.http.post<Respuesta<InsertarFamiliarRequest[]>>(`${environment.endpointPersonas}/familiares`, boolToChar(familiar, ['esCargaFamiliar']));
	}

	eliminarFamiliar(familiar: EliminarFamiliarRequest): Observable<Respuesta<any[]>>
	{
		const {codigoPersona,codigoPersonaFamiliar}=familiar
		return this.http.put<Respuesta<EliminarFamiliarRequest[]>>(`${environment.endpointPersonas}/familiares/eliminar`,{codigoPersona,codigoPersonaFamiliar});
	}

	actualizarFamiliar(actualizarRequest: ActualizarFamiliarRequest): Observable<Respuesta<void>> {
		const {codigoParentesco ,codigoPersona,codigoPersonaFamiliar, esCargaFamiliar,observacion}=actualizarRequest
		return this.http.put<Respuesta<void>>(`${environment.endpointPersonas}/familiares`, boolToChar({codigoParentesco ,codigoPersona,codigoPersonaFamiliar, esCargaFamiliar,observacion}, ['esCargaFamiliar']));
	}

}
