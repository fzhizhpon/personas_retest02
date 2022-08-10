import {Component, Input, OnChanges, SimpleChanges} from '@angular/core';
import {DocumentologiaService} from '../../services/documentologia.service';
import {finalize} from 'rxjs/operators';
import {PersonaService} from '../../services/persona.service';
import {EdadService} from 'src/app/services/edad/edad.service';
import {MessageService} from 'src/app/services/common/message/message.service';
import {Catalogo} from 'src/app/components/catalogos/catalogo';
import {CatalogoService} from 'src/app/services/api/catalogos/catalogo.service';

@Component({
	selector: 'app-persona',
	templateUrl: './persona.component.html',
	styleUrls: ['./persona.component.scss']
})
export class PersonaComponent implements OnChanges {

	_codigoPersona!: number | null;
	_codigoTipoPersona!: number | null;

	isLoading = false;

	imagen: {
		frontal?: string;
		posterior?: string;
	} = {}

	@Input('codigoPersona')
	set codigoPersona(value: number | null) {
		if (value != null) {
			this._codigoPersona = value;
		}
	}

	@Input('codigoTipoPersona')
	set codigoTipoPersona(value: number | null) {
		if (value != null) {
			this._codigoTipoPersona = value;
		}
	}

	@Input('width') width = '21.5rem';

	@Input('sticky') sticky = false;

	@Input('stickyValue') stickyPosition: PersonaSticky = {
		top: 'auto',
		left: 'auto'
	};

	showFrontImage = true;

	persona: any = {};
	edadCalculada = "";


	tiposSociedades: Catalogo<number>[] = []

	constructor(
		private _personaService: PersonaService,
		private _documentoService: DocumentologiaService,
		private _calcularEdadService: EdadService,
		private _messageService: MessageService,
		private _catalogoService: CatalogoService,
	) {
		this._catalogoService.obtenerPorGet<number>('TiposSociedades')
			.subscribe(api => this.tiposSociedades = api.resultado)
	}


	ngOnChanges(changes: SimpleChanges) {
		this.ObtenerDatosPersona();
	}

	ObtenerDatosPersona() {
		if (this._codigoPersona != null && this._codigoTipoPersona != null) {
			this.isLoading = true;
			if (this._codigoTipoPersona === 1) {
				// * persona natural
				this._personaService.ObtenerInfoPersona(this._codigoPersona)
					.pipe(finalize(() => this.isLoading = false))
					.subscribe(data => {
						this.persona = data.resultado;
						this.edadCalculada = this._calcularEdadService.CalcularEdad(this.persona.fechaNacimiento);
						if (this.persona.codigoDocumento) {
							this.ObtenerImagenPersona(this.persona.codigoDocumento)
						}
					}, (error) => this._messageService.showError(error.mensaje || error))
			} else {
				// * persona no natural
				this._personaService.ObtenerInfoPersonaNoNatural(this._codigoPersona)
					.pipe(finalize(() => this.isLoading = false))
					.subscribe(data => {
						this.persona = data.resultado;
					}, (error) => this._messageService.showError(error.mensaje || error))
			}
		}
	}

	ObtenerNombreTipoSociedad(codigo: number) {
		return this.tiposSociedades.find(p => p.codigo === codigo)?.descripcion || codigo
	}

	ObtenerImagenPersona(codigo: string) {
		this._documentoService.obtenerDocumento(codigo)
			.subscribe(data => {
				this.imagen.frontal = `data:${data.resultado.format};base64,${data.resultado.bytesFile}`
			}, (error) => this._messageService.showError(error.mensaje || error))
	}
}

interface PersonaSticky {
	top?: string;
	left?: string;
	bottom?: string;
	right?: string;
}
