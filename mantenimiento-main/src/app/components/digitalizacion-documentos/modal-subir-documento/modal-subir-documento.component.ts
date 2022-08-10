import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NzUploadFile } from 'ng-zorro-antd/upload';
import { DocumentoRequest, GrupoDocumentologiaResponse } from '../../../models/documento';
import { ConfiguracionDigitalizacion } from '../digitalizacion.model';
import { differenceInCalendarDays } from 'date-fns';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { DocumentologiaService } from '../../../services/documentologia.service';
import { MessageService } from 'src/app/services/common/message/message.service';

@Component({
	selector: 'vs-modal-subir-documento',
	templateUrl: './modal-subir-documento.component.html',
	styleUrls: ['./modal-subir-documento.component.scss']
})
export class ModalSubirDocumentoComponent {

	@Input('codigoPersona') codigoPersona!: number;
	@Input('codigoComponente') codigoComponente!: number;
	@Input('grupoDocumentos') grupoDocumentos!: GrupoDocumentologiaResponse;

	@Input('configuracion') configuracion!: ConfiguracionDigitalizacion;

	archivosCargados: NzUploadFile[] = []
	documentos: FormGroup[] = []

	loading = false;
	recargarAlCerrar = false;
	dateFormat = "dd/MM/yyyy";

	disabledDate = (current: Date): boolean => differenceInCalendarDays(current, new Date()) <= 0;

	constructor(
		private _modalRef: NzModalRef,
		private _documentoService: DocumentologiaService,
		private _messageService: MessageService,
	) { }

	seleccionarArchivos(archivos: NzUploadFile[]) {
		//? Quitamos el primer elemento porque NgZorro
		//? duplica el primero cuando son varios archivos
		if (archivos.length > 1) archivos.shift()

		this.archivosCargados = archivos;

		//? Creamos un formulario por cada archivo seleccionado
		this.archivosCargados.forEach(archivo => {
			const form = this.obtenerFormulario()
			form.get('documento')?.setValue(archivo)
			form.get('nombreArchivo')?.setValue(archivo.originFileObj?.name)
			this.documentos.push(form);
		})
	}

	quitarArchivoSeleccionado(indice: number) {
		this.documentos.splice(indice, 1)
	}

	obtenerFormulario(): FormGroup {
		return new FormGroup({
			guardadoOk: new FormControl(false, []),
			documento: new FormControl(null, [Validators.required]),
			codigoComponente: new FormControl(this.codigoComponente, [Validators.required]),
			codigoGrupo: new FormControl(this.grupoDocumentos.codigoGrupo, [Validators.required]),
			codigoTipoDocumento: new FormControl(this.grupoDocumentos.codigoDocumento, [Validators.required]),
			codigoPersona: new FormControl(this.codigoPersona, [Validators.required]),
			numeroOperacion: new FormControl(this.configuracion?.numeroOperacion || 0, [Validators.required]),
			idDocumento: new FormControl(null, []),
			tipoArchivo: new FormControl('I', []),
			nombreArchivo: new FormControl(null, [Validators.required]),
			fechaVigencia: new FormControl(null, [Validators.required]),
			observaciones: new FormControl(null, [Validators.required]),
			numeroDocumento: new FormControl(0, [Validators.required]),
			codigoAdicional1: new FormControl(this.configuracion?.codigoAdicional1, []),
			codigoAdicional2: new FormControl(this.configuracion?.codigoAdicional2, []),
		});
	}

	async guardarDocumentos() {
		//? Validacion de formularios
		let valid = true;
		this.documentos.forEach(doc => {
			if(doc.invalid) {
				valid = false
				doc.markAllAsTouched()
			}
		})

		if (!valid) return;

		this.loading = true;
		let errores = 0;

		for await (const documento of this.documentos) {
			if (documento.get('guardadoOk')?.value === true) return;

			try {
				const form = documento.getRawValue()
				const doc = (await this._documentoService.subirArchivo(form.documento?.originFileObj).toPromise()).resultado
				form.idDocumento = doc.idFile;

				await this._documentoService.guardarDocumento(<DocumentoRequest>(form)).toPromise()

				documento.get('guardadoOk')?.setValue(true)
				this.recargarAlCerrar = true
			}
			catch(error: any) {
				this._messageService.showError(error.mensaje || error)
				documento.get('guardadoOk')?.setValue(false)
				errores++;
			}
		}

		this.loading = false;
		if (errores == 0) {
			this.cerrarModal()
		}
	}

	cerrarModal() {
		this._modalRef.close(this.recargarAlCerrar)
	}

}
