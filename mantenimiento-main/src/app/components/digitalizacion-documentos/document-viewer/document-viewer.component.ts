import { Component, Input, ViewChild } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { PdfViewerComponent } from 'ng2-pdf-viewer';
import { DocumentoResponse } from '../../../models/documento';
import { DocumentologiaService } from '../../../services/documentologia.service';
import { IMAGES_EXTENSIONS, SUPPORTED_EXTENSIONS } from './files_extensions';
import { MessageService } from 'src/app/services/common/message/message.service';

@Component({
	selector: 'vs-document-viewer',
	templateUrl: './document-viewer.component.html',
	styleUrls: ['./document-viewer.component.scss']
})
export class DocumentViewerComponent {

	codigoDocumento!: string | null;
	documento: DocumentoResponse | null = null;
	mostrarPdfPantallaCompleta = false;
	busquedaPdf!: string;

	url = '';
	soportado = false;
	esImagen = false;

	@Input('codigoDocumento')
	set _codigoDocumento(value: string | null) {
		this.documento = null;
		if (value != null) {
			this.codigoDocumento = value;
			this.CargarDocumento()
		}
	}

	@Input('nombreDocumento') nombreDocumento!: string | null;
	@Input('widthImage') widthImage !: string | null;

	@ViewChild(PdfViewerComponent) private pdfComponent!: PdfViewerComponent;

	constructor(
		private _documentoService: DocumentologiaService,
		private _domSanitizer: DomSanitizer,
		private _messageService: MessageService,
	) { }

	CargarDocumento() {
		if (this.codigoDocumento == null) return;

		this.InicializarVariables()

		this._documentoService.obtenerDocumento(this.codigoDocumento)
		.subscribe(data => {
			this.documento = data.resultado
			this.VisualizarDocumento()
		}, (error) => {
			this._messageService.showError(error.mensaje || error)
		})
	}

	InicializarVariables() {
		this.documento = null
		this.url = ''
		this.esImagen = false
		this.soportado = false
	}

	VisualizarDocumento() {
		if(this.documento == null) return;

		if(SUPPORTED_EXTENSIONS.includes(this.documento.extension)) {
			this.soportado = true;

			this.esImagen = IMAGES_EXTENSIONS.includes(this.documento.extension)

			if (!this.esImagen) {
				this.mostrarPdfPantallaCompleta = false;
				this.search('')
			}

			this.url = `data:${this.documento.format};base64,${this.documento.bytesFile}`;
		} else {
			this.soportado = false;

			this.DescargarDocumento()
		}
	}

	DescargarDocumento() {
		if (this.documento === null) return;

		const a = document.createElement('a');
		const blob = new Blob([this.documento.bytesFile], {type: this.documento.extension});
		a.href = URL.createObjectURL(blob);
		a.download = this.nombreDocumento || `${Date.now()}-${this.codigoDocumento}.${this.documento.extension}`;
		a.click();
	}

	SanitizarUrl(url: string) {
		return this._domSanitizer.bypassSecurityTrustResourceUrl(url)
	}

	search(stringToSearch: string) {
		if (this.pdfComponent) {
			this.pdfComponent.pdfFindController.executeCommand('find', {
				caseSensitive: false, findPrevious: undefined, highlightAll: true, phraseSearch: true, query: stringToSearch
			});
		}
	}

}
