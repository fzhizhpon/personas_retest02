import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {VsPersonaDocComponent} from './persona-doc.component';

// * vima
import {VsDocumentViewerModule} from '../digitalizacion-documentos/document-viewer/document-viewer.module';
import {LanguageDirectiveModule} from 'src/app/components/ui/directives/language/language.directive.module';
import {PersDatosGeneralesModule} from "../datos-generales/datos-generales.module";

// * ng-zorro
import {NzIconModule} from 'ng-zorro-antd/icon';
import {NzSpinModule} from 'ng-zorro-antd/spin';
import {NzImageModule} from 'ng-zorro-antd/image';
import {NzButtonModule} from 'ng-zorro-antd/button';

const nzVima = [VsDocumentViewerModule, LanguageDirectiveModule, PersDatosGeneralesModule]
const nzModules = [NzIconModule, NzSpinModule, NzImageModule, NzButtonModule];


@NgModule({
	declarations: [
		VsPersonaDocComponent
	],
	exports: [
		VsPersonaDocComponent
	],
	imports: [
		CommonModule,
		nzModules,
		nzVima
	]
})
export class PersonaDocModule {
}
