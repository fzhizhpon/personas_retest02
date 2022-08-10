export interface Respuesta<T> {
	codigo: number;
	resultado: T;
	mensajeUsuario?: string | any;
	mensaje?: string | any;
}
