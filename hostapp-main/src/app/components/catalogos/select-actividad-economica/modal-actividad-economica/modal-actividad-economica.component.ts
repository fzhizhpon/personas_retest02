import { Catalogo } from '../../catalogo';
import { CatalogoService } from 'src/app/services/api/catalogos/catalogo.service';
import {
	Component,
	ElementRef,
	OnInit,
	ViewChild
	} from '@angular/core';
import { finalize } from 'rxjs/operators';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NzModalRef } from 'ng-zorro-antd/modal';

@Component({
	selector: 'app-modal-actividad-economica',
	templateUrl: './modal-actividad-economica.component.html',
	styleUrls: ['../select-actividad-economica.component.scss']
})
export class ModalActividadEconomicaComponent implements OnInit {

	@ViewChild('buscar') inputBusqueda!: ElementRef;

	isLoading = false;

	form: FormGroup;
	actividades: Catalogo<number>[] = [];

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

		this._catalogoService.obtenerPorGet<number>('ActividadesEconomicas', {
			descripcion: valorBuscado
		})
		.pipe(finalize(() => this.isLoading = false))
		.subscribe(api => {
			this.actividades = api.resultado;
		})
	}

	seleccionarActividad(actividad: Catalogo<number>) {
		this._modalRef.close(actividad)
	}

}
