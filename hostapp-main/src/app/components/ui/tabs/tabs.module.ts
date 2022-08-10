import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TabsComponent } from './tabs.component';
import { TabComponent } from './tab/tab.component';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { TabsHeaderComponent } from './tabs-header/tabs-header.component';


@NgModule({
	declarations: [ TabsComponent, TabComponent, TabsHeaderComponent ],
	exports: [ TabsComponent, TabComponent, TabsHeaderComponent ],
	imports: [
		CommonModule,
		NzIconModule,
	]
})
export class VsTabsModule { }
