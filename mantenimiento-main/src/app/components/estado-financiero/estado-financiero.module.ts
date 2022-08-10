import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PersEstadoFinancieroComponent } from './estado-financiero.component';
import { VsTableModule } from 'src/app/components/ui/table/table.module';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { FormsModule } from '@angular/forms';
import { PersTrabajosModule } from '../trabajos/trabajos.module';
import { PersReferenciaFinancieraModule } from '../referencia-financiera/referencia-financiera.module';
import { PersBienesModule } from '../bienes/bienes.module';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { LanguageDirectiveModule } from 'src/app/components/ui/directives/language/language.directive.module';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzPopoverModule } from 'ng-zorro-antd/popover';
import { TrimDirectiveModule } from 'src/app/components/ui/directives/trim/trim.directive.module';

@NgModule({
	declarations: [PersEstadoFinancieroComponent],
	exports: [PersEstadoFinancieroComponent],
	imports: [
		CommonModule,
		FormsModule,
		VsTableModule,
		NzInputModule,
		NzButtonModule,
		NzToolTipModule,
		NzIconModule,
		NgxChartsModule,
		NzTabsModule,
		NzIconModule,
		PersTrabajosModule,
		PersReferenciaFinancieraModule,
		PersBienesModule,
		NzModalModule,
		NzInputNumberModule,
		LanguageDirectiveModule,
		NzPopoverModule,
		TrimDirectiveModule,
	]
})
export class PersEstadoFinancieroModule { }
