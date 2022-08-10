import { Component, Input } from "@angular/core";
import { Observable } from "rxjs";
import { AccionesFormulario } from "src/app/enums/acciones-formulario.enum";

@Component({ template: '' })
export class ComponenteBasePersona {
	nombreComponente!: string;
	accionesHtml = AccionesFormulario;
	tieneCambios = false;

	cargando: Cargando = {
		guardar: false,
		listar: false,
		actualizar: false,
	};

	@Input('mostrarGuardar') mostrarGuardar = true;
	@Input('mostrarListar') mostrarListar = true;
	@Input('guardarConCallback') guardarConCallback = false;

	codigoPersona!: number | null;

	@Input('codigoPersona')
	set _codigoPersona(value: number | null) {
		if (value != null) {
			this.codigoPersona = value;
			this.ObtenerTodo();
		}
	}


	codigoTipoPersona!: number | null;
	@Input('codigoTipoPersona')
	set _codigoTipoPersona(value: number | null) {
		if (value != null) {
			this.codigoPersona = value;

		}
	}

	CerrarModalOk() { return; }

	GuardarCambios(): Observable<any> {
		throw `La función 'GuardarCambios' debe tener una implementación.`;
	}

	ObtenerTodo(): void {
		throw `La función 'ObtenerTodo' debe tener una implementación.`;
	}

	LimpiarCampos() { throw `La función 'LimpiarCampos' debe tener una implementación.`; }

	ResetearVista() { throw `La función 'ResetearVista' debe tener una implementación.` }
}

export interface Cargando {
	guardar: boolean;
	listar: boolean;
	actualizar?: boolean;
}
