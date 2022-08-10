import {Component} from '@angular/core';
import {SeguridadService} from '../../shared/services/seguridad/seguridad.service';
import {ParametrosService} from '../../shared/services/parametros/parametros.service';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {finalize} from 'rxjs/operators';
import {LoadingService} from 'src/app/services/common/loading/loading.service';
import {LocalStorageService} from 'src/app/services/common/local-storage/local-storage.service';
import {Router} from '@angular/router';
import {MessageService} from 'src/app/services/common/message/message.service';
import {RolResponse} from '../../models/rol';
import {NzModalService} from 'ng-zorro-antd/modal';

@Component({
	selector: 'app-acceso',
	templateUrl: './acceso.component.html',
	styleUrls: ['./acceso.component.scss']
})
export class AccesoComponent {

	idiomas = [
		{
			prefix: 'es',
			label: 'Español',
			icon: 'https://raw.githubusercontent.com/hampusborgos/country-flags/main/svg/ec.svg',
		},
		{
			prefix: 'en',
			label: 'English',
			icon: 'https://raw.githubusercontent.com/hampusborgos/country-flags/main/svg/us.svg',
		},
	]

	langPrefix!: {
		prefix: string,
		label: string,
		icon: string,
	};

	formLogin: FormGroup = new FormGroup({});

	roles: RolResponse[] = [];

	nombreEmpresa = "";

	loadings = {
		roles: false,
		acceder: false,
	};

	loginError = "";
	mostrarError = false;

	constructor(
		private _seguridadService: SeguridadService,
		private _parametrosService: ParametrosService,
		private _loadingService: LoadingService,
		private _storage: LocalStorageService,
		private _router: Router,
		private _messageService: MessageService,
		private _modal: NzModalService,
	) {
		this.crearFormulario()

		this.obtenerDatosEmpresa()

		const langPrefix = this._storage.getStorage('lang_prefix') || 'es';
		this.idiomas.forEach(idioma => {
			if (idioma.prefix == langPrefix) this.langPrefix = idioma
		});
	}

	cambiarIdioma(lang: { prefix: string }): void {
		this._storage.setStorage('lang_prefix', lang.prefix)
		window.location.reload()
	}

	crearFormulario(): void {
		this.formLogin = new FormGroup({
			usuario: new FormControl(null, [Validators.required]),
			clave: new FormControl(null, [Validators.required]),
			rol: new FormControl(null, [Validators.required]),
			forzarCierreSesiones: new FormControl(false, []),
		})
	}

	cargarDatosFormulario(): void {
		if (this.formLogin.get('cedula')?.invalid) return
		this.mostrarError = false
		this.loginError = ""

		const usuario = this.formLogin.get('usuario')?.value
		this.obtenerRoles(usuario)
	}

	obtenerRoles(usuario: string) {
		this.roles = []
		this.formLogin.get('rol')?.setValue(null)

		if (this.formLogin.get('usuario')?.invalid) return

		this.loadings.roles = true

		this._seguridadService.obtenerRoles(usuario)
			.pipe(finalize(() => this.loadings.roles = false))
			.subscribe(api => {
				this.roles = api.resultado
				if (this.roles.length > 0) {
					this.formLogin.get('rol')?.setValue(this.roles[0])
				} else {
					this.colocarError('-0002-01-25 - No se encontraron roles para el usuario ' + this.formLogin.get('usuario')?.value);
				}
			}, (err) => {
				this.colocarError(err.mensaje || 'Usuario ingresado no posee roles');
			})
	}

	restablecerFormulario(): void {
		this.formLogin.reset()
	}

	obtenerDatosEmpresa(): void {
		this._loadingService.show()

		this._parametrosService.obtenerEmpresa()
			.pipe(finalize(() => this._loadingService.hide()))
			.subscribe(
				data => {
					this.formLogin.get('codigoEmpresa')?.setValue(data.resultado.codigoEmpresa)
					this.nombreEmpresa = data.resultado.nombre
				},
				error => {
					this.colocarError(error.mensaje || 'Error desconocido. Comuniquese con soporte')
				}
			);
	}

	loginCedula(): void {
		this.mostrarError = false

		if (this.formLogin.invalid) {
			if (this.loginError.length > 0) {
				this.mostrarError = true
			}
			this.formLogin.markAllAsTouched()
			return;
		}

		this.loadings.acceder = true

		const form = this.formLogin.getRawValue();

		const loginDto = {
			codigoRol: form.rol.codigoRol,
			codigoAgencia: form.rol.codigoAgencia,
			usuario: form.usuario,
			clave: form.clave,
			forzarCierreSesiones: form.forzarCierreSesiones == null ? false : form.forzarCierreSesiones
		}

		this._seguridadService.iniciarSesion(loginDto)
			.pipe(finalize(() => this.loadings.acceder = false))
			.subscribe(
				data => {
					this._storage.setStorage('sesion', {
						codigoUsuario: 1,
						usuario: form.usuario,
						token: data.resultado.token,
						informacion: data.resultado.informacion
					})
					this._router.navigate(['inicio'])

					if (data.resultado.mensajes?.length > 0) {
						let advertencias = "";

						data.resultado.mensajes.forEach(m => {
							if (m) advertencias += `${m} </br>`
						})

						if (advertencias.length > 0) {
							this._modal.warning({
								nzTitle: 'Alerta',
								nzContent: advertencias
							});
						}
					}
				},
				error => {
					this.colocarError(error.mensaje)
				}
			);
	}

	colocarError(error: string): void {
		this.loginError = error
		this.mostrarError = true
	}

}
