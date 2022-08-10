using Catalogo.Application.Services;
using Catalogo.Core.Interfaces.DataBase;
using Catalogo.Core.Interfaces.IRepositories;
using Catalogo.Core.Interfaces.IServices;
using Catalogo.Infrastructure.Configuraciones;
using Catalogo.Infrastructure.Data;
using Catalogo.Infrastructure.Options;
using Catalogo.Infrastructure.Repositories;
using Catalogo.Infrastructure.Validadores.Provincia;
using Consul;
using CoopCrea.Cross.Cache.Src;
using CoopCrea.Cross.Discovery.Consul;
using CoopCrea.Cross.Discovery.Fabio;
using CoopCrea.Cross.Discovery.Mvc;
using CoopCrea.Cross.Log.Src;
using CoopCrea.Cross.Tracing.Src;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oracle.ManagedDataAccess.Client;
using System.Threading;
using Vimasistem.QueryFilter.Implementations;
using Vimasistem.QueryFilter.Interfaces;

namespace Catalogo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            ThreadPool.SetMinThreads(50, 50);

            services.AddSingleton<IPagination, OraclePagination>();

            /*Inicio Repositories*/
            services.AddTransient<ITipoSangreRepository, TipoSangreRepository>();
            services.AddTransient<ITipoPersonaRepository, TipoPersonaRepository>();
            services.AddTransient<ITipoIdentificacionRepository, TipoIdentificacionRepository>();
            services.AddTransient<IPaisRepository, PaisRepository>();
            services.AddTransient<IMensajeRespuestaRepository, MensajeRespuestaRepository>();
            services.AddTransient<IProvinciaRepository, ProvinciaRepository>();
            services.AddTransient<ICiudadRepository, CiudadRepository>();
            services.AddTransient<IParroquiaRepository, ParroquiaRepository>();
            services.AddTransient<ITipoEtniaRepository, TipoEtniaRepository>();
            services.AddTransient<ITipoTrabajoRepository, TipoTrabajoRepository>();
            services.AddTransient<ITipoTiempoRepository, TipoTiempoRepository>();
            services.AddTransient<IIndustriaRepository, IndustriaRepository>();
            services.AddTransient<IOperadoraMovilRepository, OperadoraMovilRepository>();
            services.AddTransient<IOperadoraFijaRepository, OperadoraFijaRepository>();
            services.AddTransient<ITipoCuentaFinancieraRepository, TipoCuentaFinancieraRepository>();
            services.AddTransient<IGeneroRepository, GeneroRepository>();
            services.AddTransient<IEstadoCivilRepository, EstadoCivilRepository>();
            services.AddTransient<ITipoDiscapacidadRepository, TipoDiscapacidadRepository>();
            services.AddTransient<ITipoActividadTrabajoRepository, TipoActividadTrabajoRepository>();
            services.AddTransient<IProfesionRepository, ProfesionRepository>();
            services.AddTransient<IParentescoRepository, ParentescoRepository>();
            services.AddTransient<INivelInstruccionRepository, NivelInstruccionRepository>();
            services.AddTransient<ISucursalRepository, SucursalRepository>();
            services.AddTransient<IAgenciaRepository, AgenciaRepository>();
            services.AddTransient<IEmpresaRepository, EmpresaRepository>();
            services.AddTransient<ITipoContribuyenteRepository, TipoContribuyenteRepository>();
            services.AddTransient<ILugaresRepository, LugaresRepository>();
            services.AddTransient<ITipoSociedadRepository, TipoSociedadRepository>();
            services.AddTransient<IActividadesEconomicasRepository, ActividadesEconomicasRepository>();
            services.AddTransient<ITipoRepresentanteRepository, TipoRepresentanteRepository>();
            services.AddTransient<ITipoResidenciaRepository, TipoResidenciaRepository>();
            services.AddTransient<IPersistenciaRepository, PersistenciaRepository>();
            services.AddTransient<ITiposInstitucionesFinancierasRepository, TiposInstitucionesFinancierasRepository>();
            services.AddTransient<IInstitucionesFinancierasRepository, InstitucionesFinancierasRepository>();
            services.AddTransient<ITiposBienesInmueblesRepository, TiposBienesInmueblesRepository>();
            services.AddTransient<ITiposBienesMueblesRepository, TiposBienesMueblesRepository>();
            services.AddTransient<ITiposBienesIntangiblesRepository, TiposBienesIntangiblesRepository>();
            services.AddTransient<ITablasComunesCabeceraRepository, TablasComunesCabeceraRepository>();
            services.AddTransient<ITablasComunesDetallesReposity, TablasComunesDetallesRepository>();

            services.AddTransient<ITipoAutenticacionRepository, TipoAutenticacionRepository > ();
            services.AddTransient<IEstadoUsuarioRepository, EstadoUsuarioRepository>();
            services.AddTransient<INombreCargosRepository, NombreCargosRepository>();
            services.AddTransient<IUsuarioDataBaseRepository, UsuariosDataBaseRepository>();
            services.AddTransient<IDepartamentoRepository, DepartamentosRepository>();

            services.AddTransient<IMonedaRepository, MonedaRepository>();
            services.AddTransient<IProductosRepository, ProductosRepository>();
            services.AddTransient<ITipoFirmanteRepository, TipoFirmanteRepository>();
            services.AddTransient<ITipoCuentaRepository, TipoCuentaRepository>();
            services.AddTransient<ICaracteristicaCuentaRepository, CaracteristicaCuentaRepository>();

            /*Fin Repositories*/

            /*Inicio Services*/
            services.AddTransient<ITipoSangreService, TipoSangreService>();
            services.AddTransient<ITipoPersonaService, TipoPersonaService>();
            services.AddTransient<ITipoIdentificacionService, TipoIdentificacionService>();
            services.AddTransient<IPaisService, PaisService>();
            services.AddTransient<IProvinciaService, ProvinciaService>();
            services.AddTransient<ICiudadService, CiudadService>();
            services.AddTransient<IParroquiaService, ParroquiaService>();
            services.AddTransient<ITipoEtniaService, TipoEtniaService>();
            services.AddTransient<ITipoTrabajoService, TipoTrabajoService>();
            services.AddTransient<ITipoTiempoService, TipoTiempoService>();
            services.AddTransient<IIndustriaService, IndustriaService>();
            services.AddTransient<IOperadoraMovilService, OperadoraMovilService>();
            services.AddTransient<IOperadoraFijaService, OperadoraFijaService>();
            services.AddTransient<ITipoCuentaFinancieraService, TipoCuentaFinancieraService>();
            services.AddTransient<IGeneroService, GeneroService>();
            services.AddTransient<IEstadoCivilService, EstadoCivilService>();
            services.AddTransient<ITipoDiscapacidadService, TipoDiscapacidadService>();
            services.AddTransient<ITipoActividadTrabajoService, TipoActividadTrabajoService>();
            services.AddTransient<IProfesionService, ProfesionService>();
            services.AddTransient<IParentescoService, ParentescoService>();
            services.AddTransient<INivelInstruccionService, NivelInstruccionService>();
            services.AddTransient<ISucursalService, SucursalService>();
            services.AddTransient<IAgenciaService, AgenciaService>();
            services.AddTransient<IEmpresaService, EmpresaService>();
            services.AddTransient<ITipoContribuyenteService, TipoContribuyenteService>();
            services.AddTransient<ILugaresService, LugaresService>();
            services.AddTransient<ITipoSociedadService, TipoSociedadService>();
            services.AddTransient<IActividadesEconomicasService, ActividadesEconomicasService>();
            services.AddTransient<ITipoRepresentanteService, TipoRepresentanteService>();
            services.AddTransient<ITipoResidenciaService, TipoResidenciaService>();
            services.AddTransient<IPersistenciaService, PersistenciaService>();
            services.AddTransient<ITiposInstitucionesFinancierasService, TiposInstitucionesFinancierasService>();
            services.AddTransient<IInstitucionesFinancierasService, InstitucionesFinancierasService>();
            services.AddTransient<ITiposBienesInmueblesService, TiposBienesInmueblesService>();
            services.AddTransient<ITiposBienesMueblesService, TiposBienesMueblesService>();
            services.AddTransient<ITiposBienesIntangiblesService, TiposBienesIntangiblesService>();
            services.AddTransient<ITablasComunesCabeceraService, TablasComunesCabeceraService>();
            services.AddTransient<ITablasComunesDetallesService, TablasComunesDetallesService>();
            services.AddTransient<ITipoAutenticacionService, TipoAutenticacionService>();

            services.AddTransient<ITipoAutenticacionService, TipoAutenticacionService>();
            services.AddTransient<IEstadoUsuarioService, EstadoUsuarioService>();
            services.AddTransient<INombreCargosService, NombreCargosService>();
            services.AddTransient<IUsuarioDatabaseService, UsuarioDatabaseService>();
            services.AddTransient<IDepartamentosService, DepartamentoService>();

            services.AddTransient<IMonedaService, MonedaService>();
            services.AddTransient<IProductosService, ProductosService>();
            services.AddTransient<ITipoFirmanteService, TipoFirmanteService>();
            services.AddTransient<ITipoCuentaService, TipoCuentaService>();
            services.AddTransient<ICaracteristicaCuentaService, CaracteristicaCuentaService>();


            /*Fin Services*/


            /*Inicio DB*/
            services.AddTransient<IConexion<OracleConnection>, ConexionOracle>();

            var nombreConexion = Configuration.GetValue<string>("ActiveDataBase");
            services.Configure<DbConexionOption>(options =>
                Configuration.GetSection("ConnectionObjects:" + nombreConexion).Bind(options));
            /*Fin DB*/

            /*Inicio Fluent*/

            services.AddControllers().AddFluentValidation(cfg =>
                cfg.RegisterValidatorsFromAssemblyContaining<ObtenerProvinciaPaisDtoValidador>());

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            services.AddMvc(options => { options.Filters.Add(typeof(ValidationResultAttribute)); })
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddTransient<IValidatorInterceptor, ValidadorInterceptor>();
            /*Fin Fluent*/

            /*Inicio - Redis Cache*/
            services.AddRedis();
            services.AddSingleton<IExtensionCache, ExtensionCache>();
            /*Fin - Redis Cache*/

            /*Inicio - Consul*/
            services.AddSingleton<IServiceId, ServiceId>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddConsul();
            /*Fin - Consul*/

            /*Inicio - Fabio*/
            services.AddFabio();
            /*Fin - Fabio*/

            /*Inicio - Tracer distributed*/
            services.AddJaeger();
            services.AddOpenTracing();
            /*Fin - Tracer distributed*/

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "VimaCoop | Catalogo";
                    document.Info.Description = "Endpoints de catalogos.";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Vimasistem",
                        Email = "gerencia@vimasistem.com",
                        Url = "https://www.vimasistem.com/"
                    };
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IHostApplicationLifetime applicationLifetime, IConsulClient consulClient)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            //Consul
            var serviceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() => { consulClient.Agent.ServiceDeregister(serviceId); });

            //Seq logs
            app.UseLogSeq();
        }
    }
}