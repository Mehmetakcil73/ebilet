using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using eBilet.Data;
using eBilet.Data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using eBilet.Data.Cart;

namespace eBilet
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
            var supportedCultures = new[]
            {
                new CultureInfo("tr-TR"),    // Türkçe (Türkiye)
                new CultureInfo("en-US"),    // Ýngilizce (ABD) – isteðe baðlý
            };
            services.Configure<RequestLocalizationOptions>(options =>
            {
                // Kullanýlamazsa varsayýlan olarak tr-TR kullan
                options.DefaultRequestCulture = new RequestCulture("tr-TR", "tr-TR");      // :contentReference[oaicite:0]{index=0}
                options.SupportedCultures = supportedCultures;                             // sayý, tarih formatý için
                options.SupportedUICultures = supportedCultures;                           // UI çevirileri için
            });

            //DbContext Configuration
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnectionString")));
            services.AddControllersWithViews();

            //Services Configuration
            services.AddScoped<IActorsService, ActorsService>();
            services.AddScoped<IProducersService, ProducersServices>();
            services.AddScoped<IMoviesService, MoviesService>();
            services.AddScoped<ICinemasService, CinemasService>();
			
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

            services.AddSession();

            services.AddControllersWithViews();
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            
            //Seed database
            AppDbInitializer.Seed(app);
        }
    }
}
