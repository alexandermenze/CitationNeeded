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

                services.AddDbContext<AccountContext>(
                    o => o.UseSqlite("Filename=Accounts.db"));

                services.AddSingleton<IEmailService, ConsoleEmailService>();
            }
            else
            {
                services.AddDbContext<CitationContext>(
                    o => o.UseMySql(Configuration[connectionStringKey],
                    mo => mo.MigrationsAssembly("CitationNeeded.Database")));

                services.AddDbContext<AccountContext>(
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
            AccountContext accountContext,
            CitationContext citationContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SetupDev(accountContext, citationContext);
            }
            else
            {
                UpdateDatabase(accountContext, citationContext);
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();
        }

        private void UpdateDatabase(AccountContext accountContext, CitationContext citationContext)
        {
            accountContext.Database.Migrate();
            citationContext.Database.Migrate();
        }

        private void SetupDev(AccountContext accountContext, CitationContext citationContext)
        {
            accountContext.Database.EnsureCreated();
            citationContext.Database.EnsureCreated();
        }
    }
}
