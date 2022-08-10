import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { routesMantenimiento } from 'projects/mantenimiento/src/app/app-routing.module';
import { routesMarketing } from 'projects/marketing/src/app/app-routing.module';
import { routesSocios } from 'projects/socios/src/app/app-routing.module';
import { SesionGuard } from 'src/app/guards/sesion/sesion.guard';
import { MainLayoutComponent } from './main-layout.component';

const routes: Routes = [
	{
		path: '', component: MainLayoutComponent, children: [
			{
				path: 'mantenimiento', loadChildren: () => import('../../../../projects/mantenimiento/src/app/app.module').then(m => m.MantenimientoSharedModule),
				children: routesMantenimiento,
				canActivate: [SesionGuard]
			},
			{
				path: 'marketing', loadChildren: () => import('../../../../projects/marketing/src/app/app.module').then(m => m.MarketingSharedModule),
				children: routesMarketing,
				canActivate: [SesionGuard]
			},
			{
				path: 'socios', loadChildren: () => import('../../../../projects/socios/src/app/app.module').then(m => m.SociosSharedModule),
				children: routesSocios,
				// canActivate: [SesionGuard]
			},
		]
	}
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class MainLayoutRoutingModule { }
