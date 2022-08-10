import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {AccesoRoutingModule} from './acceso-routing.module';
import {AccesoComponent} from './acceso.component';
import {NzInputModule} from 'ng-zorro-antd/input';
import {NzButtonModule} from 'ng-zorro-antd/button';
import {NzSelectModule} from 'ng-zorro-antd/select';
import {NzCheckboxModule} from 'ng-zorro-antd/checkbox';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {LanguageDirectiveModule} from 'src/app/components/ui/directives/language/language.directive.module';
import {NzIconModule} from 'ng-zorro-antd/icon';
import {VisualizarCarruselModule} from 'src/app/components/ui/visualizar-carrusel/visualizar-carrusel.module';
import { VisualizarImagenModule } from 'src/app/components/ui/visualizar-imagen/visualizar-imagen.module';
import { NzMessageModule } from 'ng-zorro-antd/message';
import {MessageService} from 'src/app/services/common/message/message.service';
import { NzModalModule } from 'ng-zorro-antd/modal';

@NgModule({
	declarations: [
		AccesoComponent
	],
	imports: [
		CommonModule,
		FormsModule,
		ReactiveFormsModule,
		AccesoRoutingModule,
		NzInputModule,
		NzButtonModule,
		NzSelectModule,
		NzCheckboxModule,
		LanguageDirectiveModule,
		NzIconModule,
		VisualizarCarruselModule,
		VisualizarImagenModule,
		NzMessageModule,
		NzSelectModule,
		NzModalModule,
	],
	providers: [
		MessageService
	]
})
export class AccesoModule {
}
