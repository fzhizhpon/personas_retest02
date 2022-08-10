export interface PersonaRequest {
	codigoPersona?: number;
	codigoTipoIdentificacion: number;
	codigoTipoPersona: number;
	numeroIdentificacion: string;

	observaciones?: string;
	codigoTipoContribuyente: string;
}

export interface PersonaResponse {
	codigoPersona?: number;
	codigoTipoIdentificacion: number;
	codigoTipoPersona: number;
	numeroIdentificacion: string;

	observaciones?: string;
	codigoTipoContribuyente: string;
	codigoDocumento?: string;
}

export interface PersonaMinResponse {
	codigoPersona: number;
	codigoTipoPersona: number;
	nombre: string;
}

export interface PersonaNaturalResponse {
	nombres: string;
	apellidoPaterno: string;
	apellidoMaterno: string;
	fechaNacimiento: string | Date;

	codigoPais: number;
	codigoProvincia: number;
	codigoCiudad: number;
	codigoParroquia: number;

	codigoTipoSangre: number;
	codigoEstadoCivil: number;
	codigoConyuge?: number;
	codigoGenero: number;
	codigoProfesion: number;
	codigoTipoEtnia: number;

	tieneDiscapacidad: string;
	esVulnerable: string;
	codigoTipoDiscapacidad?: number;
	porcentajeDiscapacidad?: number;
}

export interface PersonaNoNaturalResponse {
	codigoPersona: number;
	razonSocial: string;
	fechaConstitucion: string | Date;
	objetoSocial: string;
	finalidadLucro: boolean;
	tipoSociedad: number;
	obligadoLlevarContabilidad: boolean;
	agenteRetencion: boolean;
	direccionWeb?: string;
}

export interface PersonaNoNaturalRequest {
	codigoPersona: number;
	razonSocial: string;
	fechaConstitucion: string | Date;
	objetoSocial: string;
	finalidadLucro: boolean;
	tipoSociedad: number;
	obligadoLlevarContabilidad: boolean;
	agenteRetencion: boolean;
	direccionWeb?: string;
}
