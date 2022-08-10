import {Component, Input, OnChanges, SimpleChanges} from '@angular/core';
import {PersonaService} from "../../services/persona.service";
import {MessageService} from 'src/app/services/common/message/message.service';
import {NzModalService} from 'ng-zorro-antd/modal';
import {DatosGeneralesComponent} from "../datos-generales/datos-generales.component";
import {environmentHost} from 'src/environments/environment';
import {DocumentologiaService} from "../../services/documentologia.service";

interface PersonaSticky {
	top?: string;
	left?: string;
	bottom?: string;
	right?: string;
}


@Component({
	selector: 'vs-persona-doc',
	templateUrl: './persona-doc.component.html',
	styleUrls: ['./persona-doc.component.scss']
})
export class VsPersonaDocComponent implements OnChanges {

	// * props componente
	isLoading = false;
	persona: any = {};
	cedulas: any = {};
	firma: any = {}

	// * props de inputs
	_codigoPersona!: number | null;
	_codigoTipoPersona!: number | null;

	@Input('codigoPersona')
	set codigoPersona(value: number | null) {
		if (value != null) {
			this._codigoPersona = value;
		}
	}

	@Input('codigoTipoPersona')
	set codigoTipoPersona(value: number | null) {
		if (value != null) {
			this._codigoTipoPersona = value;
		}
	}

	@Input('codigoMenorEdad') codigoMenorEdad: number | null = null;

	// * props de estilo
	@Input('vertical') vertical = true;
	@Input('width') width = '21.5rem';
	@Input('sticky') sticky = false;
	@Input('stickyValue') stickyPosition: PersonaSticky = {
		top: 'auto',
		left: 'auto'
	};


	constructor(
		private _personaService: PersonaService,
		private _documentologiaService: DocumentologiaService,
		private _messageService: MessageService,
		private _modalService: NzModalService
	) {
	}

	ngOnChanges(changes: SimpleChanges) {
		this.obtenerDatosPersona();
	}

	// * metodos para peticiones

	async obtenerDatosPersona() {
		if (this._codigoPersona != null && this._codigoTipoPersona != null) {
			this.isLoading = true;
			try {
				if (this._codigoTipoPersona === 1) {
					// * persona natural
					// ? codigoTipoDocumento: cedulas -> 4 firma -> 174
					const personaEncontrada = await this._personaService.ObtenerInfoPersona(this._codigoPersona).toPromise();
					this.persona = personaEncontrada.resultado;
				} else {
					// * persona no natural
					// ? codigoTipoDocumento: cedulas -> 4 firma -> 174
					const personaEncontrada = await this._personaService.ObtenerInfoPersonaNoNatural(this._codigoPersona).toPromise();
					this.persona = personaEncontrada.resultado;
				}
				// ? siempre se devolvera 2 objetos en un array
				const documentosCedula = await this._documentologiaService.obtenerDocumentosPorGrupo({
					codigoComponente: 1,
					codigoTipoDocumento: 4,
					codigoGrupo: 35,
					codigoPersona: this._codigoPersona
				}).toPromise();
				// ? siempre se devolvera 1 objetos
				const documentoFirma = await this._documentologiaService.obtenerDocumentosPorGrupo({
					codigoComponente: 1,
					codigoTipoDocumento: 174,
					codigoGrupo: 35,
					codigoPersona: this._codigoPersona
				}).toPromise();
				this.cedulas = documentosCedula.resultado;
				this.firma = documentoFirma.resultado;

			} catch (e: any) {
				this._messageService.showError(e.mensaje || e);
				this.cedulas = [];
				this.firma = [];
			} finally {
				this.isLoading = false;
			}


		}
	}

	// ! fin metodos para peticiones

	// * visualizar informacion del menor

	verInformacionMenor(codigoPersona: number) {
		this._modalService.create({
			nzContent: DatosGeneralesComponent,
			nzWidth: '140rem',
			nzTitle: 'Datos generales',
			nzComponentParams: {
				mostrarGuardar: false,
				_codigoPersona: codigoPersona
			},
			nzOkText: 'Más información',
			nzCancelText: 'Cerrar ventana',
			nzOnOk: () => {
				window.open(`${environmentHost}/mantenimiento/ingresosocios-mock?codigo=${codigoPersona}`);
			}
		});
	}

	// ! fin visualizar informacion del menor


}



