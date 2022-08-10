import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AutenticacionSharedModule } from 'projects/autenticacion/src/app/app.module';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NZ_I18N } from 'ng-zorro-antd/i18n';
import { es_ES } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import es from '@angular/common/locales/es';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MantenimientoSharedModule } from 'projects/mantenimiento/src/app/app.module';
import { AuthInterceptorService } from './services/interceptors/auth/auth-interceptor.service';
import { MarketingSharedModule } from 'projects/marketing/src/app/app.module';
import { SociosSharedModule } from 'projects/socios/src/app/app.module';

registerLocaleData(es);

@NgModule({
	declarations: [
		AppComponent
	],
	imports: [
		BrowserModule,
		AppRoutingModule,
		AutenticacionSharedModule,
		MantenimientoSharedModule,
		MarketingSharedModule,
		SociosSharedModule,
		FormsModule,
		HttpClientModule,
		BrowserAnimationsModule,
	],
	providers: [
		{ provide: NZ_I18N, useValue: es_ES },
		{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorService, multi: true },
	],
	bootstrap: [AppComponent]
})
export class AppModule { }
