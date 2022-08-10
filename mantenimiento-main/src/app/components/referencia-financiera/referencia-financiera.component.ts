import {Component, forwardRef} from '@angular/core';
import {ComponenteBasePersona} from '../base/componente-base-persona';
import {finalize} from 'rxjs/operators';
import {concat, Observable, of} from 'rxjs';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {referenciaFinanciera} from '../../models/ingreso-socios.model';
import {MessageService} from 'src/app/services/common/message/message.service';
import {ReferenciaFinancieraService} from '../../services/referencia-financiera.service';
import {ElementoFormulario} from 'src/app/models/elemento-formulario';
import {AccionesFormulario} from 'src/app/enums/acciones-formulario.enum';
import {RegisteredComponent} from 'src/app/components/common/registered.component';
import {differenceInCalendarDays} from 'date-fns';
import {CatalogoService} from 'src/app/services/api/catalogos/catalogo.service';
import {Catalogo} from 'src/app/components/catalogos/catalogo';

@Component({
	selector: 'pers-referencia-financiera',
	templateUrl: './referencia-financiera.component.html',
	styleUrls: ['./referencia-financiera.component.scss'],
	providers: [{
		provide: ComponenteBasePersona,
		useExisting: forwardRef(() => ReferenciaFinancieraComponent)
	}]
})

export class ReferenciaFinancieraComponent extends ComponenteBasePersona implements RegisteredComponent {

	readonly codigoComponente = 3;

	nombreComponente = 'Referencias Financieras';
	form: FormGroup = this.IniFormReferenciaFinanciera();
	_accion = 'Ingresar';
	indSeleccionado = -1;
	disabledDate = (current: Date): boolean => differenceInCalendarDays(current, new Date()) > 0 || differenceInCalendarDays(current, new Date()) <-37380;
	listaReferenciaFinanciera: ElementoFormulario<referenciaFinanciera>[] = [];
	dateFormat = "dd/MM/yyyy";

	formatterDollar = (value: number): string => `$ ${value ? value : ''}`;
	parserDollar = (value: string): string => value.replace('$ ', '');

	tiposIntitucionesFinancieras: Catalogo<number>[] = []

	constructor(
		private referenciaFinancieraService: ReferenciaFinancieraService,
		private _messageService: MessageService,
		private _catalogoService: CatalogoService,
	) {
		super();
		this._catalogoService.obtenerPorGet<number>('institucionesfinancieras/full')
			.subscribe(api => {
				this.tiposIntitucionesFinancieras = api.resultado
			})

	}

	ObtenerTodo() {
		this.tieneCambios = false;

		if (this.codigoPersona != null) {
			this.cargando.listar = true

			this.listaReferenciaFinanciera = [];
			this.LimpiarCampos();
			this.form.reset();

			const filtros = {
				//codigoReferenciaFinanciera: 3,
				numeroRegistro: 3,
				codigoPersona: this.codigoPersona,
				paginacion: {
					indiceInicial: 0,
					numeroRegistros: 50
				}
			}

			this.cargando.listar = true
			this.referenciaFinancieraService.obtenerReferenciasFinancieras(filtros)
				.pipe(finalize(() => this.cargando.listar = false))
				.subscribe(
					data => {
						data.resultado.map((ref) => {
							this.listaReferenciaFinanciera.push({objeto: ref, accion: AccionesFormulario.Leer});
						});

						this.ResetearVista();
					},
					error => {
						this._messageService.showError(error.mensaje || error)
					}
				);
		}
	}

	IngresarFila() {
		if (this.form.invalid) {
			this._messageService.showError('Los campos marcados son obligatorios')
			Object.values(this.form.controls).forEach(control => {
				if (control.invalid) {
					control.markAsDirty();
					control.updateValueAndValidity({onlySelf: true});
				}
			});
			return;
		}

		const referencia = this.form.getRawValue();
		if(referencia.saldoObligacion===null || referencia.saldoObligacion===''){
			referencia.saldoObligacion=0;
		}
		if(referencia.obligacionMensual===null || referencia.saldoObligacion===''){
			referencia.obligacionMensual=0
		}
		if (this.indSeleccionado == -1) {
			this.listaReferenciaFinanciera.push({objeto: referencia, accion: AccionesFormulario.Insertar});
		} else {
			this.listaReferenciaFinanciera[this.indSeleccionado] = {
				objeto: referencia,
				accion: AccionesFormulario.Actualizar
			};
		}

		this.tieneCambios = true;
		this.ResetearVista();
	}

	EditarFila(elemento: ElementoFormulario<referenciaFinanciera>, index: number) {
		this._accion = 'Actualizar';
		if (elemento.accion == AccionesFormulario.Insertar) {
			this.form.patchValue(elemento.objeto);
			this.listaReferenciaFinanciera.splice(index, 1);
		} else {
			this.indSeleccionado = index;
			this.referenciaFinancieraService.obtenerReferenciaFinaniera(elemento.objeto)
				.pipe(finalize(() => null))
				.subscribe(
					data => {


						this.listaReferenciaFinanciera[index].objeto = data.resultado;
						const {codigoInstitucionFinanciera, ...resto} = data.resultado;
						if(resto.saldoObligacion===null){
							resto.saldoObligacion=0;
						}
						if(resto.obligacionMensual===null){
							resto.obligacionMensual=0
						}
						this.form.patchValue(resto);

						// * desactivamos los campos que no pueden ser modificados
						this.form.get('codigoTipoInstitucionFinanciera')?.disable();
						this.form.get('codigoTipoCuentaFinanciera')?.disable();
						this.form.get('numeroCuenta')?.disable();
						this.form.get('fechaCuenta')?.disable();

						setTimeout(() => {
							// * retraso para que las instituciones esten cargados
							this.form.get('codigoInstitucionFinanciera')?.setValue(codigoInstitucionFinanciera);
							this.form.get('codigoInstitucionFinanciera')?.disable();
						}, 0);
					},
					error => {
						this._messageService.showError(error.mensaje || error)
					}
				);
		}
	}

	EliminarFila(index: number) {
		const referencia = this.listaReferenciaFinanciera[index]

		if (referencia.accion == AccionesFormulario.Insertar) {
			this.listaReferenciaFinanciera.splice(index, 1);
		} else {
			referencia.accion = AccionesFormulario.Eliminar;
		}

		this.tieneCambios = true;
	}

	GuardarBase() {
		const message = this._messageService.showLoading(`Guardando información: REFERENCIAS FINANCIERAS - Codigo persona: ${this.codigoPersona}`)

		this.GuardarCambios()
			.pipe(finalize(() => this._messageService.removeLoading(message)))
			.subscribe((resp) => {
				this._messageService.showSuccess('Datos guardados correctamente');
			}, (err) => {
				this._messageService.showError(err.mensaje || err);
			});
	}

	GuardarCambios(): Observable<unknown> {
		if (!this.tieneCambios) return of(0);

		this.cargando.guardar = true;

		const operaciones: Observable<unknown>[] = [];

		this.listaReferenciaFinanciera.forEach((elemento) => {

			const referencia = elemento.objeto;

			if (this.codigoPersona) referencia.codigoPersona = parseInt(this.codigoPersona.toString());
			switch (elemento.accion) {
				case AccionesFormulario.Insertar:
					const { numeroRegistro, ...resto } = referencia;

					operaciones.push(this.referenciaFinancieraService.guardarReferenciasFinancieras(resto))
					break;

				case AccionesFormulario.Actualizar:
					operaciones.push(this.referenciaFinancieraService.actualizaReferenciasFinancieras(referencia))
					break;

				case AccionesFormulario.Eliminar:

					operaciones.push(this.referenciaFinancieraService.eliminaReferenciasFinancieras(referencia))
					break;
			}
		});

		return concat(...operaciones)
			.pipe(finalize(() => {
				this.cargando.guardar = false
				this.ObtenerTodo();
			}));
	}

	IniFormReferenciaFinanciera() {
		return new FormGroup({
			numeroRegistro: new FormControl(null),
			codigoPersona: new FormControl(this.codigoPersona),

			codigoTipoInstitucionFinanciera: new FormControl(null, [Validators.required]),
			codigoInstitucionFinanciera: new FormControl(null, [Validators.required]),
			codigoTipoCuentaFinanciera: new FormControl(null, [Validators.required]),
			numeroCuenta: new FormControl(null, [Validators.required]),
			cifras: new FormControl(null, [Validators.required]),
			fechaCuenta: new FormControl(null, [Validators.required]),
			obligacionMensual: new FormControl(0, []),
			saldoObligacion: new FormControl(0, []),
			saldo: new FormControl(0, [Validators.required]),
			observaciones: new FormControl(null, ),


		});
	}

	LimpiarCampos() {
		this.form.reset()
		this.form.get('codigoTipoInstitucionFinanciera')?.enable();
		this.form.get('codigoTipoCuentaFinanciera')?.enable();
		this.form.get('numeroCuenta')?.enable();
		this.form.get('fechaCuenta')?.enable();
		this.form.get('codigoInstitucionFinanciera')?.enable();
	}

	ResetearVista(): void {
		this.LimpiarCampos();
		this._accion = 'Insertar';
		this.indSeleccionado = -1;
	}

	ObtenerNombreInstitucionFinancieraPorCodigo(codigo: number) {
		return this.tiposIntitucionesFinancieras.find(p => p.codigo === codigo)?.descripcion || codigo
	}

}
