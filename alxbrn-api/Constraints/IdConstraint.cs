using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace alxbrn_api.Constraints
{
    public class IdConstraint : IHttpRouteConstraint
    {
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName,
            IDictionary<string, object> values,
            HttpRouteDirection routeDirection)
        {

            if (!values.TryGetValue(parameterName, out object value) || value == null)
            {
                return false;
            }

            try
            {
                return Convert.ToInt32(value) > 0;
            }
            catch
            {
                return false;
            }

        }
    }
}