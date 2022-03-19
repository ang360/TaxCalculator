using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TaxCalculator.API
{
    //Modify Swagger to implement headers
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        //Apply headers to Swagger
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            OpenApiSchema schema = new OpenApiSchema();
            schema.Type = "string";
            //Add as many headers as you want
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "ClientID",
                In = ParameterLocation.Header,
                Required = true,
                Schema = schema
            });
        }
    }
}
