export interface BienIntangibleResponse {
    codigoPersona: number;
    numeroRegistro: number;
    tipoBienIntangible: number;
    codigoReferencia: string;
    descripcion: string;
    avaluoComercial: number;
}
export interface BienIntangibleMinResponse {
    codigoPersona?: number;
    numeroRegistro: number;
    tipoBienIntangible: number;
    codigoReferencia: string;
    descripcion: string;
    avaluoComercial: number;
}

export interface InsertarBienIntangible {
    codigoPersona?: number;
    tipoBienIntangible: number;
    codigoReferencia: string;
    descripcion: string;
    avaluoComercial: number;
}

export interface EliminarBienIntangibleRequest {
	codigoPersona: number;
	numeroRegistro: number;
}
