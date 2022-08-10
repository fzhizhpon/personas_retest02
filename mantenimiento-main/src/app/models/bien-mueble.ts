export interface BienMuebleResponse {
    codigoPersona: number;
    numeroRegistro: number;
    tipoBienMueble: number;
    codigoReferencia: string;
    descripcion: string;
    avaluoComercial: number;
}
export interface BienMuebleMinResponse {
    codigoPersona?: number;
    numeroRegistro: number;
    tipoBienMueble: number;
    codigoReferencia: string;
    descripcion: string;
    avaluoComercial: number;
}

export interface InsertarBienMueble {
    codigoPersona: number;
    tipoBienMueble: number;
    codigoReferencia: string;
    descripcion: string;
    avaluoComercial: number;
}

export interface EliminarBienMuebleRequest {
	codigoPersona: number;
	numeroRegistro: number;
}
