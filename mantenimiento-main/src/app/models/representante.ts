export interface RepresentanteResponse {
	codigoPersona: number;
	codigoRepresentante: number;
	nombres: string;
	apellidoPaterno: string;
	apellidoMaterno: string;
	codigoTipoRepresentante: number;
	principal: boolean;
	numeroIdentificacion: string;
}

export interface RepresentanteRequest {
	codigoPersona: number;
	codigoRepresentante: number;
	codigoTipoRepresentante: number;
	principal: boolean;
}

export interface ElimRepresentanteRequest {
	codigoPersona: number;
	codigoRepresentante: number;
}
