using Csp.EF.Extensions;
using Csp.Jwt.Extensions;
using Csp.Web;
using Csp.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mt.Ask.Api.Infrastructure;

namespace Mt.Ask.Api
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
            services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());
            });

            services.AddHttpContextAccessor();
            //services.AddConsul(Configuration);
            services.AddJwt(Configuration);
            services.AddEF<AskDbContext>(Configuration);
            //services.AddAskServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
                app.UseExceptionHandler(err => err.Run(async context => await context.ExceptionResponse()));

            app.UseStatusCodePages(err => err.Run(async context => await context.StatusCodeResponse()));

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Ok");
                });

                endpoints.MapControllers();
            });
        }
    }
}
