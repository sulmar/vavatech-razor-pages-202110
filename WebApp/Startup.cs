using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vavatech.RazorPages.FakeRepositories;
using Vavatech.RazorPages.InMemoryRepositories;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;
using WebApp.Pages.Customers;

namespace WebApp
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
            services.AddScoped<ICustomerRepository, FakeCustomerRepository>();
            services.AddScoped<Faker<Customer>, CustomerFaker>();

            services.AddScoped<IProductRepository, FakeProductRepository>();
            services.AddScoped<Faker<Product>, ProductFaker>();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
