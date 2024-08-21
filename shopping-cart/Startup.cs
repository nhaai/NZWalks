using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SA51_CA_Project_Team10.DBs;
using SA51_CA_Project_Team10.Mappings;
using SA51_CA_Project_Team10.Models;
using SA51_CA_Project_Team10.Repositories;

namespace SA51_CA_Project_Team10
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
            services.AddControllersWithViews();

            services.AddDbContext<NZWalksDbContext>(opt =>
                opt.UseLazyLoadingProxies().UseSqlServer(
                    Configuration.GetConnectionString("NZWalksConnectionString")
                    ));
            services.AddDbContext<NZWalksAuthDbContext>(opt =>
      opt.UseLazyLoadingProxies().UseSqlServer(
          Configuration.GetConnectionString("NZWalksAuthConnectionString")
          ));
            services.AddSingleton<Hasher>();
            services.AddScoped<ICartRepository, SQLCartRepository>();
            services.AddScoped<ICategoryRepository, SQLCategoryRepository>();
            services.AddScoped<IOrderItemRepository, SQLOrderItemRepository>();
            services.AddScoped<IOrderRepository, SQLOrderRepository>();
            services.AddScoped<IProductRepository, SQLProductRepository>();
            services.AddScoped<ITokenRepository, SQLTokenRepository>();
            services.AddScoped<IUserRepository, SQLUserRepository>();
            services.AddScoped<IWalkRepository, SQLWalkRepository>();
            services.AddSingleton<Verify>();
            services.AddAutoMapper(typeof(AutoMapperProfiles));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, NZWalksDbContext db, NZWalksAuthDbContext dbc)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Gallery}/{action=Index}/{id?}");
            });

            // Comment next line away if you don't want to restart the database every time
            //db.Database.EnsureDeleted(); 

            //db.Database.EnsureCreated();

            //dbc.Database.EnsureDeleted();

            //dbc.Database.EnsureCreated();
        }
    }
}
