import { Injectable } from '@angular/core';
import { CanActivate, Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { LocalStorageService } from 'src/app/services/common/local-storage/local-storage.service';

@Injectable({
	providedIn: 'root'
})
export class SesionGuard implements CanActivate {

	constructor(
		private _storage: LocalStorageService,
		private _router: Router,
	) {	}

	canActivate(): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree
	{
		if (this._storage.getSesion() == null) {
			this._router.navigate(['autenticacion']);
			return false;
		}

		return true;
	}

}
