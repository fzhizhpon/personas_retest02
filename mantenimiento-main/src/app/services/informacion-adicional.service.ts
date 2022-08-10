import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { boolToChar, charToBool } from 'src/app/helpers/common.helper';
import { Respuesta } from 'src/app/models/respuesta';
import { environmentHost } from 'src/environments/environment';
import { environment } from '../../environments/environment';
import { InformacionAdicional } from '../models/informacion-adicional';
import { TablaComunesCabecera } from '../models/tabla-comunes-cabecera';

@Injectable({
	providedIn: 'root'
})
export class InformacionAdicionalService {

	constructor(
		private http: HttpClient,
	) { }

	obtenerTablaComunesCabecera(codigoOpcion:string): Observable<Respuesta<TablaComunesCabecera[]>> {
		return this.http.get<Respuesta<TablaComunesCabecera[]>>(`${environmentHost.catalogos}/TablasComunesCabecera?codigoOpcion=`);
	}
	

	obtenerInformacionAdicional(codigoPersona: number,codigoTablaComunesCabecera:number): Observable<Respuesta<InformacionAdicional[]>> {
		return this.http.get(`${environment.endpointPersonas}/informacionAdicional/${codigoPersona}/${codigoTablaComunesCabecera}`)
		.pipe(map((resp) => {
			const x: any = resp;
			const newResp: Respuesta<InformacionAdicional[]> = {
				codigo: 1,
				mensaje: x?.mensaje,
				resultado: []
			}

			x?.resultado?.forEach((res: any) => {
				res.estado = res.estado == '1'
				newResp.resultado.push(res)
			})

			return newResp;
		}));

	}

	insertarInformacionAdicional(insertarRequest: InformacionAdicional): Observable<Respuesta<InformacionAdicional[]>> {
		return this.http.post<Respuesta<InformacionAdicional[]>>(`${environment.endpointPersonas}/InformacionAdicional`, boolToChar(insertarRequest, ['estado']));
	}

	actualizarInformacionAdicional(actualizarRequest: InformacionAdicional): Observable<Respuesta<void>> {
		if (actualizarRequest.estado===false){
			actualizarRequest.observacion='.';
		}
		return this.http.put<Respuesta<void>>(`${environment.endpointPersonas}/InformacionAdicional`, boolToChar(actualizarRequest, ['estado']));
	}

}
