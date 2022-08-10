import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {DigitalizacionDocumentosComponent} from './digitalizacion-documentos.component';
import {VsTableModule} from 'src/app/components/ui/table/table.module';
import {NzButtonModule} from 'ng-zorro-antd/button';
import {NzIconModule} from 'ng-zorro-antd/icon';
import {NzRadioModule} from 'ng-zorro-antd/radio';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {NzToolTipModule} from 'ng-zorro-antd/tooltip';
import {NzSpinModule} from 'ng-zorro-antd/spin';
import {VsDocumentViewerModule} from './document-viewer/document-viewer.module';
import {ModalSubirDocumentoComponent} from './modal-subir-documento/modal-subir-documento.component';
import {NzModalModule} from 'ng-zorro-antd/modal';
import {NzUploadModule} from 'ng-zorro-antd/upload';
import {NzInputModule} from 'ng-zorro-antd/input';
import {NzDatePickerModule} from 'ng-zorro-antd/date-picker';
import {LanguageDirectiveModule} from 'src/app/components/ui/directives/language/language.directive.module';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';


@NgModule({
	declarations: [
		DigitalizacionDocumentosComponent,
		ModalSubirDocumentoComponent,
	],
	exports: [DigitalizacionDocumentosComponent],
	imports: [
		CommonModule,
		FormsModule,
		ReactiveFormsModule,
		VsDocumentViewerModule,
		NzButtonModule,
		NzIconModule,
		VsTableModule,
		NzRadioModule,
		NzToolTipModule,
		NzSpinModule,
		NzModalModule,
		NzUploadModule,
		NzInputModule,
		NzDatePickerModule,
		LanguageDirectiveModule,
		NzPopconfirmModule,
	]
})
export class DigitalizacionDocumentosModule {
}
