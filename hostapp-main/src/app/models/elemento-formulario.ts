import { AccionesFormulario } from "../enums/acciones-formulario.enum";

export interface ElementoFormulario<T> {
	objeto: T;
	accion: AccionesFormulario;
}
