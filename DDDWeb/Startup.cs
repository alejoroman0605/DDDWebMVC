using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.EFCore;
using DataAccess.EFCore.Repositories;
using DataAccess.EFCore.UnitOfWorks;
using Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DDDWeb {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddControllers ();

            // services.AddDbContext<ApplicationContext> (options =>
            //     options.UseSqlServer (Configuration.GetConnectionString ("DefaultConnection")));
            services.AddDbContext<ApplicationContext> (options =>
                options.UseSqlServer (
                    Configuration.GetConnectionString ("DefaultConnection"),
                    b => b.MigrationsAssembly ("DataAccess.EFCore")));

            #region Repositories
            services.AddTransient (typeof (IGenericRepository<>), typeof (GenericRepository<>));
            services.AddTransient<IDeveloperRepository, DeveloperRepository> ();
            services.AddTransient<IProjectRepository, ProjectRepository> ();
            #endregion
            services.AddTransient<IUnitOfWork, UnitOfWork> ();
            services.AddRazorPages ();
            services.AddControllersWithViews ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseHttpsRedirection ();

            app.UseStaticFiles ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllerRoute (
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages ();
            });
        }
    }
}