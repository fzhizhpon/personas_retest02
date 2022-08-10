import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Respuesta} from 'src/app/models/respuesta';
import {environment} from '../../environments/environment';
import {PersonaBusquedaDto} from '../models/persona';
import {PersonaMinResponse} from '../models/persona.model';
import {map} from 'rxjs/operators';
import {charToBool} from 'src/app/helpers/common.helper';


@Injectable({
	providedIn: 'root'
})
export class PersonaService {

	constructor(
		private _http: HttpClient,
	) {
	}

	ObtenerInfoPersona(codigoPersona: number) {
		return this._http.get<Respuesta<any>>(`${environment.endpointPersonas}/naturales/informacion/${codigoPersona}`);
	}

	ObtenerInfoPersonaNoNatural(codigoPersona: number) {
		return this._http.get<Respuesta<any>>(`${environment.endpointPersonas}/nonaturales/${codigoPersona}`)
			.pipe(map((resp) => {
				const x: any = resp;
				const newResp: any = {
					codigo: x.codigo,
					mensaje: x.mensaje
				}
				newResp['resultado'] = charToBool(x?.resultado,
					['finalidadLucro', 'obligadoLlevarContabilidad', 'agenteRetencion'])
				return newResp;
			}))
	}

	BuscarPersonas(filtro: string, busqueda: string | number) {
		const url = `${environment.endpointPersonas}?${filtro}=${busqueda}`;
		return this._http.get<Respuesta<PersonaBusquedaDto[]>>(url);
	}

	ObtenerPersonaMin(codigoPersona: number) {
		return this._http.get<Respuesta<PersonaMinResponse>>(`${environment.endpointPersonas}/min/${codigoPersona}`);
	}

}
