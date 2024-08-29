using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;
using System.Linq;

namespace PG.ApiBase.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerDefaultValues>();
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DefaultModelsExpandDepth(-1);
                var swaggerEndpoints = provider.ApiVersionDescriptions
                    .OrderByDescending(x => x.ApiVersion)
                    .Select(description => new
                    {
                        Url = $"/swagger/{description.GroupName}/swagger.json",
                        Name = description.GroupName.ToUpperInvariant()
                    });

                foreach (var endpoint in swaggerEndpoints)
                {
                    options.SwaggerEndpoint(endpoint.Url, endpoint.Name);
                }
            });

            return app;
        }
    }

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description, UriString));
            }
        }

        private static string UriString => "";

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description, string uriString)
        {
            var info = new OpenApiInfo()
            {
                Title = "API Base",
                Version = description.ApiVersion.ToString(),
            };

            if (description.IsDeprecated)
            {
                info.Description += " Esta versão está obsoleta!";
            }

            return info;
        }
    }


    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            operation.Deprecated |= apiDescription.IsDeprecated();

            foreach (var responseType in context.ApiDescription.SupportedResponseTypes)
            {
                var responseKey = responseType.IsDefaultResponse ? "default" : responseType.StatusCode.ToString();
                var response = operation.Responses[responseKey];
                foreach (var contentType in from contentType in response.Content.Keys
                                            where responseType.ApiResponseFormats.All(x => x.MediaType != contentType)
                                            select contentType)
                {
                    response.Content.Remove(contentType);
                }
            }

            if (operation.Parameters == null)
                return;

            var parametersToRemove = operation.Parameters.Where(x => x.Name == "api-version").ToList();
            foreach (var parameter in parametersToRemove)
                operation.Parameters.Remove(parameter);

            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                parameter.Description ??= description.ModelMetadata.Description;

                if (parameter.Schema.Default == null && description.DefaultValue != null)
                {
                    var json = JsonSerializer.Serialize(description.DefaultValue, description.ModelMetadata.ModelType);
                    parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson(json);
                }

                parameter.Required |= description.IsRequired;
            }
        }
    }
}
