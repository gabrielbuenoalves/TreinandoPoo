using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//onde é instanciada todos os objetos
namespace web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Preciso declarar esse trem para que a injeção de dependencia funcione 

            var catalogo = new Catalogo();
            services.AddSingleton<ICatalogo>(catalogo);
            services.AddSingleton<IRelatorio>(new Relatorio(catalogo));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //ao inves de instanciar com new eu faço dessa forma para ocorrer a injeção de dependencia           
            ICatalogo catalogo = serviceProvider.GetService<ICatalogo>();
            IRelatorio relatorio = serviceProvider.GetService <IRelatorio>();
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await relatorio.imprimir(context);
                    
                });
            });
        }
    }
}
