import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {NoSesionGuard} from 'src/app/guards/sesion/no-sesion.guard';

const routes: Routes = [
	{
		path: 'autenticacion',
		children: [
			{
				path: '',
				loadChildren: () => import('./pages/acceso/acceso.module').then(m => m.AccesoModule),
				canActivate: [NoSesionGuard]
			},
		],
	},
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule {
}
