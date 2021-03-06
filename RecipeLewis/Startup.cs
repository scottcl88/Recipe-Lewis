using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Radzen;
using RecipeLewis.Areas.Identity;
using RecipeLewis.Data;
using RecipeLewis.Services;
using System;

namespace RecipeLewis
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
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseLazyLoadingProxies()
                .UseSqlServer(
                    Configuration["DefaultConnection"]));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                 .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor(options => options.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(3));
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddScoped<RecipeService>();
            services.AddScoped<TagService>();
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddLogging(builder => builder
                .SetMinimumLevel(LogLevel.Trace)
            );
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = Configuration["GoogleClientId"];
                    options.ClientSecret = Configuration["GoogleClientSecret"];
                });
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);
            services.AddScoped<EmailSender>();
            services.AddResponseCaching();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.Use(async (ctx, next) =>
            {
                //  <!-- Got hashes for src tags in html from https://www.srihash.org/ -->
                // Scanning website: https://observatory.mozilla.org/analyze/www.recipelewis.com#third-party
                ctx.Response.Headers.Add("Content-Security-Policy",
                                         @"base-uri 'self';block-all-mixed-content; default-src 'self'; frame-ancestors 'none'; form-action 'self'; style-src 'self' 'unsafe-hashes' 'sha256-7W8xHu0tqHDkDW2kEP4KSnp2wgzuUFTfA6muyo1zgQE=' 'sha256-0RLfJ5/cKZctV8KWy8KO8XjuA2RbRgEA29A4w0kWsmU=' https://unpkg.com; script-src 'self' https://ajax.aspnetcdn.com https://code.jquery.com https://unpkg.com; report-uri https://recipelewis.report-uri.com/r/d/csp/reportOnly;");
                ctx.Response.Headers.Add("Report-To",
                                         "{\"group\":\"default\",\"max_age\":31536000,\"endpoints\":[{\"url\":\"https://recipelewis.report-uri.com/a/d/g\"}],\"include_subdomains\":true}");
                ctx.Response.Headers.Add("Expect-CT", "max-age=604800, report-uri=\"https://recipelewis.report-uri.com/r/d/ct/enforce\"");
                ctx.Response.Headers.Add("Permissions-Policy", "none");
                ctx.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
                ctx.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                ctx.Response.Headers.Add("X-Frame-Options", "deny");
                await next();
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseResponseCaching();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}