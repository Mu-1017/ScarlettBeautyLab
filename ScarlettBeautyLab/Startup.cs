using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScarlettBeautyLab.Filters;
using ScarlettBeautyLab.Infrastructure;
using ScarlettBeautyLab.Models;
using ScarlettBeautyLab.Services;
using AspNet.Security.OpenIdConnect.Primitives;
using OpenIddict.Validation;
using System;

namespace ScarlettBeautyLab
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
            services.AddScoped<IUserService, DefaultUserService>();

            services.AddDbContext<BeautyLabDbContext>(
                options =>
                {
                    options.UseInMemoryDatabase("beautyLabDb");
                    options.UseOpenIddict<Guid>();
                });

            AddOpenIddictService(services);

            // Add ASP.NET Core Identity
            AddIdentityCoreServices(services);

            services.AddAutoMapper(
                options => options.AddProfile<MappingProfile>(), typeof(Startup));

            services
                .AddMvc(options =>
                {
                    options.Filters.Add<JsonExceptionFilter>();
                    options.Filters.Add<RequireHttpsOrCloseAttribute>();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseMvc();
        }

        private static void AddOpenIddictService(IServiceCollection services)
        {
            services.AddOpenIddict()
                            .AddCore(options =>
                            {
                                options.UseEntityFrameworkCore()
                                .UseDbContext<BeautyLabDbContext>()
                                .ReplaceDefaultEntities<Guid>();
                            })
                            .AddServer(options =>
                            {
                                options.UseMvc();
                                options.EnableTokenEndpoint("/api/token");
                                options.AllowPasswordFlow();
                                options.AcceptAnonymousClients();
                            })
                            .AddValidation();

            // ASP.NET Core Identity should use the same claim names as OpenIddict
            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = OpenIddictValidationDefaults.AuthenticationScheme;
            });
        }

        private static void AddIdentityCoreServices(IServiceCollection services)
        {
            var builder = services.AddIdentityCore<UserEntity>();
            builder = new IdentityBuilder(builder.UserType,
                                        typeof(UserRoleEntity),
                                        builder.Services);

            builder.AddRoles<UserRoleEntity>()
                .AddEntityFrameworkStores<BeautyLabDbContext>()
                .AddDefaultTokenProviders()
                .AddSignInManager<SignInManager<UserEntity>>();
        }
    }
}
