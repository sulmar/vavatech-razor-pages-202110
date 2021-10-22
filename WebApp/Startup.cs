using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
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

            services.AddTransient<IMessageService, FakeMessageService>();

            // Install-Package FluentValidation.AspNetCore
            services.AddRazorPages()
                .AddFluentValidation();

            //services.AddRazorPages(options =>
            //{
            //    options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
            //});


            // Install-Package AspNetCoreHero.ToastNotification
            services.AddNotyf(options =>
            {
                options.DurationInSeconds = 5;
                options.IsDismissable = true;
                options.Position = NotyfPosition.BottomRight;
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            //    options.Cookie.Name = "MySessionCoockie";
            });

            /// services.AddMemoryCache();
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
                options.InstanceName = "customers";
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            app.UseNotyf();

            app.UseRouting();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
