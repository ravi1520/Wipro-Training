using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AdvancedRoutingDemo.Constraints   // 👈 must match your project namespace
{
    public class CategoryConstraint : IRouteConstraint
    {
        private readonly string[] _categories = { "electronics", "books", "clothing" };

        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey,
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey(routeKey)) return false;
            var category = values[routeKey]?.ToString()?.ToLower();
            return _categories.Contains(category);
        }
    }
}
