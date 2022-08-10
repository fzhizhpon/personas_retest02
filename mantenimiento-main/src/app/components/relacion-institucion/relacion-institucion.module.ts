import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RelacionInstitucionComponent} from "./relacion-institucion.component";
import {LanguageDirectiveModule} from 'src/app/components/ui/directives/language/language.directive.module';

// * ng-zorro
import {NzTableModule} from 'ng-zorro-antd/table';


const NzModules: any[] = [
	NzTableModule
]

@NgModule({
	declarations: [RelacionInstitucionComponent],
	exports: [RelacionInstitucionComponent],
	imports: [
		CommonModule,
		LanguageDirectiveModule,
		NzTableModule
	]
})
export class RelacionInstitucionModule {
}
