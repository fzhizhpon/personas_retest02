# VimaCoop

## Creación de subproyectos

>**NOTA:** Al crear el subproyecto, este se crea en la carpeta `projects`, la aplicación padre ignora los archivos dentro de esta, por ello, una vez creado el nuevo subproyecto, se debe crear los archivos para `git` del repositorio que sean necesarios.

1. Ejecutar el comando `ng g application NOMBRE`
2. Abrir el archivo `app.module.ts` del subproyecto
3. Agregar el siguiente fragmento de código:
```ts
@NgModule({})
export class NombreProyectoSharedModule {
	static forRoot(): ModuleWithProviders<any> {
		return {
			ngModule: AppModule,
			providers
		};
	}
}
```
4. En el `app.module.ts` del proyecto padre, agregar lo siguiente:
```ts
...
imports: [
    ...,
    NombreProyectoSharedModule,
    ...,
],
providers: [],
...
```
5. En el `app-routing.module.ts` del proyecto, exportar la constante de rutas y cambiar su nombre:
```ts
...
export const routesNombreProyecto: Routes = []
...
```
> **Nota**: Las rutas del subproyecto empiezan siempre con la url que se haya definido en los pasos siguientes

6. En el `app-routing.module.ts` del proyecto padre agregar la ruta (URL) para el subproyecto e importar el módulo compartido y cambiar la constante de rutas del import.
```ts
...
imports: [
    RouterModule.forRoot(routesNombreProyecto),
    ...,
    NombreProyectoSharedModule.forRoot(),
    ...,
],
...
```
En caso de no requerir la plantilla máster (ejemplo: pantalla de inicio de sesión) usar el siguiente código:
```ts
...
const routes: Routes = [
    ...,
	{
		path: 'nombreProyecto', loadChildren: () => import('../../projects/nombre_proyecto/src/app/app.module').then(m => m.NombreProyectoSharedModule),
		children: routesNombreProyecto
	}
    ...,
];
...
```
Si se requiere la plantilla máster, colocar la ruta dentro de `pages/main-layout/main-layout-routing.module.ts`:
```ts
...
const routes: Routes = [
	{
		path: '', component: MainLayoutComponent, children: [
			...,
			{
				path: 'nombreProyecto', loadChildren: () => import('../../projects/nombre_proyecto/src/app/app.module').then(m => m.NombreProyectoSharedModule),
				children: routesNombreProyecto
			}
			...,
		]
	}
];
...
```

7. En el archivo `angular.json` del proyecto padre, agregar los siguientes estilos al subproyecto:
```json
...,
"styles": [
	"../src/styles.scss",
	"../src/theme.less",
	"projects/NOMBRE_PROYECTO/src/styles.scss"
],
...,
```
