import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { VsModalBuscarPersonaModule } from '../modal-buscar-persona/modal-buscar-persona.module';
import { VsSelectPersonaComponent } from './select-persona.component';
import {LanguageDirectiveModule} from 'src/app/components/ui/directives/language/language.directive.module';

@NgModule({
	declarations: [VsSelectPersonaComponent],
	exports: [VsSelectPersonaComponent],
	imports: [
		CommonModule,
		NzInputModule,
		NzButtonModule,
		VsModalBuscarPersonaModule,
		NzIconModule,
		NzToolTipModule,
		NzModalModule,
		LanguageDirectiveModule
	],
})
export class VsSelectPersonaModule { }
