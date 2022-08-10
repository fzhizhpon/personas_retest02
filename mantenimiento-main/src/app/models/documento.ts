export interface DocumentoResponse {
	idFile: string;
	bytesFile: string;
	format: string;
	extension: string;
}

export interface SubirArchivoResponse {
	idFile: string;
	bytesFile: null;
	format: string;
	extension: string;
}

export interface GrupoDocumentologiaResponse {
	codigoGrupo: number;
	descripcionGrupo: string;
	codigoDocumento: number;
	diasValidezDocu: number;
	descripcionDocumento: string;
}

export interface DocumentoGrupoReponse {
	numeroDocumento: number;
	numeroOperacion: number;
	idDocumento: string;
	tipoArchivo: 'I' | 'D';
	nombreArchivo: string;
	fechaSistema: string | Date;
	fechaVigencia: string | Date;
	observaciones: string;
}

export interface DocumentoRequest {
	codigoComponente: number;
	codigoGrupo: number;
	codigoTipoDocumento: number;
	codigoPersona: number;
	numeroOperacion: number;
	idDocumento: string;
	tipoArchivo: 'I' | 'D';
	nombreArchivo: string;
	fechaSistema: Date;
	fechaVigencia: Date;
	observaciones: string;
	numeroDocumento: number;
	codigoAdicional1?: string;
	codigoAdicional2?: string;
}
