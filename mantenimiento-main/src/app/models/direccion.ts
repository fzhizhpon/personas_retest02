export interface DireccionResponse {
	codigoPersona: number;
	numeroRegistro: number;
	codigoPais: number;
	codigoProvincia: number;
	codigoCiudad: number;
	codigoParroquia: number;
	callePrincipal: string;
	calleSecundaria: string;
	numeroCasa: string;
	sector: string;
	principal: boolean;
	comunidad?: string;
	codigoTipoResidencia: number;
	descripcionTipoResidencia: string;
	telefonoFijo: {
		codigoPersona: number;
		numeroRegistro: number
		numero: string;
		codigoOperadora: 1;
		observaciones: string;
	};
	fechaInicialResidencia?: string | Date;
	valorArriendo: number;
	lugar?: any;
	bienInmueble?: any;
	numeroRegistroTelFijo: number;
	numeroTelFijo: String;
	nombreParroquia?: string;
	/*numeroTelefono:String;
	codigoOperadora:number;
	observaciones :String;*/
}

export interface DireccionMinResponse {
	codigoPersona?: number;
	numeroRegistro: number;
	codigoPais: number;
	codigoProvincia: number;
	codigoCiudad: number;
	codigoParroquia: number;
	callePrincipal: string;
	calleSecundaria: string;
	longitud?: number;
	latitud?: number;
	numeroCasa: string;
	sector: string;
	principal: boolean;
	codigoTipoResidencia: number;
	descripcionTipoResidencia: string;
	telefonoFijo: {
		codigoPersona: number;
		numero: string;
		numeroRegistro: number;
		codigoOperadora: 1;
		observaciones: string;
	};
	fechaInicialResidencia?: string | Date;
	valorArriendo: number;

	numeroRegistroTelFijo: number;
	numeroTelFijo: String;
	lugar?: any; bienInmueble?: any;
	nombreParroquia?: string;
	/*numeroTelefono: String;
	codigoOperadora: number;
	observaciones: String;*/
}
