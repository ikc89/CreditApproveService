namespace KocFinansCC.Api
{
    using Common.Config;
    using Common.Extensions;
    using Data.Config;
    using Data.Context;
    using Data.Repositories;
    using Data.Repositories.Abstract;
    using Helpers;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Abstract;
    using Swashbuckle.AspNetCore.Swagger;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });
            services.AddSwaggerGen(c =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(description.GroupName, CreateInfoForApiVersion("KoçFinans CC {0}", description));
                }

                c.OperationFilter<SwaggerDefaultValues>();
            });
            services.AddApiVersioning(o => 
            {
                o.ApiVersionReader = new UrlSegmentApiVersionReader();
                o.ReportApiVersions = true;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var serviceSettings = Configuration.GetSection("Services").Get<ServiceSettings>();
            services.AddCreditScoreService(options =>
            {
                options.CreditScoreServiceUrl = serviceSettings.CreditScoreServiceUrl;
            });
            services.AddSMSService(options =>
            {
                options.SMSSenderServiceUrl = serviceSettings.SMSSenderServiceUrl;
            });

            var mongoDBSettings = Configuration.GetSection("MongoDB").Get<MongoDBSettings>();
            var creditApproveContext = new CreditApproveContext(mongoDBSettings);
            var creditApproveRepository = new CreditApproveRepository(creditApproveContext);
            services.AddSingleton<ICreditApproveRepository>(creditApproveRepository);

            services.AddSingleton<ICreditApproveService, CreditApproveService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
            app.UseMvc();
        }

        private Info CreateInfoForApiVersion(string title, ApiVersionDescription description)
        {
            var info = new Info
            {
                Title = string.Format(title, description.ApiVersion),
                Version = description.ApiVersion.ToString()
            };

            if (description.IsDeprecated)
            {
                info.Description += " This api version has been deprecated.";
            }

            return info;
        }
    }
}
