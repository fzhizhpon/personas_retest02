import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Respuesta } from 'src/app/models/respuesta';
import { environmentHost } from 'src/environments/environment';
import Language from '../../../../../language.json';
import { LocalStorageService } from '../local-storage/local-storage.service';

@Injectable({
	providedIn: 'root'
})
export class LanguagesService {

	constructor(
		private _http: HttpClient,
		private _storage: LocalStorageService,
	) { }

	// get(stringKeys: string): string {
	// 	const langPrefix: string = <string>(this._storage.getStorage('lang_prefix')) || 'es';

	// 	stringKeys = stringKeys.replace(/\$lang\$/g, langPrefix);

	// 	const keys = stringKeys.split(':');

	// 	let json: never = <never>(Language['ui'])

	// 	keys.forEach(key => json = json[key] )

	// 	return json;
	// }

	get(stringKeys: string): Observable<Respuesta<string>> {
		const keys = stringKeys.split(':')
		const componente = keys[0]
		keys.shift()

		const langPrefix: string = <string>(this._storage.getStorage('lang_prefix')) || 'es';

		const params = `lang=${langPrefix}&componente=${componente}&label=${keys.join(':')}`

		return this._http.get<Respuesta<string>>(`${environmentHost.idioma}/componentes?${params}`)
	}

}
