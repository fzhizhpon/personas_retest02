import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ModalBuscarPersonaComponent} from './modal-buscar-persona.component';
import {NzInputModule} from 'ng-zorro-antd/input';
import {NzButtonModule} from 'ng-zorro-antd/button';
import {VsTableModule} from 'src/app/components/ui/table/table.module';
import {NzSelectModule} from 'ng-zorro-antd/select';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MessageService} from 'src/app/services/common/message/message.service';
import {ModalBuscarPersonaService} from './modal-buscar-persona.service';
import {LanguageDirectiveModule} from 'src/app/components/ui/directives/language/language.directive.module';
import {NzIconModule} from 'ng-zorro-antd/icon';

@NgModule({
	declarations: [ModalBuscarPersonaComponent],
	exports: [ModalBuscarPersonaComponent],
	imports: [
		CommonModule,
		FormsModule,
		ReactiveFormsModule,
		NzInputModule,
		NzButtonModule,
		VsTableModule,
		NzSelectModule,
		LanguageDirectiveModule,
		NzIconModule
	],
	providers: [
		MessageService,
		ModalBuscarPersonaService,
	]
})
export class VsModalBuscarPersonaModule {
}
