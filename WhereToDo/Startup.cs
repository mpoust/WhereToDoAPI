////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: Startup.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Entry point for the application.
 * References: Structure of this project was created using guidance provided from the lynda.com class
 *   "Building and Securing RESTful APIs in ASP.NET Core" by Nate Barbettini.
 *   Other references are cited within the files they are used. 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


using System;
using System.Linq;
using AspNet.Security.OpenIdConnect.Primitives;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Validation;
using WhereToDo.Entities;
using WhereToDo.Filters;
using WhereToDo.Infrastructure;
using WhereToDo.Models;
using WhereToDo.Services;

namespace WhereToDo
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
            // Connect to Database
            services.AddDbContext<WTD_DBContext>(opt =>
            {
                opt.UseSqlServer(
                    Configuration.GetConnectionString("DatabaseConnection"));
                opt.UseOpenIddict<int>();
            });

            // Configure Open ID Connect
            services.AddOpenIddict()
                .AddCore(opt =>
                {
                    opt.UseEntityFrameworkCore()
                    .UseDbContext<WTD_DBContext>()
                    .ReplaceDefaultEntities<Guid>();
                })
                .AddServer(opt =>
                {
                    opt.UseMvc();
                    opt.EnableTokenEndpoint("/token");
                    opt.AllowPasswordFlow();
                    opt.AcceptAnonymousClients();
                    opt.SetAccessTokenLifetime(TimeSpan.FromDays(180));
                })
                .AddValidation();

            // ASP.NET Core Identity should use the same claim names as OpenIddict
            services.Configure<IdentityOptions>(opt =>
            {
                opt.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                opt.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                opt.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            // Add Authentication and set some defaults
            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = OpenIddictValidationDefaults.AuthenticationScheme;
            });

            // Add ASP.NET Core Identity  
            AddIdentityCoreServices(services);

            // Authorization Policies
            services.AddAuthorization(opt =>
            {
                // Can use these policies if we wish - leaving examples in from my senior project

                opt.AddPolicy("ViewAllUsersPolicy",
                    p => p.RequireAuthenticatedUser().RequireRole("Administrator")); // Users will need to be specially put into this role.

                // All registered users are put into the 'Employee' role
            });

            // Auto Mapping of DB Context Entities to Models
            services.AddAutoMapper();

            services.AddRouting(opt => opt.LowercaseUrls = true);

            services.AddResponseCaching(); 

            services.AddApiVersioning(opt =>
            {
                opt.ApiVersionReader = new MediaTypeApiVersionReader();
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionSelector = new CurrentImplementationApiVersionSelector(opt); // will use highest version of route if none is requested
            });

            // TODO: Configure CORS Properly
            services.AddCors(opt =>
            {
                // proper policy here

                // DEVELOPMENT
                opt.AddPolicy("AllowAny",
                    policy => policy.AllowAnyOrigin());
            });

            // Add Framework Services
            services.AddMvc(opt =>
            {
                // Use created exception filter to serialize unhandled exceptions as JSON objects
                opt.Filters.Add(typeof(JsonExceptionFilter));
                // Use created result filter to rewrite links before they are sent as a response
                opt.Filters.Add(typeof(LinkRewritingFilter));

                // Update the media type ot application/ion+json
                var jsonFormatter = opt.OutputFormatters.OfType<JsonOutputFormatter>().Single(); // current formatter
                opt.OutputFormatters.Remove(jsonFormatter);
                opt.OutputFormatters.Add(new IonOutputFormatter(jsonFormatter)); // add new formatter supplied with removed

                // Add cache profile
                opt.CacheProfiles.Add("Static", new CacheProfile
                {
                    Duration = 86400
                });

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Connect static information in appsettings.json file
            services.Configure<ApplicationInfo>(Configuration.GetSection("Info"));
            services.Configure<PagingOptions>(Configuration.GetSection("DefaultPagingOptions"));

            // Adding Service Interfaces so Default service is selected
            services.AddScoped<IUserService, DefaultUserService>();
            services.AddScoped<IListService, DefaultListService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseHttpsRedirection();

            // TODO: Configure Cors for Deployment
            app.UseCors("AllowAny");

            app.UseAuthentication();

            app.UseResponseCaching();

            app.UseMvc();
        }

        // Connect IdentityCore Services with custom User classes and DB Context.
        private static void AddIdentityCoreServices(IServiceCollection services)
        {
            var builder = services.AddIdentityCore<UserEntity>();
            builder = new IdentityBuilder(
                builder.UserType,
                typeof(UserRoleEntity),
                builder.Services);

            builder.AddRoles<UserRoleEntity>()
                .AddEntityFrameworkStores<WTD_DBContext>()
                .AddDefaultTokenProviders()
                .AddSignInManager<SignInManager<UserEntity>>();
        }
    }
}
