namespace KocFinansCC.Api.Helpers
{
    using System.ComponentModel;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            foreach (var parameter in operation.Parameters.OfType<NonBodyParameter>())
            {
                var description = context.ApiDescription
                    .ParameterDescriptions
                    .First(p => p.Name == parameter.Name);
                var routeInfo = description.RouteInfo;

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                var defaultValueAttribute = ((DefaultModelMetadata)description.ModelMetadata)?.Attributes?.Attributes?.FirstOrDefault(x => x.GetType() == typeof(DefaultValueAttribute));
                if (defaultValueAttribute != null)
                {
                    parameter.Default = ((DefaultValueAttribute)defaultValueAttribute).Value;
                }

                if (routeInfo == null)
                {
                    continue;
                }

                if (routeInfo.DefaultValue != null)
                {
                    parameter.Default = routeInfo.DefaultValue;
                }

                parameter.Required |= !routeInfo.IsOptional;
            }
        }
    }
}
