import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { IngresoSociosMockRoutingModule } from './ingreso-socios-mock-routing.module';
import { IngresoSociosMockComponent } from './ingreso-socios-mock.component';
import { PanelAccionesModule } from 'src/app/components/ui/panel-acciones/panel-acciones.module';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzStepsModule } from 'ng-zorro-antd/steps';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { VsTabsModule } from 'src/app/components/ui/tabs/tabs.module';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';

import { PersTrabajosModule } from '../../components/trabajos/trabajos.module';
import { PersDireccionModule } from '../../components/direccion/direccion.module';
import { PersCelularesModule } from '../../components/celulares-socio/celulares-socio.module';
import { PersReferenciaPersonalModule } from '../../components/referencia-personal/referencia-personal.module';
import { PersReferenciaFinancieraModule } from '../../components/referencia-financiera/referencia-financiera.module';
import { PersReferenciaComercialModule } from '../../components/referencia-comercial/referencia-comercial.module';
import { ConsejoAccionistasModule } from '../../components/consejo-accionistas/consejo-accionistas.module';
import { PersDatosGeneralesModule } from '../../components/datos-generales/datos-generales.module';
import { PersonaModule } from '../../components/persona/persona.module';
import { DigitalizacionDocumentosModule } from '../../components/digitalizacion-documentos/digitalizacion-documentos.module';
import { PersCorreosElectronicosModule } from '../../components/correos-electronicos/correos-electronicos.module';
import { VsModalBuscarPersonaModule } from '../../components/modal-buscar-persona/modal-buscar-persona.module';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzTimelineModule } from 'ng-zorro-antd/timeline';
import { PersRepresentantesModule } from '../../components/representantes/representantes.module';
import { PersFamiliaresModule } from '../../components/familiares/familiares.module';
import { PersEstadoFinancieroModule } from '../../components/estado-financiero/estado-financiero.module';
import { PersBienesMueblesModule } from '../../components/bienes-muebles/bienes-muebles.module';
import { PersBienesInmueblesModule } from '../../components/bienes-inmuebles/bienes-inmuebles.module';
import { PersBienesModule } from '../../components/bienes/bienes.module';
import {LanguageDirectiveModule} from 'src/app/components/ui/directives/language/language.directive.module';
import { PersInformacionAdicionalModule } from '../../components/informacion-adicional/informacion-adicional.module';
import {RelacionInstitucionModule} from "../../components/relacion-institucion/relacion-institucion.module";

@NgModule({
	declarations: [
		IngresoSociosMockComponent
	],
	imports: [
		CommonModule,
		IngresoSociosMockRoutingModule,
		PanelAccionesModule,
		NzIconModule,
		NzStepsModule,
		NzInputModule,
		NzButtonModule,
		NzSelectModule,
		VsTabsModule,
		NzCheckboxModule,
		NzTimelineModule,
		PersDireccionModule,
		PersCelularesModule,
		PersReferenciaPersonalModule,
		PersTrabajosModule,
		PersReferenciaFinancieraModule,
		PersReferenciaComercialModule,
		ConsejoAccionistasModule,
		PersDatosGeneralesModule,
		PersonaModule,
		DigitalizacionDocumentosModule,
		PersCorreosElectronicosModule,
		VsModalBuscarPersonaModule,
		NzModalModule,
		PersRepresentantesModule,
		PersFamiliaresModule,
		PersEstadoFinancieroModule,
		PersBienesMueblesModule,
		PersBienesInmueblesModule,
		PersBienesModule,
		LanguageDirectiveModule,
		PersInformacionAdicionalModule,
		RelacionInstitucionModule
	]
})
export class IngresoSociosMockModule { }
