using DevIO.UI.Site.Data;
using DevIO.UI.Site.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DevIO.UI.Site
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear(); //Limpa Convencao

                //Nova Convencao por conta das Areas
                options.AreaViewLocationFormats.Add(item: "/Modulos/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add(item: "/Modulos/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add(item: "/Views/Shared/{0}.cshtml");


            });

            services.AddDbContext<MeuDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("MeuDbContext")));

            //Configuração de Injeção de dependencia
            services.AddTransient<IPedidoRepository, PedidoRepository>(); //Na duvida pra quando nao saber sar qual tipo de injecao de dependencia usar
            //services.AddScoped<IPedidoRepository, PedidoRepository>(); //Na Web com ASP NET CORE MVC
            //services.AddSingleton<IPedidoRepository, PedidoRepository>(); //Situações mais raras, principalmente para aplicações que nao guardam estado
            //services.AddSingleton<IPedidoRepository>(new Pedido { Id = Guid.Empty()}); //Situações mais raras, principalmente para aplicações que nao guardam estado
            
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();

            app.UseStaticFiles(); //Arquivos estaticos como bootstrap, js, etc, estao sendo habilitados

            app.UseEndpoints(routes =>
            {
                //routes.MapControllerRoute(
                //    name: "areas",
                //    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapAreaControllerRoute("AreaProdutos", "Produtos", "Produtos/{controller=Cadastro}/{action=Index}/{id?}");
                routes.MapAreaControllerRoute("AreaVendas", "Vendas", "Vendas/{controller=Pedidos}/{action=Index}/{id?}");

                routes.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


                //O mesmo que:
                //routes.MapControllerRoute("default","{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
