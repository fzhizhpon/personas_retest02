import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PersonaComponent } from './persona.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { VsSelectTipoPersonaModule } from 'src/app/components/catalogos/select-tipo-persona/vs-select-tipo-persona.module';
import { VsSelectTipoIdentificacionModule } from 'src/app/components/catalogos/select-tipo-identificacion/vs-select-tipo-identificacion.module';
import { VsSelectPaisModule } from 'src/app/components/catalogos/select-pais/vs-select-pais.module';
import { NzImageModule } from 'ng-zorro-antd/image';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { VsDocumentViewerModule } from '../digitalizacion-documentos/document-viewer/document-viewer.module';
import {LanguageDirectiveModule} from 'src/app/components/ui/directives/language/language.directive.module';


@NgModule({
	declarations: [PersonaComponent],
	exports: [PersonaComponent],
	imports: [
		CommonModule,
		FormsModule,
		ReactiveFormsModule,
		NzInputModule,
		NzButtonModule,
		NzInputNumberModule,
		NzImageModule,
		NzIconModule,
		NzToolTipModule,
		NzSelectModule,
		NzSpinModule,
		VsSelectTipoPersonaModule,
		VsSelectTipoIdentificacionModule,
		VsSelectPaisModule,
		VsDocumentViewerModule,
		LanguageDirectiveModule
	]
})
export class PersonaModule { }
