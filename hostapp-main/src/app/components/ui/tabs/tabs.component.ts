import { AfterContentInit, Component, ContentChildren, EventEmitter, Input, Output, QueryList } from '@angular/core';
import { TabComponent } from './tab/tab.component';

@Component({
	selector: 'vs-tabs',
	templateUrl: './tabs.component.html',
	styleUrls: ['./tabs.component.scss']
})
export class TabsComponent implements AfterContentInit {

	@ContentChildren(TabComponent) tabs!: QueryList<TabComponent>;

	index!: number;

	@Input('index') set indexSetter(value: number) {
		this.index = value;

		if(value != null && this.tabs) {
			this.selectTab(this.tabs.toArray()[value])
		}
	}

	@Output('indexChange') indexEmmiter = new EventEmitter<number>();

	@Input('bodyClass') bodyClass = 'w-full p-20';
	@Input('bodyStyle') bodyStyle = '';

	ngAfterContentInit(): void {
		const activeTabs = this.tabs.filter((tab) => tab.selected);

		// if there is no active tab set, activate the first
		if(activeTabs.length === 0) {
			if(this.index != null) {
				this.selectTab(this.tabs.toArray()[this.index])
			} else {
				this.selectTab(this.tabs.first);
			}
		}
	}

	selectTab(tab: TabComponent) {
		if(!tab.disabled) {
			const tabs = this.tabs.toArray();
			for(let i = 0; i < tabs.length; i++) {
				tabs[i].selected = false

				if(tabs[i] == tab) {
					tab.selected = true
					this.indexEmmiter.emit(i)
				}
			}
		}
	}

}
