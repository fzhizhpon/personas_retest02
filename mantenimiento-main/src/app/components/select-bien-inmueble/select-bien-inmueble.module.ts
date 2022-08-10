import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {NzButtonModule} from 'ng-zorro-antd/button';
import {NzIconModule} from 'ng-zorro-antd/icon';
import {NzInputModule} from 'ng-zorro-antd/input';
import {NzModalModule} from 'ng-zorro-antd/modal';
import {NzToolTipModule} from 'ng-zorro-antd/tooltip';
import {VsModalBuscarBienInmuebleModule} from '../modal-buscar-bien-inmueble/modal-buscar-bien-inmueble.module';
import {VsSelectBienInmuebleComponent} from './select-bien-inmueble.component';
import {LanguageDirectiveModule} from 'src/app/components/ui/directives/language/language.directive.module';
import { PersBienesInmueblesModule } from '../bienes-inmuebles/bienes-inmuebles.module';

@NgModule({
	declarations: [
		VsSelectBienInmuebleComponent
	],
	exports: [VsSelectBienInmuebleComponent],
	imports: [
		CommonModule,
		NzInputModule,
		NzButtonModule,
		NzIconModule,
		NzToolTipModule,
		NzModalModule,
		VsModalBuscarBienInmuebleModule,
		LanguageDirectiveModule,
		PersBienesInmueblesModule,
	]
})
export class VsSelectBienInmuebleModule {
}
