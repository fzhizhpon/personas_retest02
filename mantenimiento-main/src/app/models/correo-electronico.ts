export interface CorreoElectronicoInDto<T> {
	codigoCorreo: number;
	codigoPersona?: number;
	correoElectronico: string;
	esPrincipal: T;
	observaciones: string;
}
