import { DocumentoGrupoReponse, DocumentoRequest, DocumentoResponse, GrupoDocumentologiaResponse, SubirArchivoResponse } from '../models/documento';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Respuesta } from 'src/app/models/respuesta';
import { ListarDocumentos } from '../models/documentologia';

@Injectable({
	providedIn: 'root'
})
export class DocumentologiaService {

	constructor(
		private _http: HttpClient,
	) { }

	obtenerDocumento(codigo: string) {
		return this._http.get<Respuesta<DocumentoResponse>>(`${environment.endpointDocumentologia}/archivos/${codigo}`)
	}

	obtenerGruposDocumentos(codigoComponente: number) {
		return this._http.get<Respuesta<GrupoDocumentologiaResponse[]>>(`${environment.endpointDocumentologia}/documentos/${codigoComponente}`);
	}

	obtenerDocumentosPorGrupo(data: ListarDocumentos) {
		return this._http.post<Respuesta<DocumentoGrupoReponse[]>>(`${environment.endpointDocumentologia}/documentos/listar`, data);
	}

	subirArchivo(fichero: File) {
		const formData = new FormData();
		formData.append('formFile', fichero)

		let headers = new HttpHeaders();
		headers = headers.set('Auto-header', 'true');

		return this._http.post<Respuesta<SubirArchivoResponse>>(`${environment.endpointDocumentologia}/archivos`, formData, {headers: headers});
	}

	guardarDocumento(documento: DocumentoRequest) {
		return this._http.post<Respuesta<any>>(`${environment.endpointDocumentologia}/documentos`, documento);
	}

	eliminarDocumento(idDocumento: any) {
		console.log(idDocumento);
		return this._http.delete<Respuesta<any>>(`${environment.endpointDocumentologia}/archivos/${idDocumento}`);
	}

}
