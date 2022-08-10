import {BotonAccion} from 'src/app/components/ui/panel-acciones/panel-acciones.class';
import {AfterViewInit, Component, QueryList, ViewChild, ViewChildren} from '@angular/core';
import {ComponenteBasePersona} from '../../components/base/componente-base-persona';
import {ModalBuscarPersonaService} from '../../components/modal-buscar-persona/modal-buscar-persona.service';
import {TabComponent} from 'src/app/components/ui/tabs/tab/tab.component';
import {finalize} from 'rxjs/operators';
import {ActivatedRoute, Router} from '@angular/router';
import {Location} from '@angular/common';
import {PersonaService} from "../../services/personas.service";
import {ReportesService} from "../../services/reportes.service";
import {MessageService} from 'src/app/services/common/message/message.service';
import {PersonaComponent} from '../../components/persona/persona.component';
import {EventosService} from '../../../../../../src/app/services/api/eventos/eventos.service';
import {Observable} from 'rxjs';

@Component({
	selector: 'app-ingreso-socios-mock',
	templateUrl: './ingreso-socios-mock.component.html',
	styleUrls: ['./ingreso-socios-mock.component.scss']
})
export class IngresoSociosMockComponent implements AfterViewInit {

	guardando = false;
	isLoading = false;
	errorDatosGenerales = false;

	registroCambios: {
		nombre: string,
		realizados: string[],
		errores: any[],
		finalizado: boolean
	}[] = [];

	tabSelected = 0;

	codigoPersona: number | null = null;
	codigoTipoPersona: number | null = null;
	codigoModulo = 1;
	opciones: BotonAccion[] = [
		{
			label: 'Nuevo',
			icono: 'file-add',
			click: (): void => {
				const url = this.router.createUrlTree([], {relativeTo: this.activatedRoute}).toString()
				this.location.go(url);
				window.location.reload();
			}
		},
		{
			label: 'Guardar',
			icono: 'save',
			click: (): void => {
				this.GuardarTodo();
			}
		},
		{
			label: 'Buscar',
			icono: 'search',
			click: (): void => {
				this.abrirBusquedaPersona()
			}
		},
		{
			label: 'Imprimir información',
			icono: 'printer',
			deshabilitado: true,
			click: (): void => {
				this.descargarReporte()
			}
		},
		{
			label: 'Salir',
			icono: 'logout',
			click: (): void => {
				this.salir()

			}
		}
	];

	@ViewChildren(ComponenteBasePersona) hojasDeTrabajo!: QueryList<ComponenteBasePersona>;
	@ViewChildren(TabComponent) tabs!: QueryList<TabComponent>;
	@ViewChild(PersonaComponent) persona!: PersonaComponent;

	constructor(
		private _modalService: ModalBuscarPersonaService,
		private activatedRoute: ActivatedRoute,
		private location: Location,
		private router: Router,
		private _personaService: PersonaService,
		private _reportesService: ReportesService,
		private _messageService: MessageService,
		private _eventosService: EventosService
	) {
	}

	ngAfterViewInit(): void {
		this.CambiarEstadoTabs(true)

		const subscription$: any = this.activatedRoute.queryParams
			.pipe(finalize(() => {
				subscription$.unsubscribe();
			}))
			.subscribe(params => {
				if (params.codigo == null) return;
				this.codigoPersona = params.codigo;

				this.CambiarEstadoTabs(false);
				this.toggleOpcionesMenu(false);
				this.obtenerpersonaMin();
			}, (error) => {
				this._messageService.showError(error.mensaje || error);
			});

	}

	toggleOpcionesMenu(estado: boolean) {
		this.opciones[3].deshabilitado = estado
	}

	async abrirBusquedaPersona(codigoPersona: number | null = null) {
		if (codigoPersona == null) {
			const persona = await this._modalService.abrirBusqueda();

			if (!persona) return;

			this.codigoPersona = persona.codigoPersona;
			this.codigoTipoPersona = persona.codigoTipoPersona;

			this.CambiarEstadoTabs(false)
			this.toggleOpcionesMenu(false)
			if (persona.codigoPersona != null) this.ColocarUrlPersona(persona.codigoPersona)
		} else {
			this.toggleOpcionesMenu(true)
			this.codigoPersona = codigoPersona;
			this.CambiarEstadoTabs(false)
			this.ColocarUrlPersona(codigoPersona)
		}

		this.obtenerpersonaMin();
	}

	GuardarTodo() {
		this.guardando = true;
		this.registroCambios = [{
			nombre: 'Datos Generales',
			realizados: [],
			errores: [],
			finalizado: false,
		}]

		this.hojasDeTrabajo.first.GuardarCambios()
			.pipe(finalize(() => {
				this.registroCambios[0].finalizado = true
				this.persona.ObtenerDatosPersona()
			}))
			.subscribe((api) => {
				this.errorDatosGenerales = false;
				if (this.codigoPersona == null) {
					this.abrirBusquedaPersona(api.resultado || api[0].resultado)
				}
				this.hojasDeTrabajo.forEach((hoja, i) => {
					if (i == 0) return;

					this.registroCambios.push({
						nombre: hoja.nombreComponente ?? `${i} - No indicado`,
						realizados: [],
						errores: [],
						finalizado: !hoja.tieneCambios,
					})

					if (!hoja.tieneCambios) return;

					hoja.GuardarCambios()
						.pipe(finalize(() => {
							this.registroCambios[i].finalizado = true
						}))
						.subscribe(
							(res) => {
								if (res.mensaje) {
									this.registroCambios[i].realizados.push(res.mensaje);
								}
							},
							(error) => {
								// * es un array cuando es un error de un validador back
								if (Array.isArray(error.resultado)) {
									// * obtenemos peticiones a realizar y campos con error de validaciones
									const {
										peticiones,
										campos
									} = this._eventosService.obtenerResultadosEventos(error.resultado);
									// * realizamos las validaciones

									peticiones.forEach(
										(peticion: Observable<unknown>, index: number) => {
											peticion
												.subscribe(
													(res2: any) => {
														this.registroCambios[i].errores.push(res2.resultado + campos[index]);
													},
													(err2: any) => {
														this.registroCambios[i].errores.push(err2 + campos[index]);
													})
										})
								} else {
									// * es un codigo de error
									this.registroCambios[i].errores.push(error.mensaje || error)
								}
							})
				})
			}, (error) => {
				this.errorDatosGenerales = true;
				this.registroCambios[0].errores.push(error.mensaje || error.erroMsg || error);
			})
	}

	CerrarVentanaGuardado() {
		let guardadoFinalizado = true;

		this.registroCambios.forEach(r => !r.finalizado ? guardadoFinalizado = false : '')

		if (!guardadoFinalizado) {
			const confirmado = confirm('Los cambios no se han terminado de guardar. ¿Desea continuar con la ejecución en segundo plano y salir?')
			if (confirmado) {
				this.guardando = false;
				return;
			}
		}

		this.guardando = false;
	}

	CambiarEstadoTabs(estado: boolean) {
		this.tabs.forEach((tab, i) => {
			if (i > 0) tab.disabled = estado;
		});
	}

	ColocarUrlPersona(codigo: number) {
		if (codigo == null) return;

		const url = this.router.createUrlTree([], {
			relativeTo: this.activatedRoute,
			queryParams: {codigo: codigo}
		}).toString()
		this.location.go(url);
	}

	async descargarReporte() {
		if (this.codigoPersona && this.codigoTipoPersona) {
			const loading = this._messageService.showLoading(`Generando reporte de la persona nro. ${this.codigoPersona}`)
			try {
				const resultado = await this._reportesService.reportesPersonas(this.codigoPersona, this.codigoTipoPersona).toPromise();

				const linkSource = 'data:application/pdf;base64,' + resultado.base64;
				const downloadLink = document.createElement("a");
				const fileName = `Per.No.${this.codigoPersona}.pdf`;

				downloadLink.href = linkSource;
				downloadLink.download = fileName;
				downloadLink.type = "application/pdf"

				downloadLink.click();

				this._messageService.removeLoading(loading)
				this._messageService.showSuccess(`Reporte de la persona nro. ${this.codigoPersona} generado exitosamente`)

			} catch (err: any) {
				this._messageService.removeLoading(loading)
				this._messageService.showError(err.mensaje || err)
			}
		}
	}

	obtenerpersonaMin() {
		if (!this.codigoPersona) return;

		const msgId = this._messageService.showLoading('Obteniendo información de la persona')
		let poseeError = false;
		this._personaService.ObtenerPersonaMin(this.codigoPersona)
			.pipe(finalize(() => {
				this._messageService.removeLoading(msgId)
				// if (error) this._messageService.showError(msg)
				if (!poseeError) {
					this._messageService.showSuccess('Información obtenida correctamente')
				}
			}))
			.subscribe(resp => {
				this.codigoTipoPersona = resp.resultado.codigoTipoPersona
				if (this.codigoTipoPersona == 2) this.tabSelected = 0
			}, (err: any) => {
				this._messageService.showError(err.mensaje);
				poseeError = true;
			})
	}

	recargarImagenes() {
		this.hojasDeTrabajo.first.ObtenerTodo()
		this.persona.ObtenerDatosPersona()
	}

	salir() {
		this.router.navigateByUrl("/inicio")
	}

}
