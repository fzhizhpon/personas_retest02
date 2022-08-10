export interface ObtenerReferenciasFinancieras {
    //codigoReferenciaFinanciera: number,
    numeroRegistro:number,
    codigoPersona: number,
    paginacion: {
        indiceInicial: number,
        numeroRegistros: number
    }
}

export interface ObtenerReferenciasComerciales {
    codigoPersona: number,
    paginacion: {
        indiceInicial: number,
        numeroRegistros: number
    }
}

export interface ObtenerReferenciasPersonales {
    codigoPersona: number,
    paginacion: {
        indiceInicial: number,
        numeroRegistros: number
    }
}

export interface ObtenerCelulares {
    codigoPersona: number,
    paginacion: {
        indiceInicial: number,
        numeroRegistros: number
    }
}

export interface ObtenerDirecciones {
    codigoPersona: number,
    //codigoDireccion: number,
    paginacion: {
        indiceInicial: number,
        numeroRegistros: number
    }
}
export interface ObtenerDirecciones1 {
    codigoPersona: number,
    codigoDireccion: number,
    
}

export interface ObtenerCorreos {
    codigoPersona: number,
    paginacion: {
        indiceInicial: number,
        numeroRegistros: number
    }
}


export interface ObtenerBienesMuebles {
    codigoPersona: number,
    //codigoDireccion: number,
    paginacion: {
        indiceInicial: number,
        numeroRegistros: number
    }
}

export interface ObtenerBienesInmuebles {
    codigoPersona: number,
    //codigoDireccion: number,
    paginacion: {
        indiceInicial: number,
        numeroRegistros: number
    }
}

export interface ObtenerBienesIntangibles {
    codigoPersona: number,
    //codigoDireccion: number,
    paginacion: {
        indiceInicial: number,
        numeroRegistros: number
    }
}