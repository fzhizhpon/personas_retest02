export interface EstadoFinanciero {
    codigoPersona: number;
	cuentaContable: string;
	valor: number;
	descripcion?: string;
	observacion: string;
	recursoExterno: boolean;
	codigoComponente?: number;
}

export interface EstadoFinancieroResponse {
    codigoPersona: number;
	cuentaContable: string;
	valor: number;
	descripcion?:string;
	observacion: string;
	recursoExterno: boolean;
	codigoComponente?: number;
}

export interface ActualizarEstadoFinancieroRequest {
    codigoPersona: number;
	cuentaContable: string;
	valor: number;
	descripcion?:string;
	observacion: string;
}

export interface InsertarEstadoFinancieroRequest {
    codigoPersona: number;
	cuentaContable: string;
	valor: number;
	descripcion?:string;
	observacion: string;
}
