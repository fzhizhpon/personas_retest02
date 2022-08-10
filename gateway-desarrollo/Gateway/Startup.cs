using System.Text;
using System.Threading;
using CoopCrea.Cross.Tracing.Src;
using Gateway.Api.utilis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string clientPolicy = "_clientPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ThreadPool.SetMinThreads(50, 50);

            /*Start - Cors*/
            services.AddCors(o => o.AddPolicy(clientPolicy, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();

            }));
            services.AddRouting(r => r.SuppressCheckForUnhandledSecurityMetadata = true);
            /*End - Cors*/

            services.AddOcelot();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sVq8Yd@EQ&$^o<,QOH<h:FtfQ+2Ap%7ZC4lDK?!$cn:(#KN,;s6VyK}w}KC4tH6*"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

            /*Start - Tracer distributed*/
            services.AddJaeger();
            services.AddOpenTracing();
            /*End - Tracer distributed*/
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseMiddleware<RequestInterceptor>();

            /*Start - Cors*/
            app.UseCors(clientPolicy);
            app.Use((context, next) =>
            {
                context.Items["__CorsMiddlewareInvoked"] = true;
                return next();
            });
            /*End - Cors*/

            app.UseOcelot().Wait();
        }
    }
}
