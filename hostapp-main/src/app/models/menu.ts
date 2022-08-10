export interface Submenu {
    codigoOpcion: string;
    descripcion: string;
    opcionNet: string;
}

export interface Menu {
	codigoOpcion: string;
	descripcion: string;
	submenus: Submenu[];
}
