import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { finalize } from 'rxjs/operators';
import { CatalogoService } from 'src/app/services/api/catalogos/catalogo.service';
import { Lugar } from '../../catalogo';

@Component({
	selector: 'app-modal-lugar',
	templateUrl: './modal-lugar.component.html',
	styleUrls: ['../select-lugar.component.scss']
})
export class ModalLugarComponent implements OnInit {

	@ViewChild('buscar') inputBusqueda!: ElementRef;

	isLoading = false;

	form: FormGroup;
	lugares: Lugar[] = [];

	constructor(
		private _catalogoService: CatalogoService,
		private _modalRef: NzModalRef,
	) {
		this.form = new FormGroup({
			busqueda: new FormControl(null, [Validators.required]),
		})
	}

	ngOnInit(): void {
		setTimeout(() => {
			this.inputBusqueda.nativeElement.focus()
		}, 500);
	}

	cargarLugares()
	{
		if(this.form.invalid) {
			this.form.markAllAsTouched()
			return
		}

		this.isLoading = true;

		let valorBuscado = this.form.get('busqueda')?.value;

		if (valorBuscado.charAt(0) != '%') valorBuscado = '%' + valorBuscado;
		if (valorBuscado.charAt(valorBuscado.length - 1) != '%') valorBuscado = valorBuscado + '%';

		this._catalogoService.obtenerLugares({
			'descripcionLugar': valorBuscado
		})
		.pipe(finalize(() => this.isLoading = false))
		.subscribe(api => {
			this.lugares = api.resultado;
		})
	}

	seleccionarLugar(lugar: Lugar) {
		this._modalRef.close(lugar)
	}

}
