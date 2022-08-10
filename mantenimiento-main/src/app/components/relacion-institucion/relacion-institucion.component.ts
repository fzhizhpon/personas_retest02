import {Component, OnInit} from '@angular/core';
import {ComponenteBasePersona} from "../base/componente-base-persona";
import {RelacionInstitucionService} from "../../services/relacion-institucion.service";
import {RelacionInstitucion} from "../../models/relacion-institucion";
import {MessageService} from 'src/app/services/common/message/message.service';

@Component({
	selector: 'pers-relacion-institucion',
	templateUrl: './relacion-institucion.component.html',
	styleUrls: ['./relacion-institucion.component.scss']
})
export class RelacionInstitucionComponent extends ComponenteBasePersona implements OnInit {

	// * props tabla
	datosRelacionesInstitucionales: RelacionInstitucion [] = [];
	cargarRelaciones = false;

	constructor(
		private _relacionInstitucionService: RelacionInstitucionService,
		private _messageService: MessageService,
	) {
		super();
	}

	ngOnInit(): void {
	}

	ObtenerTodo() {
		if (this.codigoPersona) {
			this._relacionInstitucionService.obtenerRelacionInstitucion(this.codigoPersona)
				.subscribe((res) => {
					this.datosRelacionesInstitucionales = res.resultado;
				}, (err) => {
					this._messageService.showError(err.mensaje || err)
				})
		}
	}

}
