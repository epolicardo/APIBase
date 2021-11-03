using Configuration;
using Controllers;
using Data;
using Infrastructure.Swagger;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Text;

namespace Gastos.API
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
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("Conexion_Hangfire")));
            services.AddHangfireServer();
            services.AddCors();
            services.AddControllers();

            services.AddHttpClient("Gastos.API", c =>
            {
                c.BaseAddress = new Uri("http://localhost:44365/api/");
            });

            IConfiguration config = new ConfigurationBuilder()
                    .AddUserSecrets("7a219868-0c3b-4d8a-96e7-d2c5bef3bd16") //Nombre de la carpeta que hemos creado
                        .Build();

            //// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-5.0#named-clients
            //services.AddHttpClient("github", c =>
            //{
            //    c.BaseAddress = new Uri("https://api.github.com/");
            //    // Github API versioning
            //    c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            //    // Github requires a user-agent
            //    c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            //});

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options =>
            {
                // add a custom operation filter which sets default values
                options.OperationFilter<SwaggerDefaultValues>();
            });


            //https://dev.to/moesmp/what-every-asp-net-core-web-api-project-needs-part-2-api-versioning-and-swagger-3nfm
            services.AddApiVersioning(options =>
            {
                // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });









            services.AddDbContext<DataContext>(
              options => options.UseSqlServer(Configuration.GetConnectionString("Conexion")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IDemoService, DemoService>();
            services.AddScoped<IConfigurationHelper, ConfigurationHelper>();


            //  services.AddApiVersioning(o => o.ApiVersionReader = new UrlSegmentApiVersionReader());

            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<DataContext>();

            // configure strongly typed settings objects
            var jwtSection = Configuration.GetSection("JwtBearerTokenSettings");
            services.Configure<JwtBearerTokenSettings>(jwtSection);
            var jwtBearerTokenSettings = jwtSection.Get<JwtBearerTokenSettings>();
            var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = jwtBearerTokenSettings.Issuer,
                    ValidAudience = jwtBearerTokenSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(options => { options.RouteTemplate = "swagger/{documentName}/docs.json"; });
                app.UseSwaggerUI(options =>
                {
                    options.RoutePrefix = "swagger";
                    foreach (var description in provider.ApiVersionDescriptions)
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/docs.json", description.GroupName.ToUpperInvariant());
                });
            }
            app.UseHangfireDashboard();
            //   BackgroundJob.Enqueue(() => Console.WriteLine("Tarea generada en Startup"));


            app.UseHttpsRedirection();
            // app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
