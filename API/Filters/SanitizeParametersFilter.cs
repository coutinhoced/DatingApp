using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
    public class SanitizeParametersFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Get the action method parameters
            var parameters = context.ActionArguments;

            var actionDescriptorparameters = context.ActionDescriptor.Parameters;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();

            foreach (var parameter in actionDescriptorparameters)
            {
                var par = parameter.ParameterType.FullName;
                Type type = Type.GetType(parameter.ParameterType.AssemblyQualifiedName);

                keyValuePairs.Add(par, null);
            }

            foreach (var parameter in parameters)
            {
                // Get parameter name and type
               // var paramName = parameter.Key;
               // var paramType = parameter.Value;

            }

            await next();
        }
    }
}
