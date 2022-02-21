namespace Presentation.WebSPA.ApiConfig;

using System;
using System.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class SwaggerDefaultValueAttribute : Attribute
{
    public SwaggerDefaultValueAttribute(string param, string value)
    {
        Parameter = param;
        Value = value;
    }

    public string Parameter { get; set; }
    public string Value { get; set; }
}

public class AddDefaulValueOperation : IOperationFilter
{
    public void Apply(OpenApiOperation operationParam, OperationFilterContext contextParam)
    {
        if (operationParam.Parameters == null || !operationParam.Parameters.Any())
        {
            return;
        }

        var attributes = contextParam.MethodInfo.GetCustomAttributes
                (typeof(SwaggerDefaultValueAttribute), true)
            .Cast<SwaggerDefaultValueAttribute>()
            .ToList();

        if (!attributes.Any())
        {
            return;
        }

        foreach (var parameter in operationParam.Parameters)
        {
            var attr = attributes.SingleOrDefault(it => it.Parameter == parameter.Name);
            if (attr != null)
            {
                parameter.Example = new OpenApiString(attr.Value);
            }
        }
    }
}