using Bogus;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
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
using Vavatech.RazorPages.Models.Validators;
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
            services.AddSingleton<ICustomerRepository, FakeCustomerRepository>();
            services.AddSingleton<Faker<Customer>, CustomerFaker>();
            services.AddSingleton<Faker<Address>, AddressFaker>();
            services.AddSingleton<ICityRepository, FakeCityRepository>();
            services.AddSingleton<ICustomerGroupRepository, FakeCustomerGroupRepository>();

            services.AddSingleton<IProductRepository, FakeProductRepository>();
            services.AddSingleton<Faker<Product>, ProductFaker>();
            services.AddSingleton<ITagRepository, FakeTagRepository>();

            services.AddTransient<IValidator<Product>, ProductValidator>();

            // Install-Package FluentValidation.AspNetCore
            services.AddRazorPages()
                .AddFluentValidation();

            //services.AddRazorPages(options =>
            //{
            //    options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
            //});
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
