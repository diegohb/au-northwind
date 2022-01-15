namespace Presentation.WebSPA.ApiConfig
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;

    public static class MvcOptionsExtensions
    {
        public static void UseGeneralRoutePrefix(this MvcOptions optsParam, IRouteTemplateProvider routeAttributeParam)
        {
            optsParam.Conventions.Add(new RoutePrefixConvention(routeAttributeParam));
        }

        public static void UseGeneralRoutePrefix
        (this MvcOptions optsParam, string
            prefixParam)
        {
            optsParam.UseGeneralRoutePrefix(new RouteAttribute(prefixParam));
        }
    }
}