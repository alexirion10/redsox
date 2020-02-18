using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace redsox
{
    /// <summary>
    /// https://github.com/microsoft/aspnet-api-versioning/wiki/API-Documentation#aspnet-core
    /// </summary>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private const string AppTitle = "Alex making the RedSox";

        readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach(var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, createInfoForApiVersion(description));
            }

            // Set the comment path for the Swagger JSON and UI
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }

        private static OpenApiInfo createInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = AppTitle,
                Version = description.ApiVersion.ToString(),
                Description = "Beautiful web api - built with Swagger, Swashbuckle, and API versioning",
                Contact = new OpenApiContact() { Name = "Alex Irion", Email = "alexirion10@gmail.com" }
            };

            if (description.IsDeprecated)
                info.Description += "This API version has been deprecated.";

            return info;
        }
    }
}
