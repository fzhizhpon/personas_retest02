import {AfterViewInit, Component, forwardRef} from '@angular/core';
import * as Highcharts from 'highcharts';
import noData from 'highcharts/modules/no-data-to-display';
import exporting from 'highcharts/modules/exporting';
import exportData from 'highcharts/modules/export-data.js';
import {ElementoFormulario} from 'src/app/models/elemento-formulario';
import {EstadoFinanciero} from '../../models/estado-financiero';
import {EstadoFinancieroService} from '../../services/estado-financiero.service';
import {ComponenteBasePersona} from '../base/componente-base-persona';
import {AccionesFormulario} from 'src/app/enums/acciones-formulario.enum';
import {concat, Observable} from 'rxjs';
import {finalize} from 'rxjs/operators';
import {MessageService} from 'src/app/services/common/message/message.service';
import {RegisteredComponent} from 'src/app/components/common/registered.component';
import {RegisteredComponentsService} from 'src/app/services/common/registered-components/registered-components.service';
import {NzModalService} from 'ng-zorro-antd/modal';
import {EventosService} from '../../../../../../src/app/services/api/eventos/eventos.service';

noData(Highcharts);
exporting(Highcharts);
exportData(Highcharts);

Highcharts.setOptions({
	lang: {
		downloadJPEG: 'Descargar como JPEG',
		downloadPDF: 'Descargar como PDF',
		downloadPNG: 'Descargar como PNG',
		downloadSVG: 'Descargar como SVG',
		viewFullscreen: 'Ver pantalla completa',
		printChart: 'Imprimir',
		exitFullscreen: 'Salir de pantalla completa',
		downloadCSV: 'Descargar como csv',
		downloadXLS: 'Descargar como xlsx',
		viewData: 'Mostrar datos',
		hideData: 'Ocultar datos',
		noData: 'No hay datos que mostrar'
	},
});

@Component({
	selector: 'pers-estado-financiero',
	templateUrl: './estado-financiero.component.html',
	styleUrls: ['./estado-financiero.component.scss'],
	providers: [{
		provide: ComponenteBasePersona,
		useExisting: forwardRef(() => PersEstadoFinancieroComponent)
	}]
})
export class PersEstadoFinancieroComponent extends ComponenteBasePersona implements RegisteredComponent, AfterViewInit {

	readonly codigoComponente = 9;

	nombreComponente = 'Estado Financiero';
	tabSeleccionado = 0;
	_isEditable = false;
	cuentaEditar = '';
	indSeleccionado = -1;

	listaEstadoFinancieroA: ElementoFormulario<EstadoFinanciero>[] = [];
	listaEstadoFinancieroP: ElementoFormulario<EstadoFinanciero>[] = [];
	listaEstadoFinancieroI: ElementoFormulario<EstadoFinanciero>[] = [];
	listaEstadoFinancieroG: ElementoFormulario<EstadoFinanciero>[] = [];
	listaEstadoFinancieroT: ElementoFormulario<EstadoFinanciero>[] = [];

	isLoading = false;

	labels = {
		activosPasivos: {
			activos: 0,
			pasivos: 0
		},
		ingresosGastos: {
			ingresos: 0,
			gastos: 0
		}
	}

	formatterDollar = (value: number): string => `$ ${value ? value : ''}`;
	parserDollar = (value: string): string => value.replace('$ ', '');

	constructor(
		private estadoFinancieroService: EstadoFinancieroService,
		private _messageService: MessageService,
		private _registeredComponentService: RegisteredComponentsService,
		private _modal: NzModalService,
		private _eventosService: EventosService
	) {
		super();
	}

	ngAfterViewInit() {
		this.construirGraficas();
		this.tieneCambios = true;
	}

	ObtenerTodo(): void {
		this.ObtenerActivosPasivos();
		this.ObtenerIngresosGastos();
	}

	async ObtenerActivosPasivos() {
		try {
			this.cargando.listar = true;

			const activos = await this.ObtenerEstadoFinanciero('A');
			const pasivos = await this.ObtenerEstadoFinanciero('P');
			const patrimonio = await this.ObtenerEstadoFinanciero('T');

			this.listaEstadoFinancieroA = []
			this.listaEstadoFinancieroP = []
			this.listaEstadoFinancieroT = []

			activos?.resultado.forEach(el => {
				let accion;

				if (el.valor === null) {
					el.valor = 0
					accion = AccionesFormulario.Insertar
				} else {
					accion = AccionesFormulario.Actualizar
				}

				this.listaEstadoFinancieroA.push({objeto: el, accion: accion})
			});

			pasivos?.resultado.forEach(el => {
				let accion;

				if (el.valor === null) {
					el.valor = 0
					accion = AccionesFormulario.Insertar
				} else {
					accion = AccionesFormulario.Actualizar
				}

				this.listaEstadoFinancieroP.push({objeto: el, accion: accion})
			});

			patrimonio?.resultado.forEach(el => {
				let accion;

				if (el.valor === null) {
					el.valor = 0
					accion = AccionesFormulario.Insertar
				} else {
					accion = AccionesFormulario.Actualizar
				}

				this.listaEstadoFinancieroT.push({objeto: el, accion: accion})
			});

			this.obtenerOptionsGrafica1()
			this.construirGraficas();

		} catch (err: any) {
			this._messageService.showError(err.mensaje || err)
		} finally {
			this.cargando.listar = false;
		}
	}

	async ObtenerIngresosGastos() {
		try {
			this.cargando.listar = true;

			const ingresos = await this.ObtenerEstadoFinanciero('I');
			const gastos = await this.ObtenerEstadoFinanciero('G');

			this.listaEstadoFinancieroI = []
			this.listaEstadoFinancieroG = []

			ingresos?.resultado.forEach(el => {
				let accion;

				if (el.valor === null) {
					el.valor = 0
					accion = AccionesFormulario.Insertar
				} else {
					accion = AccionesFormulario.Actualizar
				}

				this.listaEstadoFinancieroI.push({objeto: el, accion: accion})
			});

			gastos?.resultado.forEach(el => {
				let accion;

				if (el.valor === null) {
					el.valor = 0
					accion = AccionesFormulario.Insertar
				} else {
					accion = AccionesFormulario.Actualizar
				}

				this.listaEstadoFinancieroG.push({objeto: el, accion: accion})
			});

			this.obtenerOptionsGrafica2()
			this.construirGraficas();

		} catch (err: any) {
			this._messageService.showError(err.mensaje || err)
		} finally {
			this.cargando.listar = false;
		}
	}

	obtenerOptionsGrafica1(): any {
		this.labels.activosPasivos = {
			activos: 0,
			pasivos: 0
		}
		this.listaEstadoFinancieroA.forEach(el => this.labels.activosPasivos.activos += el.objeto.valor)
		this.listaEstadoFinancieroP.forEach(el => this.labels.activosPasivos.pasivos += el.objeto.valor)

		// * redondeos
		this.labels.activosPasivos.activos = +this.labels.activosPasivos.activos.toFixed(2);
		this.labels.activosPasivos.pasivos = +this.labels.activosPasivos.pasivos.toFixed(2);

		return {
			chart: {
				type: 'bar',
			},
			title: {
				text: 'Situación Financiera'
			},
			xAxis: {
				categories: ['']
			},
			credits: {
				enabled: false
			},
			yAxis: {
				title: {
					text: ' $ USD'
				}
			},
			tooltip: {
				enabled: false
			},
			legend: {
				layout: 'vertical',
				align: 'right',
				verticalAlign: 'middle'
			},
			series: [
				{
					name: 'Activos',
					data: [this.labels.activosPasivos.activos],

				}, {
					name: 'Pasivos',
					data: [this.labels.activosPasivos.pasivos]
				}, {
					name: 'Patrimonio',
					data: [this.labels.activosPasivos.activos - this.labels.activosPasivos.pasivos]
				}],
			responsive: {
				rules: [{
					condition: {
						maxWidth: 500
					},
					chartOptions: {
						legend: {
							layout: 'horizontal',
							align: 'center',
							verticalAlign: 'bottom'
						}
					}
				}]
			},
			exporting: {
				buttons: {
					contextButton: {
						menuItems: [
							'viewFullscreen',
							'exitFullscreen',
							'printChart',
							'downloadPNG',
							'downloadXLS'
						]
					}
				}
			}
		}
	}

	obtenerOptionsGrafica2(): any {
		this.labels.ingresosGastos = {
			ingresos: 0,
			gastos: 0
		}
		this.listaEstadoFinancieroI.forEach(el => this.labels.ingresosGastos.ingresos += el.objeto.valor)
		this.listaEstadoFinancieroG.forEach(el => this.labels.ingresosGastos.gastos += el.objeto.valor)

		// * redondeos
		this.labels.ingresosGastos.ingresos = +this.labels.ingresosGastos.ingresos.toFixed(2);
		this.labels.ingresosGastos.gastos = +this.labels.ingresosGastos.gastos.toFixed(2);

		return {
			chart: {
				plotBackgroundColor: null,
				plotBorderWidth: null,
				plotShadow: false,
				type: 'pie'
			},
			title: {
				text: 'Situación Financiera'
			},
			colors: ['#FFC300', '#FF1818'],
			credits: {
				enabled: false
			},
			tooltip: {
				enabled: false
			},
			series: [
				{
					colorByPoint: true,
					data: [
						{
							name: `Ingresos (${this.labels.ingresosGastos.ingresos})`,
							y: this.labels.ingresosGastos.ingresos
						},
						{
							name: `Gastos (${this.labels.ingresosGastos.gastos})`,
							y: this.labels.ingresosGastos.gastos
						}
					]
				}
			],
			exporting: {
				buttons: {
					contextButton: {
						menuItems: [
							'viewFullscreen',
							'exitFullscreen',
							'printChart',
							'downloadPNG',
							'downloadXLS'
						]
					}
				}
			}
		}
	}

	construirGraficas() {

		setTimeout(() => {
			if (this.tabSeleccionado === 0) {
				Highcharts.chart('container', this.obtenerOptionsGrafica1());
			} else {
				Highcharts.chart('container-pie', this.obtenerOptionsGrafica2());
			}
		}, 2000)

	}

	cambioTab(event: any) {
		if (event === 0) {
			try {
				Highcharts.chart('container', this.obtenerOptionsGrafica1());
			} catch (err: any) {
				this._messageService.showError(err.mensaje || err)
			}

		} else {
			Highcharts.chart('container-pie', this.obtenerOptionsGrafica2());
		}
	}

	ObtenerEstadoFinanciero(estado: string) {
		if (this.codigoPersona != null) {
			this.isLoading = true;

			return this.estadoFinancieroService.obtenerEstadoFinanciero(this.codigoPersona, estado)
				.toPromise()
		}
	}

	GuardarBase() {
		this.GuardarCambios()
			.pipe(finalize(() => {
				this.tieneCambios = false;
			}))
			.subscribe((res) => {
				this._messageService.showSuccess(res.mensaje || "Datos guardados correctamente");
			}, (err) => {
				// * error de validadores
				if (Array.isArray(err.resultado)) {
					// * obtenemos peticiones a realizar y campos con error de validaciones
					const {peticiones, campos} = this._eventosService.obtenerResultadosEventos(err.resultado);
					peticiones.forEach(
						(peticion: Observable<unknown>, index: number) => {
							peticion
								.subscribe(
									(res2: any) => {
										this._messageService.showError(res2.resultado + campos[index]);
									},
									(err2: any) => {
										this._messageService.showError(err2 + campos[index]);
									})
						})
				} else {
					// * error de repos y servicios back
					this._messageService.showError(err.mensaje || err);
				}
			});
	}

	GuardarCambios(): Observable<any> {
		this.cargando.guardar = true;
		const operaciones: Observable<unknown>[] = [];

		this.listaEstadoFinancieroA.forEach((elemento) => {
			if (elemento.objeto.recursoExterno) return;

			if (elemento.objeto.valor === null) elemento.objeto.valor = 0
			elemento.objeto.codigoPersona = this.codigoPersona || 0

			const {codigoComponente, descripcion, recursoExterno, ...resto} = elemento.objeto;
			if (elemento.accion === AccionesFormulario.Actualizar) {
				operaciones.push(this.estadoFinancieroService.actualizarEstadoFinanciero(resto));
			} else {
				operaciones.push(this.estadoFinancieroService.insertarEstadoFinanciero(resto))
			}
		});

		this.listaEstadoFinancieroP.forEach((elemento) => {
			if (elemento.objeto.recursoExterno) return;

			if (elemento.objeto.valor === null) elemento.objeto.valor = 0
			elemento.objeto.codigoPersona = this.codigoPersona || 0

			const {codigoComponente, descripcion, recursoExterno, ...resto} = elemento.objeto;
			if (elemento.accion === AccionesFormulario.Actualizar)
				operaciones.push(this.estadoFinancieroService.actualizarEstadoFinanciero(resto));
			else
				operaciones.push(this.estadoFinancieroService.insertarEstadoFinanciero(resto))
		});

		this.listaEstadoFinancieroI.forEach((elemento) => {
			if (elemento.objeto.recursoExterno) return;

			if (elemento.objeto.valor === null) elemento.objeto.valor = 0
			elemento.objeto.codigoPersona = this.codigoPersona || 0
			const {codigoComponente, descripcion, recursoExterno, ...resto} = elemento.objeto;
			if (elemento.accion === AccionesFormulario.Actualizar)
				operaciones.push(this.estadoFinancieroService.actualizarEstadoFinanciero(resto));
			else
				operaciones.push(this.estadoFinancieroService.insertarEstadoFinanciero(resto))
		});

		this.listaEstadoFinancieroG.forEach((elemento) => {
			if (elemento.objeto.recursoExterno) return;

			if (elemento.objeto.valor === null) elemento.objeto.valor = 0
			elemento.objeto.codigoPersona = this.codigoPersona || 0
			const {codigoComponente, descripcion, recursoExterno, ...resto} = elemento.objeto;
			if (elemento.accion === AccionesFormulario.Actualizar)
				operaciones.push(this.estadoFinancieroService.actualizarEstadoFinanciero(resto));
			else
				operaciones.push(this.estadoFinancieroService.insertarEstadoFinanciero(resto))
		});

		return concat(...operaciones)
			.pipe(finalize(() => {
				this.cargando.guardar = false;
				this.ObtenerTodo();
				this.construirGraficas();
			}));
	}

	async abrirModalComponente(codigoComponente: number | null) {
		if (codigoComponente) {
			const componente = await this._registeredComponentService.obtenerComponente(codigoComponente)

			try {
				const modal = this._modal.create({
					nzContent: <any>(componente),
					nzTitle: '.',
					nzComponentParams: {
						mostrarGuardar: false,
						_codigoPersona: this.codigoPersona,
					},
					nzWidth: '140rem',
					nzOkText: 'Guardar cambios',
					nzOnOk: async (comp: ComponenteBasePersona) => {
						modal.updateConfig({nzOkLoading: true})

						try {
							await comp.GuardarCambios().toPromise()

							modal.close()
							this.ObtenerTodo()
						} catch (error: any) {
							this._messageService.showError(error.mensaje || JSON.stringify(error))
							return false;
						} finally {
							modal.updateConfig({nzOkLoading: false})
						}
					}
				})
			} catch {
				this._messageService.showError('El componente indicado no puede ser visualizado')
			}
		}
	}
}
