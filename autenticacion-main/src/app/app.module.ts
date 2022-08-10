import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ModuleWithProviders, Provider } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NzInputModule } from 'ng-zorro-antd/input';

// List of providers
const providers: Provider[] = [];

@NgModule({
	declarations: [
		AppComponent,
	],
	imports: [
		BrowserModule,
		AppRoutingModule,
		NzInputModule,
	],
	providers,
	bootstrap: [AppComponent]
})
export class AppModule { }


@NgModule({})
export class AutenticacionSharedModule {
	static forRoot(): ModuleWithProviders<any> {
		return {
			ngModule: AppModule,
			providers
		};
	}
}
