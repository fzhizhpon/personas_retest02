import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PanelAccionesComponent } from './panel-acciones.component';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { IconDefinition } from '@ant-design/icons-angular';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { PlusOutline, SaveOutline, PrinterOutline, DeleteOutline, EditOutline, SearchOutline } from '@ant-design/icons-angular/icons';

const icons: IconDefinition[] = [ PlusOutline, SaveOutline, PrinterOutline, DeleteOutline, EditOutline, SearchOutline ];

@NgModule({
	declarations: [PanelAccionesComponent],
	exports: [PanelAccionesComponent],
	imports: [
		CommonModule,
		NzToolTipModule,
		NzIconModule.forChild(icons)
	]
})
export class PanelAccionesModule { }
