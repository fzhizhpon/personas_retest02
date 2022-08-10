export interface Catalogo<T> {
	codigo: T;
	descripcion: string;
}

export interface Lugar {
	codigoPais: number;
	pais: string;
	nacionalidad: string;
	codigoProvincia: number;
	provincia: string;
	codigoCiudad: number;
	ciudad: string;
	codigoParroquia: number;
	parroquia: string;
	codigoLugar: string;
	descripcionLugar: string;
}
