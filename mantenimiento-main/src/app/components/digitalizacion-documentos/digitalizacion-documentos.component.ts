import { Cargando, ComponenteBasePersona } from '../base/componente-base-persona';
import { Component, EventEmitter, forwardRef, Input, Output } from '@angular/core';
import { DocumentoGrupoReponse, GrupoDocumentologiaResponse } from '../../models/documento';
import { DocumentologiaService } from '../../services/documentologia.service';
import { finalize } from 'rxjs/operators';
import { MessageService } from 'src/app/services/common/message/message.service';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ModalSubirDocumentoComponent } from './modal-subir-documento/modal-subir-documento.component';
import { of } from 'rxjs';

@Component({
	selector: 'app-digitalizacion-documentos',
	templateUrl: './digitalizacion-documentos.component.html',
	styleUrls: ['./digitalizacion-documentos.component.scss'],
	providers: [{
		provide: ComponenteBasePersona,
		useExisting: forwardRef(() => DigitalizacionDocumentosComponent)
	}]
})
export class DigitalizacionDocumentosComponent extends ComponenteBasePersona {

	cargando: Cargando | any = {
		listar: false,
		guardar: false,
		listarDocumentos: false
	};

	nombreComponente = 'Documentos Digitalizados';
	@Input('codigoComponente') codigoComponente!: number;
	@Output('onDocUpload') uploadDocEmitter: EventEmitter<void> = new EventEmitter<void>();

	documentos: DocumentoGrupoReponse[] = [];
	documentoSeleccionado!: DocumentoGrupoReponse | null;

	gruposDocumentos: GrupoDocumentologiaResponse[] = [];
	grupoSeleccionado!: GrupoDocumentologiaResponse | null;

	constructor(
		private _messageService: MessageService,
		private _documentoService: DocumentologiaService,
		private _modalService: NzModalService,
	) {
		super();
	}

	ObtenerTodo(): void {
		if (this.codigoComponente == null) {
			this._messageService.showError('No se ha indicado el componente a trabajar')
			return
		}

		this.InicializarVariables()

		this._documentoService.obtenerGruposDocumentos(this.codigoComponente)
			.subscribe(data => {
				this.gruposDocumentos = data.resultado

				if (data.resultado.length > 0) {
					this.grupoSeleccionado = data.resultado[0]
					this.listarDocumentos()
				}
			}, (error) => {
				this._messageService.showError(error.mensaje || error)
			})
	}

	InicializarVariables() {
		this.gruposDocumentos = []
		this.documentos = []
		this.grupoSeleccionado = null
		this.documentoSeleccionado = null
	}

	seleccionarArchivo(documento: DocumentoGrupoReponse) {
		this.documentoSeleccionado = documento
	}

	listarDocumentos() {
		if (this.grupoSeleccionado == null) return;
		if (this.codigoPersona == null) return;

		this.documentos = []
		this.documentoSeleccionado = null
		this.cargando.listarDocumentos = true

		console.log(this.codigoPersona + 1)

		this._documentoService.obtenerDocumentosPorGrupo({
			codigoComponente: this.codigoComponente,
			codigoGrupo: this.grupoSeleccionado.codigoGrupo,
			codigoTipoDocumento: this.grupoSeleccionado.codigoDocumento,
			codigoPersona: this.codigoPersona
		})
			.pipe(finalize(() => this.cargando.listarDocumentos = false))
			.subscribe(data => {
				this.documentos = data.resultado
				if (data.resultado.length > 0) {
					this.seleccionarArchivo(data.resultado[0])
				}
			}, (error) => {
				this._messageService.showError(error.mensaje || error)
			})
	}

	abrirModalSubirDocumento(grupo: GrupoDocumentologiaResponse) {
		if (this.codigoPersona === null) return;

		const modal = this._modalService.create({
			nzContent: ModalSubirDocumentoComponent,
			nzTitle: `SUBIR DOCUMENTOS: ${grupo.descripcionGrupo} - ${grupo.descripcionDocumento}`,
			nzComponentParams: {
				codigoPersona: this.codigoPersona,
				codigoComponente: this.codigoComponente,
				grupoDocumentos: grupo
			},
			nzCentered: true,
			nzWidth: '100rem',
			nzClosable: false,
			nzStyle: {
				'max-width': 'calc(100% - 5rem)',
				'maz-height': 'calc(100vh - 5rem)'
			}
		});

		modal.afterClose.subscribe(data => {
			if (data) {
				this.listarDocumentos()
				this.uploadDocEmitter.emit()
			}
		}, (error) => {
			this._messageService.showError(error.mensaje || error)
		})
	}

	GuardarCambios() {
		return of(0);
	}

	ResetearVista() {
		return;
	}

	async eliminarDocumento(idDocumento: any) {
		const archivoEliminado = await this._documentoService.eliminarDocumento(idDocumento).toPromise();
		this._messageService.showInfo(archivoEliminado.mensaje);
		this.ObtenerTodo();

	}

	async descargarDocumento(idDocumento: any, nombreArchivo:any) {
		const archivoDescargado = await this._documentoService.obtenerDocumento(idDocumento).toPromise();
		const linkSource = `data:application/${archivoDescargado.resultado.extension};base64,` + archivoDescargado.resultado.bytesFile;
		const downloadLink = document.createElement("a");
		const fileName = `${nombreArchivo}`;

		downloadLink.href = linkSource;
		downloadLink.download = fileName;
		downloadLink.type = `application/${archivoDescargado.resultado.extension}`

		downloadLink.click();
		this._messageService.showInfo(archivoDescargado.mensaje);


		this.ObtenerTodo();

	}

}
