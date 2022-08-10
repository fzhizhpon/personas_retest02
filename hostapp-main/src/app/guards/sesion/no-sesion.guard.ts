import { Injectable } from '@angular/core';
import { CanActivate, Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { LocalStorageService } from 'src/app/services/common/local-storage/local-storage.service';

@Injectable({
	providedIn: 'root'
})
export class NoSesionGuard implements CanActivate {

	constructor(
		private _storage: LocalStorageService,
		private _router: Router,
	) {	}

	canActivate(): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree
	{
		console.log('activateeeee');


		if (this._storage.getSesion() != null) {
			this._router.navigate(['inicio']);
			return false;
		}

		return true;
	}

}
