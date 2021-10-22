using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Bogus;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vavatech.RazorPages.DbEfRepositories;
using Vavatech.RazorPages.FakeRepositories;
using Vavatech.RazorPages.InMemoryRepositories;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;
using Vavatech.RazorPages.Models.Validators;
using WebApp.Hubs;
using WebApp.Middlewares;
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
            //services.AddSingleton<ICustomerRepository, FakeCustomerRepository>();
            //services.AddSingleton<Faker<Customer>, CustomerFaker>();

            services.AddScoped<ICustomerRepository, DbCustomerRepository>();

            services.AddSingleton<Faker<Address>, AddressFaker>();
            services.AddSingleton<ICityRepository, FakeCityRepository>();

            // services.AddSingleton<ICustomerGroupRepository, FakeCustomerGroupRepository>();
            services.AddScoped<ICustomerGroupRepository, DbCustomerGroupsRepository>();

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


            // REDIS
            // Uruchomienie REDIS w kontenerze Dockera
            // cmd> docker run --name shop-redis -d -p 6379:6379 redis

            //services.AddDistributedRedisCache(options =>
            //{
            //    options.Configuration = "localhost:6379";
            //    options.InstanceName = "customers";
            //});


            // Install-Package Microsoft.EntityFrameworkCore.SqlServer

            string connectionString = Configuration.GetConnectionString("ShopConnectionString");
            services.AddDbContext<ShopContext>(options => options.UseSqlServer(connectionString));


            services.AddSignalR();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ShopContext context)
        {
            // context.Database.EnsureCreated();

            // Install-Package Microsoft.EntityFramework.Tools

            // PMC> Add-Migration Init
           context.Database.Migrate();

            app.UseStaticFiles();

            app.UseNotyf();

            app.UseRouting();

            app.UseSession();

            // app.UseMiddleware<LoggerMiddleware>();

            app.UseLogger();

            //app.Use(async (context, next) =>
            //{
            //    // request
            //    string url = context.Request.Path;                

            //    //if (istnieje_w_cache)
            //    //{ 
            //    //    // load body from cache

            //    //    // sent do client
            //    //    // context.Response.BodyWriter
            //    //}
            //    //else
            //    //{

            //    //}

            //    // await next();

            //    // response

            //    // Save to cache: context.Response.Body

            //    context.Response.StatusCode = (int) StatusCodes.Status400BadRequest;

              

            //  //  string bodyContent = new StreamReader(context.Response.Body).ReadToEnd();


            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                endpoints.MapHub<CustomersHub>("/signalr/customers");
            });
        }
    }
}
