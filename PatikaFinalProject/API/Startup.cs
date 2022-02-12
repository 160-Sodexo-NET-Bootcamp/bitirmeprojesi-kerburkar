using Data.Abstract;
using Data.Concrete.EntityFramework;
using Data.Concrete.EntityFramework.Contexts;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.Abstract;
using Services.Concrete;
using Services.Helpers;
using Services.Utilities.Jwt;
using Services.Utilities.Mapping.AutoMapper;
using Services.Utilities.Services;
using Services.Utilities.Services.Models;
using Services.Utilities.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
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
            //json loop'unu iptal etmek için kullanıldı.
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                )
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<RegisterValidator>();
                }
                );


            services.AddControllers();
            //appsettings.json'daki ConnectionStrings'da "DefaultConnection" ismindeki bağlantı adresi değişkene atandı. 
            var dbConnection = Configuration.GetConnectionString("DefaultConnection");

            //SqlServer kullanmak için gerekli kod eklendi. ApplicationDbContext'deki Constractordaki options parametresi.
            services.AddDbContext<ApplicationDbContext>(options =>

                    options.UseSqlServer(dbConnection).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)

            );
            //Proje derlendiğinde UnitOfWork çalışması için eklendi.
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITokenHelper, JwtHelper>();
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            //Token için Validate Kurallarý
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
                    };
                });
            //mail servisi için
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, MailService>();

            //AutoMapper için
            services.AddAutoMapper(typeof(CustomProfile));

            //hangfire için
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration["ConnectionStrings:DefaultHangfireConnection"]));
            services.AddHangfireServer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())

            {
                DataSeed.SeedAsync(app);
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            //hangfire
            app.UseHangfireDashboard();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
