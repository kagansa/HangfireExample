using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HangfireHotelExampleAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Hangfire için Kullanacaðýmýz veritabanýný tanýmlýyoruz.
            services.AddHangfire(x => x.UseSqlServerStorage("Server=DESKTOP-TUCAPO7\\SQLEXPRESS;Database=HangfireHotelDB;User Id=sa;Password=Paf*13;"));

            //Hangfire servisimizi tanýmlýyoruz.
            services.AddHangfireServer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            //Hangfire yönetim panelimizi tanýmlýyoruz
            app.UseHangfireDashboard();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}