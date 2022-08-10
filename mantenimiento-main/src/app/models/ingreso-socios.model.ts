export interface persona {
	codigoPersona: number,
	numeroIdentificacion: string,
	tieneSeguro: boolean, //'1','0'
	tieneCuenOtraInstFinan: boolean, //'1','0'
	ingresoPromedio: number,
	observaciones: string,
	codigoTipoIdentificacion: number,
	codigoTipoPersona: number,
	codigoTipoContribuyente: number
}

export interface personaNatural {
	codigoPersona: number,
	nombres: string,
	apellidoPaterno: string,
	apellidoMaterno: string,
	fechaNacimiento: Date,
	numPersonasNucleoFami: number,
	numCargasFamiliares: number,
	poseeBonoGobierno: boolean, //'1','0'
	tieneHabilidadesEspeciales: boolean, //'1','0'
	codigoTipoDiscapacidad: number,
	porcentajeDiscapacidad: number,
	tieneApoderado: boolean, //'1','0'
	codigoPaisNacimiento: number,
	codigoProvinciaNacimiento: number,
	codigoCiudadNacimiento: number,
	codigoParroquiaNacimiento: number,
	codigoTipoSangre: number,
	codigoApoderado: number,
	codigoConyuge: number,
	codigoEstadoCivil: number,
	codigoGenero: number,
	codigoProfesion: number,
	codigoTipoEtnia: number
}

export interface personaNoNatural {
	codigoPersona: number,
	razonSocial: string,
	fechaConstitucion: Date,
	objetoSocial: string,
	finalidadLucro: string,
	tipoSociedad: number
}

export interface referenciaFinanciera {
	numeroRegistro: number;
	codigoPersona: number;
	codigoTipoCuentaFinanciera:number;
	numeroCuenta: string;
	cifras: string;
	fechaCuenta: Date;
	observaciones: string;
	saldo: number;
	codigoInstitucionFinanciera: number;
	obligacionMensual:number;
	saldoObligacion:number;
	nombreFinanciera:string;
	
}
export interface referenciaFinancieraUpdate {
	numeroRegistro: number;
	cifras: string;
	codigoPersona: number;
	numeroCuenta: string;
	obligacionMensual:number;
	observaciones: string;
	saldo: number;
	saldoObligacion:number;
	
}

export interface referenciaFinancieraInsert {
	codigoPersona: number;
	codigoTipoCuentaFinanciera:number;
	numeroCuenta: string;
	cifras: string;
	fechaCuenta: Date;
	observaciones: string;
	saldo: number;
	codigoInstitucionFinanciera: number;
	obligacionMensual:number;
	saldoObligacion:number;
	nombreFinanciera:string;
	
}


export interface referenciaComercial {
	//codigoReferenciaComercial: number,
	numeroRegistro: number;
	codigoPersona: number,
	codigoPais: number,
	codigoProvincia: number,
	codigoCiudad: number,
	codigoParroquia: number,
	establecimiento: string,
	fechaRelacion: Date,
	montoCredito: number,
	plazo: number,
	codigoTipoTiempo: number,
	telefono: string,
	lugar:any,
}

export interface referenciaComercialEliminar {
	codigoPersona:number, numeroRegistro:number}


export interface referenciaPersonal {
	numeroRegistro: number;
	identificacion: string,
	codigoTipoIdentificacion: number,
	nombres: string,
	apellidoPaterno: string,
	apellidoMaterno: string,
	codigoReferenciaPersonal: number,
	codigoPersonaReferida: number,
	codigoPersona: number,
	fechaConoce: Date,
	observaciones: string
}
export interface EliminarReferenciaPersonalRequest {

	codigoPersonaReferida: number,
	codigoPersona: number,
}

export interface GuardarReferenciaPersonal {
	codigoPersona: number,
	codigoPersonaReferida: number,
	fechaConoce: Date,	
	observaciones: string
}


export interface consejoAccionistas {
	identificacion: string;
	razonSocial: string;
	nacionalidad: string;
	porcentajeAccionista: string;
}


export interface residenciaPaises {
	codigoPais: string;
	pais: string;
	identificacion: string;
}


export interface celular {
	codigoTelefonoMovil: number,
	codigoPersona: number,
	numero: string,
	codigoOperadora: number,
	observaciones: string,
	principal: string
}

export interface direccion {
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
	codigoPostal: string;
	esMarginal: string;
	latitud: number;
	longitud: number;
	fechaIngreso: Date;
	codigoDireccionRegCivil: string;
	codigoUsuarioRegistra: number;
	principal: string;
	estado: string;
	/*pais: string;
	provincia: string;
	ciudad: string;
	parroquia: string;*/

	tipoSector: string;
	sectorMarginal: boolean;
	telefonoFijo: {
		codigoPersona: number;
		numero: string;
		codigoOperadora: 1;
		observaciones: string;
	};
	tipoResidencia: string;
	tiempo: string;
	tiempoEn: string;

	referencia: string;
	fechaInicialResidencia: string | Date;
	valorArriendo:number;

}

export interface trabajo {
	codigoTrabajo:number;
	numeroRegistro: number;
	codigoPersona: number;
	codigoCategoria:number;
	fechaIngreso: Date;
	razonSocial: string;
	direccion: string;
	cargo: string;
	codigoActividad:number;
	principal:number;
	codigoPais: number;
	codigoProvincia: number;
	codigoCiudad: number;
	codigoParroquia: number;
	ingresosMensuales: number;
	lugar:any;
	
	/*numeroRegistro: number;
	codigoPersona: number;
	codigoCategoria:number;
	codigoActividad:number;
	principal:number;

	codigoSectorEconomico: string;
	codigoSubSectorEconomico: string;
	codigoIndustriaSectorEconomico: string;
	codigoTipoTrabajo: number;
	codigoTipoTiempo: number;
	codigoTipoActividadTrabajo: number;
	codigoPais: number;
	codigoProvincia: number;
	codigoCiudad: number;
	codigoParroquia: number;
	tiempoActividad: number;
	fechaIngreso: Date;
	razonSocial: string;
	direccion: string;
	cargo: string;
	ingresosMensuales: number;
	descripcionCategoria: string;*/
}

export interface ObtenerTrabajos {
	codigoPersona: number,
	paginacion: {
		indiceInicial: number,
		numeroRegistros: number
	}
}

export interface ObtenerDirecciones {
	codigoPersonaNatural: number,
	codigoDireccion: number,
	paginacion: {
		indiceInicial: number,
		numeroRegistros: number
	}
}

export interface correoElectronico {
	codigoCorreoElectronico: number;
	codigoPersona: number;
	correoElectronico: string;
	esPrincipal: string;
	observaciones: string;
}

export interface bienInmueble {
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
    codigoPostal:string;
    esMarginal:string;
    latitud:number;
    longitud:number;
    estado:string;
    //comunidad:string;
    referencia:string;
    tipoSector:string;
    avaluoComercial: number;
    avaluoCatastral: number;
    areaTerreno: number;
    areaConstruccion: number;
    valorTerrenoMetrosCuadrados: number;
	fechaConstruccion: string | Date;
    descripcion: string;
	tipoBienInmueble: number;

}

export interface bienMueble {

    codigoPersona: number;
    numeroRegistro: number;
    tipoBienMueble: number;
    codigoReferencia: string;
    descripcion: string;
    avaluoComercial: number;
    estado: number;

}
export interface bienIntangible {
    codigoPersona?: number;
    numeroRegistro: number;
    tipoBienIntangible: number;
    codigoReferencia: string;
    descripcion: string;
    avaluoComercial: number;
}


export interface manejoDeEstado<T> {
	objeto: T,
	accion: string
}
