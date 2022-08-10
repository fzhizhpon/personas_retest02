export interface LoginCedulaDto {
    cedula: string;
    contrasenia: string;
    codigoEmpresa: number;
    codigoSucursal: number;
    codigoAgencia: number;
	forzarCierreSesion: boolean;
}
