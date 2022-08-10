import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { finalize } from 'rxjs/operators';
import { MessageService } from 'src/app/services/common/message/message.service';
import { PersonaBusquedaDto } from '../../models/persona';
import { PersonaService } from '../../services/persona.service';

@Component({
	selector: 'vs-modal-buscar-persona',
	templateUrl: './modal-buscar-persona.component.html',
	styleUrls: ['./modal-buscar-persona.component.scss']
})
export class ModalBuscarPersonaComponent implements OnInit {

	@ViewChild('busqueda') inputBuscar!: ElementRef;

	isLoading = false;

	filtros: {
		nombre: string,
		campo: string
	}[] = [
		{
			nombre: 'Nombre',
			campo: 'nombre'
		},
		{
			nombre: 'Nro. Identificación',
			campo: 'nroIdentificacion'
		},
		{
			nombre: 'Código',
			campo: 'codigoPersona'
		},
	]

	personas: PersonaBusquedaDto[] = [];

	form: FormGroup;

	constructor(
		private _personaService: PersonaService,
		private _modalRef: NzModalRef,
		private _messageService: MessageService,
	) {
		this.form = new FormGroup({
			filtro: new FormControl('nombre', [Validators.required]),
			busqueda: new FormControl(null, [Validators.required]),
		});
	}

	ngOnInit(): void {
		setTimeout(() => {
			this.inputBuscar.nativeElement.focus()
		}, 400)
	}

	cargarPersonas()
	{
		if (this.form.invalid) {
			this._messageService.showError('Debe indicar el filtro a aplicar')
			this.form.markAllAsTouched()
			return;
		}

		const parametro = this.form.getRawValue();

		if (parametro.filtro == 'nombre') {
			if (parametro.busqueda.length < 5) {
				this._messageService.showError('Para buscar por nombre de ingresar mínimo 5 caracteres')
				return;
			}
			if (parametro.busqueda.charAt(0) != '%') parametro.busqueda = '%' + parametro.busqueda;
			if (parametro.busqueda.charAt(parametro.busqueda.length - 1) != '%') parametro.busqueda = parametro.busqueda + '%';
		}

		this.isLoading = true;

		this._personaService.BuscarPersonas(parametro.filtro, parametro.busqueda)
		.pipe(finalize(() => this.isLoading = false))
		.subscribe(api => {
			this.personas = api.resultado
		}, (error) => this._messageService.showError(error.mensaje || error))
	}

	seleccionarPersona(persona: PersonaBusquedaDto) {
		this._modalRef.close(persona);
	}

}
