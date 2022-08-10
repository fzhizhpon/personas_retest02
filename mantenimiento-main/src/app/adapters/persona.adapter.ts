import { PersonaNaturalResponse, PersonaRequest, PersonaResponse } from "../models/persona.model";

export class DatosGeneralesPersonaRequestAdapter {
	codigoPersona?: number;
	codigoTipoIdentificacion: number;
	codigoTipoPersona: number;
	numeroIdentificacion: string;
	observaciones?: string;
	codigoTipoContribuyente: string;

	constructor(personaRequest: PersonaRequest) {
		this.codigoPersona = personaRequest.codigoPersona;
		this.codigoTipoIdentificacion = personaRequest.codigoTipoIdentificacion;
		this.codigoTipoPersona = personaRequest.codigoTipoPersona;
		this.numeroIdentificacion = personaRequest.numeroIdentificacion;
		this.observaciones = personaRequest.observaciones;
		this.codigoTipoContribuyente = personaRequest.codigoTipoContribuyente;
	}
}

export class DatosGeneralesPersonaResponseAdapter {
	codigoPersona?: number;
	codigoTipoIdentificacion: number;
	codigoTipoPersona: number;
	numeroIdentificacion: string;
	observaciones?: string;
	codigoTipoContribuyente: string;
	codigoDocumento?: string;

	constructor(personaResponse: PersonaResponse) {
		this.codigoPersona = personaResponse.codigoPersona;
		this.codigoTipoIdentificacion = personaResponse.codigoTipoIdentificacion;
		this.codigoTipoPersona = personaResponse.codigoTipoPersona;
		this.numeroIdentificacion = personaResponse.numeroIdentificacion;
		this.observaciones = personaResponse.observaciones;
		this.codigoTipoContribuyente = personaResponse.codigoTipoContribuyente;
		this.codigoDocumento = personaResponse.codigoDocumento;
	}
}

export class PersonaNaturalResponseAdapter {
	nombres: string;
	apellidoPaterno: string;
	apellidoMaterno: string;
	fechaNacimiento: string | Date;
	codigoApoderado?: number;

	codigoPais: number;
	codigoProvincia: number;
	codigoCiudad: number;
	codigoParroquia: number;

	codigoTipoSangre: number;
	codigoConyuge?: number;
	codigoEstadoCivil: number;
	codigoGenero: number;
	codigoProfesion: number;
	codigoTipoEtnia: number;

	tieneDiscapacidad: boolean;
	esVulnerable: boolean;
	codigoTipoDiscapacidad?: number;
	porcentajeDiscapacidad?: number;

	constructor(personaResponse: PersonaNaturalResponse) {
		this.nombres = personaResponse.nombres;
		this.apellidoPaterno = personaResponse.apellidoPaterno;
		this.apellidoMaterno = personaResponse.apellidoMaterno;
		this.fechaNacimiento = personaResponse.fechaNacimiento;
		this.codigoPais = personaResponse.codigoPais;
		this.codigoProvincia = personaResponse.codigoProvincia;
		this.codigoCiudad = personaResponse.codigoCiudad;
		this.codigoParroquia = personaResponse.codigoParroquia;
		this.codigoTipoSangre = personaResponse.codigoTipoSangre;
		this.codigoEstadoCivil = personaResponse.codigoEstadoCivil;
		this.codigoGenero = personaResponse.codigoGenero;
		this.codigoProfesion = personaResponse.codigoProfesion;
		this.codigoTipoEtnia = personaResponse.codigoTipoEtnia;
		this.tieneDiscapacidad = personaResponse.tieneDiscapacidad == '1';
		this.esVulnerable = personaResponse.esVulnerable == '1';
		this.codigoTipoDiscapacidad = personaResponse.codigoTipoDiscapacidad;
		this.porcentajeDiscapacidad = personaResponse.porcentajeDiscapacidad;
	}
}

export class PersonaNoNaturalResponseAdapter {
	codigoPersona?: number;
	codigoTipoIdentificacion: number;
	codigoTipoPersona: number;
	numeroIdentificacion: string;
	observaciones?: string;
	codigoTipoContribuyente: string;

	constructor(personaResponse: PersonaResponse) {
		this.codigoPersona = personaResponse.codigoPersona;
		this.codigoTipoIdentificacion = personaResponse.codigoTipoIdentificacion;
		this.codigoTipoPersona = personaResponse.codigoTipoPersona;
		this.numeroIdentificacion = personaResponse.numeroIdentificacion;
		this.observaciones = personaResponse.observaciones;
		this.codigoTipoContribuyente = personaResponse.codigoTipoContribuyente;
	}
}
