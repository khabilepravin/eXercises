using eXercise.Diagnostics;
using eXercise.ServiceImplementations;
using eXercise.ServiceInterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceImplementations;
using ServiceImplementations.Repositories;

namespace eXercise
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
            services.Configure<ExternalServiceSettings>(Configuration.GetSection("ExternalServiceSettings"));
            services.AddHealthChecks();
            
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddControllers();

            services.AddSwaggerGen();

            // Singleton could be an enhancement
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IShopperHistoryService, ShopperHistoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITrolleyService, TrolleyService>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "eXcercise API");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
