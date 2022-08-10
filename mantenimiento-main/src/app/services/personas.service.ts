import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { boolToChar, charToBool } from 'src/app/helpers/common.helper';
import { Respuesta } from 'src/app/models/respuesta';
import { environment } from '../../environments/environment';
import { persona, personaNatural } from '../models/ingreso-socios.model';
import { PersonaBusquedaDto } from '../models/persona';
import {
	PersonaMinResponse,
	PersonaNoNaturalRequest,
	PersonaNoNaturalResponse,
	PersonaResponse
} from '../models/persona.model';

@Injectable({
	providedIn: 'root'
})
export class PersonaService {

	constructor(
		private _http: HttpClient,
	) { }

	GuardarPersonaNatural(body1: persona, body2: personaNatural) {
		
		const request = Object.assign(body1, body2);
		return this._http.post<Respuesta<any>>(`${environment.endpointPersonas}/naturales`, boolToChar(request, ['tieneDiscapacidad', 'esVulnerable']));
	}

	ActualizarPersona(body: persona) {
		return this._http.put<Respuesta<any>>(`${environment.endpointPersonas}`, body);
	}

	ActualizarPersonaNatural(body: personaNatural) {
		const request = Object.assign(body);
		return this._http.put<Respuesta<any>>(`${environment.endpointPersonas}/naturales`, boolToChar(request, ['tieneDiscapacidad', 'esVulnerable']));
	}

	ObtenerPersona(codigoPersona: number | null) {
		return this._http.get<Respuesta<PersonaResponse>>(`${environment.endpointPersonas}/${codigoPersona}`);
	}

	ObtenerPersonaNatural(codigoPersona: number | null) {
		return this._http.get<Respuesta<any>>(`${environment.endpointPersonas}/naturales/${codigoPersona}`)
		.pipe(map(resp => {
			resp.resultado = charToBool(resp.resultado, ['tieneDiscapacidad', 'esVulnerable'])
			return resp;
		}));
	}

	GuardarPersonaNoNatural(datosGenerales: persona, personaNoNatural: PersonaNoNaturalRequest) {
		const persona = Object.assign(datosGenerales, personaNoNatural);
		return this._http.post<Respuesta<any>>(`${environment.endpointPersonas}/noNaturales`, boolToChar(persona, ['finalidadLucro', 'obligadoLlevarContabilidad', 'agenteRetencion']));
	}

	ActualizarPersonaNoNatural(body: PersonaNoNaturalRequest) {
		const request = Object.assign(body);
		return this._http.put<Respuesta<any>>(`${environment.endpointPersonas}/noNaturales`, boolToChar(request, ['finalidadLucro', 'obligadoLlevarContabilidad', 'agenteRetencion']));
	}

	ObtenerPersonaNoNatural(codigoPersona: number | null): Observable<Respuesta<PersonaNoNaturalResponse>> {
		return this._http.get(`${environment.endpointPersonas}/noNaturales/${codigoPersona}`)
		.pipe(map(resp => {
			const x: any = resp;

			const newResp: Respuesta<PersonaNoNaturalResponse> = {
				codigo: x.codigo,
				mensaje: x.mensaje,
				resultado: charToBool(x.resultado, ['finalidadLucro', 'obligadoLlevarContabilidad', 'agenteRetencion'])
			}

			return newResp;
		}));
	}

	ObtenerPersonaMinima(codigoPersona: number | null) :Observable<Respuesta<PersonaMinResponse>> {
		return this._http.get<Respuesta<PersonaMinResponse>>(`${environment.endpointPersonas}/min/${codigoPersona}`);
	}

	ObtenerPersonaMin(codigoPersona: number | null)  {
		return this._http.get<Respuesta<PersonaMinResponse>>(`${environment.endpointPersonas}/min/${codigoPersona}`);
	}

	ObtenerInfoPersona(codigoPersona: number) {
		return this._http.get<Respuesta<any>>(`${environment.endpointPersonas}/naturales/informacion/${codigoPersona}`);
	}

	BuscarPersonas(filtro: string, busqueda: string | number) {
		const url = `${environment.endpointPersonas}?${filtro}=${busqueda}`;
		return this._http.get<Respuesta<PersonaBusquedaDto[]>>(url);
	}

}
