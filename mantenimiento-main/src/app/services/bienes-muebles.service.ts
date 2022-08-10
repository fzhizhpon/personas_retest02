import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Respuesta } from 'src/app/models/respuesta';
//import { bienMueble } from '../models/ingreso-socios.model';
//import { ObtenerBienesMuebles } from '../models/referencias-financieras.model';
import { BienMuebleMinResponse, BienMuebleResponse, EliminarBienMuebleRequest, InsertarBienMueble } from '../models/bien-mueble';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
	providedIn: 'root'
})
export class BienesMueblesService {

	constructor(private http: HttpClient)
	{

	}

	obtenerBienesMuebles(codigoPersona: number): Observable<Respuesta<BienMuebleResponse[]>> {
		return this.http.get<Respuesta<BienMuebleResponse[]>>(`${environment.endpointPersonas}/bienesmuebles/${codigoPersona}`);
	}


	obtenerBienMueble(codigoPersona: number, codigoBienMueble: number): Observable<any> {
		return this.http.get(`${environment.endpointPersonas}/BienesMuebles/${codigoPersona}/${codigoBienMueble}`);
	}

	guardarBienesMuebles(body: InsertarBienMueble): Observable<Respuesta<void>> {
		return this.http.post<Respuesta<void>>(`${environment.endpointPersonas}/BienesMuebles`, body);
	}

	eliminaBienesMuebles(body: EliminarBienMuebleRequest): Observable<Respuesta<BienMuebleResponse[]>> {
		const {codigoPersona, numeroRegistro}=body
		return this.http.put<Respuesta<BienMuebleResponse[]>>(`${environment.endpointPersonas}/BienesMuebles/estado`,{codigoPersona, numeroRegistro});
	}

	actualizaBienesMuebles(body: BienMuebleResponse): Observable<Respuesta<BienMuebleResponse[]>> {
		return this.http.put<Respuesta<BienMuebleResponse[]>>(`${environment.endpointPersonas}/BienesMuebles`, body);
	}

}
