
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using mobileAPI.BussinessLogic;
using mobileAPI.Config;

namespace mobileAPI
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
            services.AddControllers().AddJsonOptions(options=> {
                options.JsonSerializerOptions.Converters.Add(new JSONValuFormatter<System.DateTime>());
                options.JsonSerializerOptions.Converters.Add(new JSONValuFormatter<System.Int32>());
            });
            services.AddCors(options=> 
            {
                options.AddPolicy("AppCORSPloicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });
            services.Configure<ApplicationConfig>(Configuration.GetSection("ApplicationConfig"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AppCORSPloicy");
            app.UseRouting();
            app.UseStaticFiles();
            //app.UseDirectoryBrowser();
           // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
