import { AfterViewInit, Component, forwardRef, Input } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd/message';
import { finalize } from 'rxjs/operators';
import { Respuesta } from 'src/app/models/respuesta';
import { CatalogoService } from 'src/app/services/api/catalogos/catalogo.service';
import { CatComun } from '../CatComun';

@Component({
	selector: 'vs-select-ciudad',
	templateUrl: '../base/catalogo-base.html',
	styleUrls: ['../base/catalogo-base.scss'],
	providers: [{
		provide: NG_VALUE_ACCESSOR,
		multi: true,
		useExisting: forwardRef(() => VsSelectCiudadComponent)
	}]
})
export class VsSelectCiudadComponent extends CatComun<number> implements AfterViewInit {

	_origen!: {
		codigoPais: number,
		codigoProvincia: number
	};

	@Input('origen')
	set origen(value: {
		codigoPais: number,
		codigoProvincia: number
	}) {
		this.writeValue(null)
		this._origen = value
		this.cargarDatos()
	}

	constructor(
		private _catalogoService: CatalogoService,
		private _message: NzMessageService
	) {
		super();
	}

	ngAfterViewInit(): void {
		this.cargarDatos()
	}

	cargarDatos() {
		if(this._origen.codigoPais == null || this._origen.codigoProvincia == null) return;

		this.isLoading = true
		this.tieneError = false

		this._catalogoService.obtenerPorGet<number>(`ciudades/provincia?codigoPais=${this._origen.codigoPais}&codigoProvincia=${this._origen.codigoProvincia}`)
		.pipe(finalize(() => this.isLoading = false))
		.subscribe(resp => {
			this.catalogos = resp.resultado
		}, (error: Respuesta<null>) => {
			this.tieneError = true
			this.labelError = error.mensajeUsuario
			this._message.create('error', error.mensajeUsuario);
		})
	}

}
