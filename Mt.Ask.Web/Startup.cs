using Csp.Jwt;
using Csp.Jwt.Extensions;
using Csp.Web;
using Csp.Web.Mvc;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mt.Ask.Web.Models;
using Mt.Ask.Web.Services;
using System;

namespace Mt.Ask.Web
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
            
            services.AddMediatR(typeof(Startup).Assembly);

            services.AddControllersWithViews().AddJsonOptions(options => {
                options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());
            });

            services.AddMvcJwt(Configuration);

            services.AddHttpClientServices();

            services.AddHttpContextAccessor();
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

            services.AddHttpClient("WxApiClient");
            services.AddHttpClient("ArticleClient").AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient("extendedhandlerlifetime").SetHandlerLifetime(TimeSpan.FromMinutes(5));//.AddDevspacesSupport();

            services.AddHttpClient<IAnnounceService, AnnounceService>();
            services.AddHttpClient<IArticleService, ArticleService>().AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
            services.AddHttpClient<ICourseService, CourseService>();
            services.AddHttpClient<IAuthService, AuthService>();

            return services;
        }
    }
}
