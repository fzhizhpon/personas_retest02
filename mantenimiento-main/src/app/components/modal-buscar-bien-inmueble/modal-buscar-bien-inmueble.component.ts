import { Component, ElementRef, Input, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { finalize } from 'rxjs/operators';
import { MessageService } from 'src/app/services/common/message/message.service';
import { BienInmuebleResponse } from '../../models/bien-inmueble';
import { BienesInmueblesService } from '../../services/bienes-inmuebles.service';

@Component({
	selector: 'vs-modal-buscar-bien-inmueble',
	templateUrl: './modal-buscar-bien-inmueble.component.html',
	styleUrls: ['./modal-buscar-bien-inmueble.component.scss']
})
export class ModalBuscarBienInmuebleComponent implements OnInit {

	@ViewChild('busqueda') inputBuscar!: ElementRef;
	@Input() personaDatos?: any | null;

	_codigoPersona!: number
	@Input('codigoPersona')
	set codigoPersona(value: number | null) {
		if (value) {
			this._codigoPersona = value;
		}
	}


	isLoading = false;

	filtros: {
		nombre: string,
		campo: string
	}[] = [
			{
				nombre: 'Casa',
				campo: 'CASA'
			},
			{
				nombre: 'Terreno',
				campo: 'TERRENO'
			},
		]
	bienesInmuebles: BienInmuebleResponse[] = [];

	form: FormGroup;

	constructor(
		private _bienesInmueblesService: BienesInmueblesService,
		private _modalRef: NzModalRef,
		private _messageService: MessageService,

	) {
		this.form = new FormGroup({

			busqueda: new FormControl(null, [Validators.required]),
		});
	}


	ngOnInit(): void {
		this.cargarBienesInmuebles();

		setTimeout(() => {
			this.inputBuscar.nativeElement.focus()
		}, 400)
	}

	cargarBienesInmuebles() {

		this.isLoading = true;

		this._bienesInmueblesService.buscarBienesInmuebles(this._codigoPersona,"tipoBienInmueble", 1 //parametro.busqueda
		)
			.pipe(finalize(() => this.isLoading = false))
			.subscribe(api => {
				this.bienesInmuebles = api.resultado
			}, (error) => this._messageService.showError(error.mensaje || error))
	}

	seleccionarBienInmueble(bienInmueble: BienInmuebleResponse) {
		this._modalRef.close(bienInmueble);
	}

}
