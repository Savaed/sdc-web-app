using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SDCWebApp.Auth;
using SDCWebApp.Helpers.Constants;
using SDCWebApp.Data;
using SDCWebApp.Helpers;
using System;
using System.Reflection;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using SDCWebApp.Services;

namespace SDCWebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Disable JWT claim name mapping.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            // Add validation of antiforgery tokens for unsave methods.
            // See https://docs.microsoft.com/pl-pl/dotnet/api/microsoft.aspnetcore.mvc.autovalidateantiforgerytokenattribute?view=aspnetcore-2.2
            services.AddMvc(options =>
            {
                //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddFluentValidation()
            .ConfigureApiBehaviorOptions(config =>
            {
                config.InvalidModelStateResponseFactory = context =>
                {
                    var validationError = new CustomValidationProblemDetails(context);
                    return new ObjectResult(validationError);
                };
            })
            .AddJsonOptions(options =>
            {
                // Ignore reference loops in JSON responses.
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                // Convert any enum to its string representation.
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.Converters.Add(new IsoDateTimeConverter());
            });

            // In production, the Angular files will be served from this directory.
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddCors(config =>
            {
                config.AddPolicy(Strings.DefaultCorsPolicyName, policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build();
                });
            });

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString(Strings.DefaultConnectionStringName)));

            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequireDigit = true;
                config.Password.RequiredLength = 8;
                config.Password.RequireLowercase = true;
                config.Password.RequireNonAlphanumeric = true;
                config.Password.RequireUppercase = true;

                config.Lockout.AllowedForNewUsers = true;
                config.Lockout.MaxFailedAccessAttempts = 3;

                config.User.RequireUniqueEmail = true;
                config.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz";

                config.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            // Config JWT settings and set properties value from app settings file.
            var jwtSettingsSection = Configuration.GetSection(nameof(JwtSettings));
            services.Configure<JwtSettings>(jwtSettingsSection);
            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

            // Add authentication
            services.AddAuthentication(config =>
            {
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(config =>
            {
                config.TokenValidationParameters = jwtSettings.GetValidationParameters();
                config.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception is SecurityTokenExpiredException)
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Strings.ApiUserPolicyName, config => config.RequireAuthenticatedUser().RequireClaim(Strings.RoleClaimName, Strings.AdministratorRoleName, Strings.ModeratorRoleName));
            });

            // Add AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly().GetTypes());

            // Replacement of built-in service container with Autofac.
            // TODO Wrap following code in private method for future reusability.
            var containerBuilder = new ContainerBuilder();

            // First register services/modules and then populate them.
            containerBuilder.RegisterModule<ApplicationModule>();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();

            //var test = container.ResolveKeyed<ServiceBase>(nameof(TicketDbService));

            //var test2 = container.Resolve<Controllers.IArticlesController>();

            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/errors/500");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            // Enabling CORS.
            app.UseCors();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
