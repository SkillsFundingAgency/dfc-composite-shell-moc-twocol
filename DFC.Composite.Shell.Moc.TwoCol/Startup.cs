﻿using DFC.Composite.Shell.Moc.TwoCol.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DFC.Composite.Shell.Moc.TwoCol
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
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.IsEssential = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddTransient<ICourseService, CourseService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                // add the courses routing
                routes.MapRoute(
                    name: $"Course-Index-Category",
                    template: "Course/Category/{category}",
                    defaults: new { controller = "Course", action = "Index" }
                );
                routes.MapRoute(
                    name: $"Course-Index-Filter",
                    template: "Course/Filter/{filter}",
                    defaults: new { controller = "Course", action = "Index" }
                );
                routes.MapRoute(
                    name: $"Course-Index",
                    template: "Course/Index",
                    defaults: new { controller = "Course", action = "Index" }
                );
                routes.MapRoute(
                    name: $"Course-Search",
                    template: "Course/Search/{searchClue?}",
                    defaults: new { controller = "Course", action = "Search" }
                );
                routes.MapRoute(
                    name: $"Course-Index-Search",
                    template: "Course/{searchClue?}",
                    defaults: new { controller = "Course", action = "Index" }
                );

                // add the site map route
                routes.MapRoute(
                    name: "Sitemap",
                    template: "Sitemap.xml",
                    defaults: new { controller = "Sitemap", action = "Sitemap" }
                );

                // add the robots.txt route
                routes.MapRoute(
                    name: "Robots",
                    template: "Robots.txt",
                    defaults: new { controller = "Robot", action = "Robot" }
                );

                // add the default route
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
}
