import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map, shareReplay, switchMap } from "rxjs/operators";
import { environmentHost } from 'src/environments/environment';
import { LocalStorageService } from '../../common/local-storage/local-storage.service';
import { AuthService } from '../../api/autenticacion/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor {

    constructor(
		private storage: LocalStorageService,
		private authService: AuthService,
	) { }

    intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
		const token = this.storage.getSesion()?.token;
        const prefix = environmentHost.gateway;

        if (!req.headers.has('Content-Type')) {
            if(!req.headers.has('Auto-header')) {
				req = req.clone({
					headers: req.headers.set('Content-Type', 'application/json')
				});
			}
        }

		if(!req.headers.has('informacionUsuario')) {
			req = req.clone({
				headers: req.headers.set('informacionUsuario', JSON.stringify(this.authService.infoTerminal))
			})
		}

		let separator = ''
		if(req.url.charAt(0) != '/') separator = '/'

        let request = req.clone({
            url: `${prefix}${separator}${req.url}`,
			withCredentials: false,
        });

        if (token) {
            request = this.addToken(request);
		}

		let responseError: any;

        return next.handle(request).pipe(
			shareReplay(),
			map((event) => {
				if (event instanceof HttpResponse) {
					if (event.body.codigo == -1) {
						responseError = event.body;
						throw new Error('API_ERROR');
					}
					event = event.clone({body: event.body});
				}
				return event;
			}),
            catchError((err: unknown) => {
                if (err instanceof HttpErrorResponse) {
					if(err.status === 401) {
						return this.handle401Error(request, next)
					}
					// Error de conexion o por parte del cliente (VPN, etc)
					else if (err.status === 0 && err.error instanceof ProgressEvent)
					{
						console.log('Client side error:', err.error)
						return throwError("Error de conexion");
					}

					return throwError("Error desconocido");
				}
				else {
					if (err instanceof Error) {
						if (err.message.replace('Error ', '') == 'API_ERROR') {
							return throwError(responseError)
						}
					}

					return throwError(err);
				}
            })
        );
    }

    private addToken(request: HttpRequest<unknown>) {
		const token = this.storage.getSesion().token ?? ''

        return request.clone({
            setHeaders: { 'Authorization': `Bearer ${token}` }
        });
    }

    private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
		console.log('Refresh token');

		return this.authService.RefrescarSesion()
		.pipe(
			switchMap((data) => {
				console.log('>>>> [REFRESH TOKEN]: Switch map');
				console.log(data);
				const sesion = this.storage.getSesion()
				sesion.token = data
				this.storage.setStorage('sesion', sesion)

				return next.handle(this.addToken(request))
			}),
			catchError((error) => {
				console.log('>>>> [REFRESH TOKEN]: Error');
				console.log(error);
				this.authService.CerrarSesion()
				return throwError("Your session has expired");
			})
		)
    }
}
