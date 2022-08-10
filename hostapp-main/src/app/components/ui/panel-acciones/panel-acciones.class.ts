export interface BotonAccion {
	icono: string;
	label: string;
	deshabilitado?: boolean;
	click: void | Function;
}
