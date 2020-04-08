using Csp.Jwt;
using Csp.Jwt.Extensions;
using Csp.Web;
using Csp.Web.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mt.Ask.Web.Services;
using Mt.Fruit.Web.Models;
using Mt.Fruit.Web.Services;
using System;

namespace Mt.Fruit.Web
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
            services.Configure<RazorViewEngineOptions>(o => {
                o.ViewLocationExpanders.Add(new ViewLocationExpander());
            });

            services.Configure<AppSettings>(Configuration);

            services.AddControllersWithViews().AddJsonOptions(options => {
                options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());
            });

            services.AddMvcJwt(Configuration);

            services.AddHttpContextAccessor();
            services.AddHttpClientServices();

            services.AddTransient<IIdentityParser<User>, IdentityParser>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
            //services.AddTransient<DevspacesMessageHandler>();
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            //services.AddTransient<HttpClientRequestIdDelegatingHandler>();

            services.AddHttpClient("extendedhandlerlifetime").SetHandlerLifetime(TimeSpan.FromMinutes(5));//.AddDevspacesSupport();

            services.AddHttpClient<ICategoryService, CategoryService>().AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
            services.AddHttpClient<IAuthService, AuthService>();

            services.AddHttpClient<IArticleService, ArticleService>().AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
            services.AddHttpClient<IResourceService, ResourceService>().AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();


            services.AddHttpClient<IOssService, OssService>().AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            return services;
        }
    }
}
