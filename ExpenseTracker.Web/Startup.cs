using ExpenseTracker.Common;
using ExpenseTracker.Domain.Adapters;
using ExpenseTracker.SQL;
using ExpenseTracker.SQL.Dao;
using ExpenseTracker.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Radzen;
using System;

namespace ExpenseTracker.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Logger.Log($"Web serice is started at {Environment.MachineName} ...");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new ExceptionsHandler());
            });

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddControllers();
            services.AddScoped<NotificationService>();
            services.AddDbContext<ExpenseTrackerContext>();
            services.AddCors();
            services.AddScoped<ICostDao, CostDao>();
            services.AddScoped<ICategoryDao, CategoryDao>();
            services.AddScoped<IDescriptionDao, DescriptionDao>();
            services.AddScoped<INameDao, NameDao>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.ConfigureExceptionHandler();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
