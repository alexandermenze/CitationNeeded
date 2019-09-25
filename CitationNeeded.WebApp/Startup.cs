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

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var connectionStringKey = $"{nameof(AppSettings)}:{nameof(AppSettings.DbConnectionString)}";

            services.AddDbContext<CitationContext>(
                o => o.UseMySql(Configuration[connectionStringKey], 
                mo => mo.MigrationsAssembly("CitationNeeded.Database")));

            services.AddDbContext<AccountContext>(
                o => o.UseMySql(Configuration[connectionStringKey],
                mo => mo.MigrationsAssembly("CitationNeeded.Database")));

            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IEmailService, SendGridEmailService>();
            services.AddTransient<ICredentialVerifier, DatabaseCredentialVerifier>();
            services.AddTransient<IHashService, BcryptHashService>();
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
        }

        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env,
            AccountContext accountContext,
            CitationContext citationContext)
        {
            UpdateDatabase(accountContext, citationContext);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
    }
}
