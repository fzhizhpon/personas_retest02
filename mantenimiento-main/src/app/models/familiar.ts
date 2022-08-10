export interface FamiliarResponse {
	codigoPersona: number;
	codigoPersonaFamiliar: number;
	codigoParentesco: number;
	nombres: string;
	apellidoPaterno: string;
	apellidoMaterno: string;
	esCargaFamiliar: boolean;
	observacion?: string;
}

export interface InsertarFamiliarRequest {
	codigoPersona: number;
	codigoPersonaFamiliar: number;
	codigoParentesco: number;
	observacion: string;
	esCargaFamiliar: boolean;
}
export interface EliminarFamiliarRequest {
	codigoPersona: number;
	codigoPersonaFamiliar: number;
}
export interface ActualizarFamiliarRequest {
	codigoPersona: number;
	codigoPersonaFamiliar: number;
	codigoParentesco: number;
	observacion: string;
	esCargaFamiliar: boolean;
}
