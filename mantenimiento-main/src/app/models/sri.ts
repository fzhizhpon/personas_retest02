export interface PlacaResponse {
    codigoVehiculo:          number;
    numeroPlaca:             string;
    numeroCamvCpn:           string;
    colorVehiculo1:          string;
    colorVehiculo2:          string;
    cilindraje:              number;
    nombreClase:             string;
    descripcionMarca:        string;
    descripcionModelo:       string;
    anioAuto:                number;
    descripcionPais:         string;
    mensajeMotivoAuto:       string;
    aplicaCuota:             boolean;
    fechaUltimaMatricula:    Date;
    fechaCaducidadMatricula: Date;
    fechaCompraRegistro:     Date;
    fechaRevision:           Date;
    descripcionCanton:       string;
    descripcionServicio:     string;
    ultimoAnioPagado:        number;
    prohibidoEnajenar:       string;
    observacion:             string;
    estadoExoneracion:       string;
}

export interface ContribuyenteConsolidadoResponse {
    contribuyenteFantasma:          string;
    numeroRucFantasma:              string;
    numeroRuc:                      string;
    razonSocial:                    string;
    nombreComercial:                string;
    estadoPersonaNatural:           string;
    estadoSociedad:                 string;
    claseContribuyente:             string;
    obligado:                       string;
    actividadContribuyente:         string;
    informacionFechasContribuyente: InformacionFechasContribuyente;
    representanteLegal:             string;
    agenteRepresentante:            string;
    personaSociedad:                string;
    subtipoContribuyente:           string;
    motivoCancelacion:              string;
    motivoSuspension:               string;
}

export interface InformacionFechasContribuyente {
    fechaInicioActividades:   string;
    fechaCese:                string;
    fechaReinicioActividades: string;
    fechaActualizacion:       string;
}