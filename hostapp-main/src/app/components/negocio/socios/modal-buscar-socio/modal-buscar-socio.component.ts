/* eslint-disable @typescript-eslint/no-explicit-any */
import { HttpBackend, HttpClient, HttpHandler } from '@angular/common/http';
import { Component } from '@angular/core';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { FakerService } from 'src/app/services/faker/faker.service';

@Component({
	selector: 'app-modal-buscar-socio',
	templateUrl: './modal-buscar-socio.component.html',
	styleUrls: ['./modal-buscar-socio.component.scss']
})
export class ModalBuscarSocioComponent {

	socios: any[];
	cargando = false;

	constructor(
		private _modal: NzModalRef,
		public _faker: FakerService, // QUITAR CUANDO SE CONSUMA EL SERVICIO REAL
	) {
		this.socios = []
	}

	buscarSocio() {
		this.cargando = true;

		const names = this._faker.getNames(20)
		const socios: any[] = []

		names.forEach(name => {
			socios.push({
				id: this._faker.getRandomNumber(1234, 9999),
				name: name
			})
		})

		setTimeout(() => {
			this.socios = socios
			this.cargando = false
		}, 700)
	}

	seleccionarSocio(codigo: number) {
		this._modal.close(codigo)
	}

	cerrarModal() {
		this._modal.close()
	}

}
