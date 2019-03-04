using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Data;
using SportStore.Repo;
using SportStore.Repo.Abstract;
using SportStore.Services;
using SportStore.Services.Abstract;

namespace SportStore.Web
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDbService(services);
            ConfigureServicesRegistration(services);
            ConfigureMVCServices(services);
        }

        private void ConfigureDbService(IServiceCollection services)
        {
            services.AddDbContext<SportStoreDbContext>(options =>
            {
                options
                    .UseSqlServer(
                        this.configuration.GetConnectionString("DefaultConnection")
                        );
            });
        }

        private void ConfigureServicesRegistration(IServiceCollection services)
        {
            services.AddTransient<IProductRepository, FakeProductsRepository>();
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddTransient<ICardRepository, EFCardRepository>();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICardService, CardService>();
        }

        private void ConfigureMVCServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseStaticFiles();

            ConfigureMvcRoutes(app);
            DataSeeding(app);
        }

        private void DataSeeding(IApplicationBuilder app)
        {
            SeedData.EnsurePopulated(app);
        }

        private void ConfigureMvcRoutes(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List" }
                    );

                routes.MapRoute(
                    name: null,
                    template: "Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List", productPage = 1 }
                    );

                routes.MapRoute(
                    name: null, template: "{category}",
                    defaults: new { controller = "Product", action = "List", productPage = 1 }
                    );

                routes.MapRoute(
                    name: null, template: "",
                    defaults: new { controller = "Product", action = "List", productPage = 1 }
                    );

                routes.MapRoute(
                    name: null,
                    template: "{controller}/{action}/{id?}"
                    );
            });
        }
    }
}