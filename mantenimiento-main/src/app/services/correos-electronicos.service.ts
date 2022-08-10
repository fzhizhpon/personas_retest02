import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { boolToChar } from 'src/app/helpers/common.helper';
import { Respuesta } from 'src/app/models/respuesta';
import { environment } from '../../environments/environment';
import { CorreoElectronicoInDto } from '../models/correo-electronico';
import { correoElectronico } from '../models/ingreso-socios.model';
import { ObtenerCorreos } from '../models/referencias-financieras.model';

@Injectable({
	providedIn: 'root'
})
export class CorreosElectronicosService {

	constructor(
		private _http: HttpClient,
	) { }

	ObtenerCorreos(codigoPersona: number) {
		return this._http
		.get<Respuesta<CorreoElectronicoInDto<string>[]>>(`${environment.endpointPersonas}/CorreosElectronicos?codigoPersona=${codigoPersona}`)
	}
	obtenerCorreosElectronicos(body : ObtenerCorreos):Observable<Respuesta<correoElectronico[]>>
	{
		return this._http.get<Respuesta<correoElectronico[]>>(`${environment.endpointPersonas}/CorreosElectronicos?codigoPersona=${body.codigoPersona}`);
	}


	guardarCorreosElectronicos(body: correoElectronico): Observable<Respuesta<correoElectronico[]>>
	{
		return this._http.post<Respuesta<correoElectronico[]>>(`${environment.endpointPersonas}/CorreosElectronicos`, boolToChar(body, [ 'esPrincipal']));
	}

	eliminaCorreosElectronicos(body: correoElectronico): Observable<Respuesta<correoElectronico[]>>
	{
		return this._http.delete<Respuesta<correoElectronico[]>>(`${environment.endpointPersonas}/CorreosElectronicos?codigoPersona=${body.codigoPersona}&codigoCorreoElectronico=${body.codigoCorreoElectronico}`);
	}

	actualizaCorreosElectronicos(body: correoElectronico): Observable<Respuesta<correoElectronico[]>>
	{
		return this._http.put<Respuesta<correoElectronico[]>>(`${environment.endpointPersonas}/CorreosElectronicos`, boolToChar(body, [ 'esPrincipal']));
	}

}
