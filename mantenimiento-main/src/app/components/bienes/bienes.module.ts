import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BienesComponent } from './bienes.component';

import { VsTableModule } from 'src/app/components/ui/table/table.module';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { FormsModule } from '@angular/forms';
import { PersBienesInmueblesModule } from '../bienes-inmuebles/bienes-inmuebles.module';
import { PersBienesMueblesModule } from '../bienes-muebles/bienes-muebles.module';
import { PersBienesIntangiblesModule } from '../bienes-intangibles/bienes-intangibles.module';
import { VsTabsModule } from 'src/app/components/ui/tabs/tabs.module';
import { LanguageDirectiveModule } from 'src/app/components/ui/directives/language/language.directive.module';

@NgModule({
	declarations: [BienesComponent],
	exports: [BienesComponent],
	imports: [
		CommonModule,
		FormsModule,
		NzInputModule,
		NzButtonModule,
		NzToolTipModule,
		NzIconModule,
		NgxChartsModule,
		NzTabsModule,
		NzIconModule,
		PersBienesMueblesModule,
		PersBienesInmueblesModule,
		PersBienesIntangiblesModule,
		VsTabsModule,
		LanguageDirectiveModule,
	]
})
export class PersBienesModule { }
