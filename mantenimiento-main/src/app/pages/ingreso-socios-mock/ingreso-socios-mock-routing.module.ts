import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IngresoSociosMockComponent } from './ingreso-socios-mock.component';

const routes: Routes = [
	{ path: '', component: IngresoSociosMockComponent }
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class IngresoSociosMockRoutingModule { }
