import { AfterViewInit, Component, forwardRef } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd/message';
import { finalize } from 'rxjs/operators';
import { Respuesta } from 'src/app/models/respuesta';
import { CatalogoService } from 'src/app/services/api/catalogos/catalogo.service';
import { CatComun } from '../CatComun';

@Component({
	selector: 'vs-select-estado-civil',
	templateUrl: '../base/catalogo-base.html',
	styleUrls: ['../base/catalogo-base.scss'],
	providers: [{
		provide: NG_VALUE_ACCESSOR,
		multi: true,
		useExisting: forwardRef(() => VsSelectEstadoCivilComponent)
	}]
})
export class VsSelectEstadoCivilComponent extends CatComun<number> implements AfterViewInit {

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
		this.isLoading = true
		this.tieneError = false

		this._catalogoService.obtenerPorGet<number>('estados-civiles')
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
