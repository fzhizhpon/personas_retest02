import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

export const routesMantenimiento: Routes = [{
	path: '',
	children: [
		{
			path: 'ingresosocios-mock',
			loadChildren: () => import('./pages/ingreso-socios-mock/ingreso-socios-mock.module').then(m => m.IngresoSociosMockModule)
		}
	]
}];



@NgModule({
	imports: [RouterModule.forChild(routesMantenimiento)],
	exports: [RouterModule]
})
export class AppRoutingModule { }
