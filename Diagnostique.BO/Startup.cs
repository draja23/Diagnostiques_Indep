using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diagnostique.BO.ModelDestination;
using Diagnostique.BO.ModelLogin;
using Diagnostique.BO.ModelSource;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Diagnostique.BO
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<ILoginDataAccessLayer, LoginDataAccessLayer>();
            services.AddTransient<ISourceDataAccessLayer, SourceDataAccessLayer>();
            services.AddTransient<IDestinationDataAcessLayer, DestinationDataAcessLayer>();

            //services.AddDbContext<DiagnoInfoTerrainContext>(options => options.UseSqlServer(Configuration.GetConnectionString("destinationConnection")));
            //services.AddDbContext<DiagnoInfoTerrainSourceContext>(options => options.UseSqlServer(Configuration.GetConnectionString("sourceConnection")));


            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseRewriter(new RewriteOptions().AddRedirectToHttps(StatusCodes.Status301MovedPermanently, 443));

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            loggerFactory.AddFile("Logs/DiagnoInfoTerrainApp-{Date}.txt");

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
