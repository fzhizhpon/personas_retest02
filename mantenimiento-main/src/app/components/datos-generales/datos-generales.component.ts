import {Component, EventEmitter, forwardRef, Output, Input} from '@angular/core';
import {ComponenteBasePersona} from '../base/componente-base-persona';
import {DatosGeneralesPersonaResponseAdapter} from '../../adapters/persona.adapter';
import {finalize} from 'rxjs/operators';
import {forkJoin, Observable, of, throwError} from 'rxjs';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {MessageService} from 'src/app/services/common/message/message.service';
import {persona} from '../../models/ingreso-socios.model';
import {PersonaService} from '../../services/personas.service';
import {IdentificacionValidadorService} from 'src/app/services/validadores/identificacion-validador.service';
import {validatorIdentificaction, validatorTipoIdentificaction} from '../../services/validarIdentificacion';
import {EdadService} from 'src/app/services/edad/edad.service';
import {RegisteredComponent} from 'src/app/components/common/registered.component';
import {SriService} from '../../services/sri';
import {differenceInCalendarDays} from 'date-fns';
import { NzModalService } from 'ng-zorro-antd/modal';

@Component({
	selector: 'pers-datos-generales',
	templateUrl: './datos-generales.component.html',
	styleUrls: ['./datos-generales.component.scss'],
	providers: [{
		provide: ComponenteBasePersona,
		useExisting: forwardRef(() => DatosGeneralesComponent)
	}],
})
export class DatosGeneralesComponent extends ComponenteBasePersona implements RegisteredComponent {

	nombreComponente = 'Datos Generales';

	readonly codigoComponente = 1;

	@Output('codigoPersonaChange') emitterPersona: EventEmitter<number> = new EventEmitter<number>();
	@Input('esConyugue') esConyugue = false;

	cargando: {
		listar: boolean;
		listarCompleto: boolean;
		guardar: boolean;
		busqueda: boolean;
	} = {
		listar: false,
		listarCompleto: false,
		guardar: false,
		busqueda: false
	};

	_formPersona: FormGroup;
	_formPersonaNatural: FormGroup;
	_formPersonaNoNatural: FormGroup;

	_tipoPersona = 1;
	dateFormat = "dd/MM/yyyy";
	edadCalculada = "";
	ruc = false;

	disabledDate = (current: Date): boolean => differenceInCalendarDays(current, new Date()) > 0 || differenceInCalendarDays(current, new Date()) < -37380;

	constructor(
		private personaService: PersonaService,
		private sriService: SriService,
		private _messageService: MessageService,
		private _identificacionValidadorService: IdentificacionValidadorService,
		public _calcularEdadService: EdadService,
		private _modalService: NzModalService,
	) {
		super();
		this._formPersona = this.IniciarFormPersona();
		this._formPersonaNatural = this.IniciarFormPersonaNatural();
		this._formPersonaNoNatural = this.IniciarFormPersonaNoNatural();

		this._formPersona.get('codigoPersona')?.disable();

		this._formPersona.valueChanges.subscribe(() => this.tieneCambios = true)
		this._formPersonaNatural.valueChanges.subscribe(() => this.tieneCambios = true)
		this._formPersonaNoNatural.valueChanges.subscribe(() => this.tieneCambios = true)
	}

	ObtenerTodo(): void {
		this._formPersona.get('codigoPersona')?.setValue(this.codigoPersona);
		this._formPersona.get('codigoPersona')?.disable();
		this._formPersonaNoNatural.get('razonSocial')?.disable();
		this._formPersonaNoNatural.get('objetoSocial')?.disable();
		this._formPersonaNoNatural.get('fechaConstitucion')?.disable();

		this.ObtenerDato(this.codigoPersona);
	}

	IniciarFormPersona() {
		return new FormGroup({
			codigoPersona: new FormControl(this.codigoPersona),
			codigoTipoIdentificacion: new FormControl(null, [Validators.required, validatorTipoIdentificaction]),
			codigoTipoPersona: new FormControl(1, [Validators.required]),
			numeroIdentificacion: new FormControl(null, [Validators.required, validatorIdentificaction]),
			observaciones: new FormControl(null),
			codigoTipoContribuyente: new FormControl(null, [Validators.required]),
			codigoDocumento: new FormControl(null, []),
		});
	}

	get numeroIdentificacion() {
		return this._formPersona.get('numeroIdentificacion') as FormControl;
	}

	IniciarFormPersonaNatural() {
		return new FormGroup({
			nombres: new FormControl(null, [Validators.required]),
			apellidoPaterno: new FormControl(null, [Validators.required]),
			apellidoMaterno: new FormControl(null, [Validators.required]),
			fechaNacimiento: new FormControl(null, [Validators.required]),
			tieneDiscapacidad: new FormControl(false),
			esVulnerable: new FormControl(false),
			codigoTipoDiscapacidad: new FormControl(null),
			porcentajeDiscapacidad: new FormControl(null),

			lugarNacimiento: new FormControl(null, [Validators.required]),
			codigoPaisNacimiento: new FormControl(null),
			codigoProvinciaNacimiento: new FormControl(null),
			codigoCiudadNacimiento: new FormControl(null),
			codigoParroquiaNacimiento: new FormControl(null),

			codigoTipoSangre: new FormControl(null, [Validators.required]),
			codigoEstadoCivil: new FormControl(null, [Validators.required]),
			codigoConyuge: new FormControl(null),
			codigoGenero: new FormControl(null, [Validators.required]),
			codigoProfesion: new FormControl(null, [Validators.required]),
			codigoTipoEtnia: new FormControl(null, [Validators.required]),
		});
	}

	IniciarFormPersonaNoNatural() {
		return new FormGroup({
			razonSocial: new FormControl(null, [Validators.required]),
			fechaConstitucion: new FormControl(null, [Validators.required]),
			objetoSocial: new FormControl(null, [Validators.required]),
			finalidadLucro: new FormControl(null, []),
			tipoSociedad: new FormControl(null, [Validators.required]),
			obligadoLlevarContabilidad: new FormControl(null, []),
			agenteRetencion: new FormControl(null, []),
			direccionWeb: new FormControl(null, []),
		});
	}

	Guardar() {
		this.GuardarCambios()
			.subscribe(data => {
				let codigoPersona;
				if (data.length == 2) {
					this._messageService.showSuccess(data[0].mensaje);
					this._messageService.showSuccess(data[1].mensaje);
					codigoPersona = data[0].resultado;
				} else {
					this._messageService.showSuccess(data.mensaje);
					codigoPersona = data.resultado;
				}

				if (codigoPersona == null) codigoPersona = this.codigoPersona

				this.codigoPersona = codigoPersona;
				this.emitterPersona.emit(codigoPersona)

				this.ObtenerTodo();
			}, (error) => this._messageService.showError(error.mensaje || error))
	}

	GuardarCambios(): Observable<any> {
		if (this.codigoPersona && !this.tieneCambios) return of({resultado: this.codigoPersona});

		let esValido = true;

		if (this._formPersona.invalid) {
			this._formPersona.markAllAsTouched()
			Object.values(this._formPersona.controls).forEach(control => {
				if (control.invalid) {
					control.markAsDirty();
					control.updateValueAndValidity({onlySelf: true});
				}
			});
			esValido = false;
		}

		if (this._formPersona.get('codigoTipoPersona')?.value === 1 && this._formPersonaNatural.invalid) {
			this._formPersonaNatural.markAllAsTouched()
			Object.values(this._formPersonaNatural.controls).forEach(control => {
				if (control.invalid) {
					control.markAsDirty();
					control.updateValueAndValidity({onlySelf: true});
				}
			});

			esValido = false;
		} else if (this._formPersona.get('codigoTipoPersona')?.value === 2 && this._formPersonaNoNatural.invalid) {
			this._formPersonaNoNatural.markAllAsTouched()
			Object.values(this._formPersonaNoNatural.controls).forEach(control => {
				if (control.invalid) {
					control.markAsDirty();
					control.updateValueAndValidity({onlySelf: true});
				}
			});

			esValido = false;
		}

		if (!esValido) return throwError({mensaje: 'Los campos marcados son obligatorios'});

		if (this._formPersona.get('codigoTipoPersona')?.value == 1) {
			return this.GuardarPersonaNatural();
		}
		if (this._formPersona.get('codigoTipoPersona')?.value == 2 && this.ruc === true) {
			return this.GuardarPersonaNoNatural();
		}

		if (this.ruc != true) return throwError('El RUC no pudo ser validado con el SRI')

		return throwError('No se selecciono el tipo de persona');
	}

	GuardarPersonaNatural(): Observable<unknown> {
		if (this._formPersonaNatural.invalid) {
			Object.values(this._formPersonaNatural.controls).forEach(control => {
				if (control.invalid) {
					control.markAsDirty();
					control.updateValueAndValidity({onlySelf: true});
				}
			});

			return throwError({mensaje: 'Los campos marcados son obligatorios'});
		}
		const lugarNacimiento = this._formPersonaNatural.get('lugarNacimiento')?.value;
		this._formPersonaNatural.get('codigoPaisNacimiento')?.setValue(lugarNacimiento.codigoPais)
		this._formPersonaNatural.get('codigoProvinciaNacimiento')?.setValue(lugarNacimiento.codigoProvincia)
		this._formPersonaNatural.get('codigoCiudadNacimiento')?.setValue(lugarNacimiento.codigoCiudad)
		this._formPersonaNatural.get('codigoParroquiaNacimiento')?.setValue(lugarNacimiento.codigoParroquia)

		const obj = this._formPersonaNatural.value;
		const objGeneral = this._formPersona.getRawValue();

		if (this.codigoPersona) {
			objGeneral.codigoPersona = this.codigoPersona;

			obj.codigoPersona = this.codigoPersona;

			return forkJoin([
				this.personaService.ActualizarPersona(<persona>(objGeneral)),
				this.personaService.ActualizarPersonaNatural(obj)
			])
		} else {
			return this.personaService.GuardarPersonaNatural(obj, objGeneral);
		}
	}

	GuardarPersonaNoNatural(): Observable<unknown> {
		if (this._formPersonaNoNatural.invalid) {
			Object.values(this._formPersonaNoNatural.controls).forEach(control => {
				if (control.invalid) {
					control.markAsDirty();
					control.updateValueAndValidity({onlySelf: true});
				}
			});
			return throwError('Los campos marcados son obligatorios');
		}

		const personaGeneral = this._formPersona.value;
		const personaNoNatural = this._formPersonaNoNatural.getRawValue()

		if (this.codigoPersona) {
			personaGeneral.codigoPersona = this.codigoPersona;
			personaNoNatural.codigoPersona = this.codigoPersona;

			return forkJoin([
				this.personaService.ActualizarPersona(personaGeneral),
				this.personaService.ActualizarPersonaNoNatural(personaNoNatural)
			]);
		} else if (this.ruc === true) {
			return this.personaService.GuardarPersonaNoNatural(personaGeneral, personaNoNatural)
		} else {
			return throwError('No se encuentra Registrado en SRI');
		}
	}

	ObtenerDato(codigo: number | null) {
		this.limpiarCampos();

		if (codigo == null) {
			return;
		}

		this.cargando.listar = true;
		this.cargando.listarCompleto = true;
		this.tieneCambios = false;

		this.personaService.ObtenerPersona(codigo)
			.pipe(finalize(() => this.cargando.listar = false))
			.subscribe((resp) => {
				if (resp) {
					const persona = new DatosGeneralesPersonaResponseAdapter(resp.resultado);
					this._formPersona.patchValue(persona);

					// Persona Natural
					if (resp.resultado.codigoTipoPersona == 1) {
						this.personaService.ObtenerPersonaNatural(codigo)
							.pipe(finalize(() => this.cargando.listarCompleto = false))
							.subscribe((resp) => {
								resp.resultado.lugarNacimiento = {
									codigoPais: resp.resultado.codigoPaisNacimiento,
									codigoProvincia: resp.resultado.codigoProvinciaNacimiento,
									codigoCiudad: resp.resultado.codigoCiudadNacimiento,
									codigoParroquia: resp.resultado.codigoParroquiaNacimiento
								}
								this._formPersonaNatural.patchValue(resp.resultado);
								this.edadCalculada = this._calcularEdadService.CalcularEdad(resp.resultado.fechaNacimiento);

								this._formPersona.get('numeroIdentificacion')?.disable()
								this._formPersona.get('codigoTipoIdentificacion')?.disable()
							});
					} else {
						this.personaService.ObtenerPersonaNoNatural(codigo)
							.pipe(finalize(() => this.cargando.listarCompleto = false))
							.subscribe((resp) => {
								this.edadCalculada = this._calcularEdadService.CalcularEdad(resp.resultado.fechaConstitucion);
								this.ruc = true
								this._formPersonaNoNatural.patchValue(resp.resultado);
							}, (error) => {
								this._messageService.showError(error.mensaje || error)
							});
					}

					setTimeout(() => this.tieneCambios = false, 100)
				} else {
					this.limpiarCampos();
				}
			}, (err) => {
				this._messageService.showError(err.mensaje || err)
			});
	}

	limpiarCampos() {
		this._formPersona.reset();
		this._formPersonaNatural.reset();
		this._formPersonaNoNatural.reset();

		this._formPersona.get('codigoTipoIdentificacion')?.enable()
	}

	validarEstadoCivil(codigoEstado: number) {
		if (codigoEstado === 1 || codigoEstado === 5) {
			if (this.esConyugue) {
				this._formPersonaNatural.get('codigoConyuge')?.setValidators([Validators.required]);
			}
		} else {
			this._formPersonaNatural.get('codigoConyuge')?.setValidators([]);
			this._formPersonaNatural.get('codigoConyuge')?.setValue(null);
		}
	}

	validarIdentificacion() {
		const persona = this._formPersona.getRawValue();

		const cod = persona.codigoTipoIdentificacion;

		if (!cod || !persona.numeroIdentificacion)
			return;

		if ((persona.numeroIdentificacion.length == 10 && cod == 1) ||
			(persona.numeroIdentificacion.length == 13 && cod == 2)) {
			if (this._identificacionValidadorService.EsIdentificacionValida(persona.numeroIdentificacion)) {
				this.verificarSiExistePersona(persona.numeroIdentificacion)
				validatorIdentificaction
			} else {
				this._messageService.showError('El tipo de documento: ' + persona.numeroIdentificacion + ' es incorrecta');
			}
		} else {
			this.verificarSiExistePersona(persona.numeroIdentificacion)
		}
	}

	verificarSiExistePersona(nroIdentificacion: string) {
		if (!nroIdentificacion || nroIdentificacion.length < 7) return;

		this.cargando.busqueda = true;

		this.personaService.BuscarPersonas('nroIdentificacion', nroIdentificacion)
			.pipe(finalize(() => {
				this.cargando.busqueda = false;
			}))
			.subscribe(api => {
				if (api.resultado.length > 0) {
					this.emitterPersona.emit(api.resultado[0].codigoPersona)
				} else {
					const tipoIdentificacion = this._formPersona.get('codigoTipoIdentificacion')?.value

					if (tipoIdentificacion == 2)
						this.ObtenerInformacionRucSRI(nroIdentificacion)
				}
			}, (error) => {
				this._messageService.showError(error.mensaje || error)
			})
	}

	calcularEdad(date: Date) {
		const anio = new Date(date).getFullYear();
		const mes = new Date(date).getMonth() + 1;
		const dia = new Date(date).getDate();

		this.edadCalculada = this._calcularEdadService.CalcularEdad(anio + "-" + mes + "-" + dia);
	}

	ObtenerInformacionRucSRI(ruc: string) {
		if (ruc.length < 13) return;

		this.sriService.obtenerContribuyente(ruc)
			.subscribe(data => {
				this.ruc = true;

				if (data.resultado !== null) {
					const datosSRI = data.resultado[0];
					const [dia, mes, anio] = datosSRI.informacionFechasContribuyente.fechaInicioActividades.split('/').map((data) => +data);
					this._formPersonaNoNatural.get('razonSocial')?.setValue(datosSRI.razonSocial);
					this._formPersonaNoNatural.get('objetoSocial')?.setValue(datosSRI.actividadContribuyente);
					this._formPersonaNoNatural.get('fechaConstitucion')?.setValue(new Date(anio, mes, dia));
					this._formPersonaNoNatural.get('obligadoLlevarContabilidad')?.setValue(datosSRI.obligado == "S");
					this.ruc = true;
				} else {
					this.ruc = false;
					this._formPersonaNoNatural.reset()
				}
			}, (err) => {
				this._modalService.confirm({
					nzTitle: 'Servicio SRI',
					nzContent: `${err.mensaje || err} <br> ¿Desea ingresar la información manualmente?`,
					nzOkText: 'Sí, ingresar manualmente',
					nzCancelText: 'Cancelar',
					nzOnCancel: () => {
						this._formPersona.reset();
						this._formPersonaNoNatural.reset();
					},
					nzOnOk: () =>
						this.ruc = true
					}
				);
			});
	}

	validarTipoDocumento() {
		const tipoDocumento = this._formPersona.get('codigoTipoIdentificacion')?.value
		const tipoPersona = this._formPersona.get('codigoTipoPersona')?.value

		if (tipoDocumento == null) return;

		this._formPersona.get('numeroIdentificacion')?.setValue(null)

		setTimeout(() => {
			if (tipoPersona === 2 && tipoDocumento !== 2) {
				this._formPersona.get('codigoTipoIdentificacion')?.setValue(2)
			} else if (tipoPersona === 1) {
				if (![1, 3, 4].includes(tipoDocumento)) {
					this._formPersona.get('codigoTipoIdentificacion')?.setValue(null)
					this._messageService.showError('El documento RUC no es válido para personas naturales')
				}
			}
		}, 0)
	}
}
