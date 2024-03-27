using System.Text.RegularExpressions;

namespace RoutingExample.CustomConstraints
{
    public class MonthsCustomConstraint : IRouteConstraint
    {
        //Eg: sales-report/2023/mar
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey(routeKey))
            {
                return false;
            }
            Regex regex = new Regex("^mar|jun|sep|dec$");
            string? monthValue = Convert.ToString(values[routeKey]);
            if (regex.IsMatch(monthValue!))
            {
                return true;
            }
            return false;
        }
    }
}
