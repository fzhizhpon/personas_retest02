import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DocumentViewerComponent } from './document-viewer.component';
import { FormsModule } from '@angular/forms';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzImageModule } from 'ng-zorro-antd/image';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzInputModule } from 'ng-zorro-antd/input';

@NgModule({
	declarations: [DocumentViewerComponent],
	exports: [DocumentViewerComponent],
	imports: [
		CommonModule,
		FormsModule,
		NzIconModule,
		NzInputModule,
		NzButtonModule,
		NzImageModule,
		NzModalModule,
		PdfViewerModule,
	]
})
export class VsDocumentViewerModule { }
