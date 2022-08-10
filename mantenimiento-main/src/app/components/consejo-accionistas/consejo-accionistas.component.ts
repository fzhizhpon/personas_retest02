import { Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { consejoAccionistas } from '../../models/ingreso-socios.model';

@Component({
  selector: 'pers-consejo-accionistas',
  templateUrl: './consejo-accionistas.component.html',
  styleUrls: ['./consejo-accionistas.component.scss']
})

export class ConsejoAccionistasComponent implements OnInit {

	// TODO: Validar que para ingresar el porcentaje total de acciones sea 100%

	form: FormGroup =this.IniFormConsejoAccionistas();

	@Input('listConsejoAccionistas')
	listConsejoAccionistas: consejoAccionistas[] = [];


	@Output()
	listConsejoAccionistasChange = new EventEmitter<consejoAccionistas[]>(true);


	constructor(
	// eslint-disable-next-line @typescript-eslint/no-empty-function
	) { }


	ngOnInit(): void {
	//	this.formChange.emit(this.form);
	//	this.form.valueChanges.subscribe(()=> {
	//	this.formChange.emit(this.form);
//		})
	}


	IniFormConsejoAccionistas() {
		return new FormGroup({
			identificacion:new FormControl('', [Validators.required]),
			razonSocial:new FormControl('', [Validators.required]),
			nacionalidad: new FormControl('', [Validators.required]),
			porcentajeAccionista:new FormControl('', [Validators.required]),
		});
	}

	NuevaFilaConsejoAccionistas() {
		if (this.form.invalid) {
			//alert('Todos los campos de persona natural son obligatorios')
			Object.values(this.form.controls).forEach(control => {
				if (control.invalid) {
					control.markAsDirty();
					control.updateValueAndValidity({ onlySelf: true });
				}
			});
			return;
		}
		this.listConsejoAccionistas.push(this.form.value);
		this.listConsejoAccionistasChange.emit(this.listConsejoAccionistas);
		this.form.setValue({
			identificacion:'',
			razonSocial:'',
			nacionalidad: '',
			porcentajeAccionista: '',
		});
		this.form.reset();
	}


	ModificarFilaConsejoAccionistas(data: consejoAccionistas, index: number) {
		this.listConsejoAccionistas[index] = (data);
		this.listConsejoAccionistasChange.emit(this.listConsejoAccionistas);

	}

	EliminarFilaConsejoAccionistas(index: number) {
		this.listConsejoAccionistas.splice(index, 1);
		this.listConsejoAccionistasChange.emit(this.listConsejoAccionistas);
	}
}
