using AppService.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using System.Reflection;

namespace DynamicAndGenericControllersSample
{
    public class GenericControllerRouteConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.IsGenericType)
            {
                var genericType = controller.ControllerType.GenericTypeArguments[0];
                var generatedControllerAttribute = genericType.GetCustomAttribute<GeneratedControllerAttribute>();
                var commandCustomNameAttribute = genericType.GetCustomAttribute<CommandRouteAttribute>();

                var route = generatedControllerAttribute?.Route + '/' + commandCustomNameAttribute.Route;
                if (string.IsNullOrEmpty(route))
                {
                    controller.Selectors.Add(new SelectorModel
                    {
                        AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(route)),
                    });
                }
                else
                {
                    controller.ControllerName = genericType.Name;
                }
            }
        }
    }
}
