export interface BienInmuebleResponse {
	codigoPersona: number;
	numeroRegistro: number;
	codigoPais: number;
	codigoProvincia: number;
	codigoCiudad: number;
	codigoParroquia: number;
	callePrincipal: string;
	calleSecundaria: string;
	numero: string;
	sector: string;
	codigoPostal: string;
	esMarginal: string;
	latitud: number;
	longitud: number;
	estado: string;
	comunidad: string;
	referencia: string;
	tipoSector: string;
	avaluoComercial: number;
	avaluoCatastral: number;
	areaTerreno: number;
	areaConstruccion: number;
	valorTerrenoMetrosCuadrados: number;
	fechaConstruccion: string | Date;
	descripcion: string;
	tipoBienInmueble: number;
	nombreParroquia?: string;
}


export interface BienInmuebleMinResponse {
	codigoPersona: number;
	numeroRegistro: number;
	codigoPais: number;
	codigoProvincia: number;
	codigoCiudad: number;
	codigoParroquia: number;
	callePrincipal: string;
	calleSecundaria: string;
	numero: string;
	sector: string;
	codigoPostal: string;
	esMarginal: string;
	latitud: number;
	longitud: number;
	estado: string;
	comunidad: string;
	referencia: string;
	tipoSector: string;
	avaluoComercial: number;
	avaluoCatastral: number;
	areaTerreno: number;
	areaConstruccion: number;
	valorTerrenoMetrosCuadrados: number;
	fechaConstruccion: string | Date;
	descripcion: string;
	tipoBienInmueble: number;
	nombreParroquia?: string;
}
