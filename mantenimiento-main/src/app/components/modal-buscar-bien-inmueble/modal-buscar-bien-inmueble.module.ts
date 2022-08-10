import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MessageService } from 'src/app/services/common/message/message.service';
import { ModalBuscarBienInmuebleService } from './modal-buscar-bien-inmueble.service';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { VsTableModule } from 'src/app/components/ui/table/table.module';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModalBuscarBienInmuebleComponent } from './modal-buscar-bien-inmueble.component';
import {LanguageDirectiveModule} from 'src/app/components/ui/directives/language/language.directive.module';

@NgModule({
  declarations: [ModalBuscarBienInmuebleComponent],
  exports:[ModalBuscarBienInmuebleComponent],
  imports: [
    CommonModule,
    FormsModule,
		ReactiveFormsModule,
		NzInputModule,
		NzButtonModule,
		VsTableModule,
		NzSelectModule,
	  LanguageDirectiveModule
  ],
  providers: [
		MessageService,
		ModalBuscarBienInmuebleService,
  ]
})
export class VsModalBuscarBienInmuebleModule { }
