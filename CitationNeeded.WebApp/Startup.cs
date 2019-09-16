using CitationNeeded.Database.Database;
using CitationNeeded.Database.Services;
using CitationNeeded.Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CitationNeeded.WebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<CitationContext>(
                o => o.UseMySql(Configuration["AppSettings:ConnectionString"], 
                mo => mo.MigrationsAssembly("CitationNeeded.Database")));

            services.AddDbContext<AccountContext>(
                o => o.UseMySql(Configuration["AppSettings:ConnectionString"],
                mo => mo.MigrationsAssembly("CitationNeeded.Database")));

            services.AddTransient<ICredentialVerifier, DatabaseCredentialVerifier>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseMvc();
        }
    }
}
