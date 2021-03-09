using API.Extensions;
using API.Helpers;
using API.Middleware;
using AutoMapper;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            this._config = config;
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) // Mainly responsible for Dependency Injection 
        {
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            services.AddDbContext<StoreContext>(x => 
                x.UseSqlite(_config.GetConnectionString("DefaultConnection")));
            
            services.AddDbContext<AppIdentityDbContext>( x => 
            {
                x.UseSqlite(_config.GetConnectionString("IdentityConnection"));
            });

            services.AddSingleton<IConnectionMultiplexer>(c => {
                var configuration = ConfigurationOptions.Parse(_config.GetConnectionString("Redis"),
                 true);
                 return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddApplicationService(); // by using this class we extend IServiceCollection services
            services.AddIdentityServices(_config);
            services.AddSwaggerDocumentation(); // by using this class we extend IServiceCollection services for swagger

            services.AddCors( opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod()
                        .WithOrigins( "https://localhost:4200" );
                });
            }); //CORS configuration
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) // Body part of the program. Http request alır, işlemleri yapar ve buna uygun çıktı üretir
        {
            app.UseMiddleware<ExceptionMiddleware>(); // Our own middleware

            app.UseStatusCodePagesWithReExecute("/errors/{0}"); // api ımızda varolmayan bir endpoint request gelirse bunu handler eder

            app.UseHttpsRedirection(); // eğer dinlenen http portlarından birinden request gelirse, bu request i https e yönlendirir

            app.UseRouting();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy"); //CORS Middleware

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwaggerDocumentation(); // by using this class we extend IApplicationBuilder middleware for swagger

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
