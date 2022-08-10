import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'projects/autenticacion/src/environments/environment';
import { Empresa } from '../../../models/empresa';
import { Observable } from 'rxjs';
import { Sucursal } from '../../../models/sucursal';
import { Agencia } from '../../../models/agencia';
import { Periodo } from '../../../models/periodo';
import { ObtenerSucursalDto } from '../../../models/dtos/obtener-sucursal-dto';
import { ObtenerAgenciaDto } from '../../../models/dtos/obtener-agencia-dto';
import { ObtenerPeriodoDto } from '../../../models/dtos/obtener-periodo-dto';
import { Respuesta } from 'src/app/models/respuesta';

@Injectable({
	providedIn: 'root'
})
export class ParametrosService {

	readonly url: string = environment.parametros;

	constructor(private http: HttpClient) { }

	obtenerEmpresa(): Observable<Respuesta<Empresa>> {
		return this.http.get<Respuesta<Empresa>>(`${this.url}/empresa`);
	}

	obtenerSucursales(body: ObtenerSucursalDto): Observable<Respuesta<Sucursal[]>> {
		return this.http.post<Respuesta<Sucursal[]>>(`${this.url}/sucursales`, body);
	}

	obtenerAgencias(body: ObtenerAgenciaDto): Observable<Respuesta<Agencia[]>> {
		return this.http.post<Respuesta<Agencia[]>>(`${this.url}/agencias`, body);
	}

	obtenerPeriodos(body: ObtenerPeriodoDto): Observable<Respuesta<Periodo[]>> {
		return this.http.post<Respuesta<Periodo[]>>(`${this.url}/periodos`, body);
	}

}
