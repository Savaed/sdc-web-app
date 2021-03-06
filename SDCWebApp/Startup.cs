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
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SDCWebApp.Auth;
using SDCWebApp.Data;
using SDCWebApp.Helpers;
using SDCWebApp.Helpers.Constants;
using System;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Threading.Tasks;

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
            // Disable JWT inbound claim name mapping. This behaviour by default maps JWT claim names to their much longer counterparts.
            // See https://mderriey.com/2019/06/23/where-are-my-jwt-claims/
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
                    // Add custom validation error response.
                    var validationError = new CustomValidationProblemDetails(context);
                    return new ObjectResult(validationError);
                };
            })
            .AddJsonOptions(options =>
            {
                // Ignore reference loops in JSON responses.
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                // Convert each enum to its string representation.
                options.SerializerSettings.Converters.Add(new StringEnumConverter());

                // Set DateTime format in JSON, eg. "2019-12-12T23:12:56Z"
                options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ssK";
            });

            // In production, the Angular files will be served from this directory.
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            // Add default CORS policy.
            services.AddCors(config =>
            {
                config.AddPolicy(ApiConstants.DefaultCorsPolicy, policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build();
                });
            });

            // Set DbContext for app.         
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(GetConnectionString(ApiConstants.DefaultConnectionString)));

            // Automatically perform database migration.
            //services.BuildServiceProvider().GetService<ApplicationDbContext>().Database.Migrate();

            // Set user requirements.
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

            // Get JWT settings.
            var jwtSettings = GetJwtSettings(services);

            // Add JWT Bearer authentication.
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
                        // Set the 'Token-Expired: true' header for each request that has provided an expired access token.
                        if (context.Exception is SecurityTokenExpiredException)
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            // Add authorization policies.
            services.AddAuthorization(options =>
            {
                options.AddPolicy(ApiConstants.ApiAdminPolicy, config => config.RequireAuthenticatedUser().RequireClaim(ApiConstants.RoleClaim, ApiConstants.AdministratorRole));
                options.AddPolicy(ApiConstants.ApiUserPolicy, config => config.RequireAuthenticatedUser().RequireClaim(ApiConstants.RoleClaim, ApiConstants.AdministratorRole, ApiConstants.ModeratorRole));
            });

            // Add AutoMapper.
            services.AddAutoMapper(Assembly.GetExecutingAssembly().GetTypes());

            // Replacement of built-in service container with Autofac.            
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<ApplicationModule>();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
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

            app.UseCors();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();

            // Seed roles and users for testing on development and staging environment.
            if (!env.IsProduction())
            {
                IdentityDataInitializer.SeedData(userManager, roleManager, Configuration);
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
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


        #region Privates

        private JwtSettings GetJwtSettings(IServiceCollection services)
        {
            var jwtSettingsSection = Configuration.GetSection(nameof(JwtSettings));
            services.Configure<JwtSettings>(jwtSettingsSection);
            return jwtSettingsSection.Get<JwtSettings>();
        }

        private string GetConnectionString(string connectionStringName)
        {
            // HACK: If environment is not Development then settings from Azure App Configuration service are used.
            // But for this to work, the names of the secrets and all local settings as well as those on the Azure MUST be the same.
            var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString(connectionStringName));
            builder.Password = Configuration[ApiConstants.DbPassword];
            builder.UserID = Configuration[ApiConstants.DbUserId];
            return builder.ConnectionString;
        }

        #endregion

    }
}
