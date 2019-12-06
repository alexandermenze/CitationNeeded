using CitationNeeded.Cryptography.Hash;
using CitationNeeded.Database.Database;
using CitationNeeded.Database.Services;
using CitationNeeded.Domain.Interfaces;
using CitationNeeded.Domain.ValueTypes;
using CitationNeeded.Infrastructure.Mail;
using CitationNeeded.WebApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CitationNeeded.WebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }
        public IHostingEnvironment HostingEnvironment { get; private set; }

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var connectionStringKey = $"{nameof(AppSettings)}:{nameof(AppSettings.DbConnectionString)}";

            if (HostingEnvironment.IsDevelopment())
            {
                services.AddDbContext<CitationContext>(
                    o => o.UseSqlite("Filename=Citations.db"));

                services.AddSingleton<IEmailService, ConsoleEmailService>();
            }
            else
            {
                services.AddDbContext<CitationContext>(
                    o => o.UseMySql(Configuration[connectionStringKey],
                    mo => mo.MigrationsAssembly("CitationNeeded.Database")));

                services.AddSingleton<IEmailService, SendGridEmailService>();
            }

            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICredentialVerifier, DatabaseCredentialVerifier>();
            services.AddTransient<IHashService, BcryptHashService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IColorService, StringRandomColorService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
        }

        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env,
            CitationContext citationContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SetupDev(citationContext);
            }
            else
            {
                UpdateDatabase(citationContext);
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();
        }

        private void UpdateDatabase(CitationContext citationContext)
        {
            citationContext.Database.Migrate();
        }

        private void SetupDev(CitationContext citationContext)
        {
            if (citationContext.Database.EnsureCreated())
            {
                TestData.Setup(citationContext);
            }
        }
    }
}
