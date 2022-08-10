import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ModuleWithProviders, Provider } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

// List of providers
const providers: Provider[] = [];

@NgModule({
	declarations: [
		AppComponent
	],
	imports: [
		BrowserModule,
		AppRoutingModule
	],
	providers,
	bootstrap: [AppComponent]
})
export class AppModule { }

@NgModule({})
export class MantenimientoSharedModule {
	static forRoot(): ModuleWithProviders<any> {
		return {
			ngModule: AppModule,
			providers
		};
	}
}
