import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';
import { Catalogo, Lugar } from 'src/app/components/catalogos/catalogo';
import filterBuilder from 'src/app/helpers/JsonHelper';
import { Respuesta } from 'src/app/models/respuesta';
import { environmentHost } from 'src/environments/environment';

@Injectable({
	providedIn: 'root'
})
export class CatalogoService {

	constructor(
		private _http: HttpClient
	) { }

	obtenerPorGet<T>(url: string, params?: unknown) {
		const finalUrl = filterBuilder(`${environmentHost.catalogos}/${url}`, params ?? {});
		return this._http.get<Respuesta<Catalogo<T>[]>>(finalUrl)
		.pipe(tap(data => {
			if(data.codigo != 0)
				throw data;

			return data;
		}));
	}

	obtenerPorPost<T>(url: string, params: unknown) {
		return this._http.post<Respuesta<Catalogo<T>[]>>(`${environmentHost.catalogos}/${url}`, params)
		.pipe(tap(data => {
			if(data.codigo != 0)
				throw data;

			return data;
		}));
	}

	obtenerLugares(params: any) {
		const url = filterBuilder(`${environmentHost.catalogos}/lugares`, params);

		return this._http.get<Respuesta<Lugar[]>>(url);
	}

}
