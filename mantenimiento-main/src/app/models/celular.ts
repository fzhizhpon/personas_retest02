export interface CelularResponse {
	codigoTelefonoMovil: number;
	codigoPais:number;
	codigoPersona: number;
	numero: string;
	codigoOperadora: number;
	observaciones: string;
	principal: boolean;
}

export interface ActualizarCelularRequest {
	codigoTelefonoMovil: number;
	codigoPais:number;
	codigoPersona: number;
	numero: string;
	codigoOperadora: number;
	observaciones: string;
	principal: boolean;
}

export interface EliminarCelularRequest {
	codigoTelefonoMovil: number;
	codigoPersona: number;
}

export interface InsertarCelularRequest {
	codigoPersona: number;
	codigoPais:number;
	numero: string;
	codigoOperadora: number;
	observaciones: string;
	principal: boolean;
}
