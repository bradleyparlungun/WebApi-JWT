using alxbrn_api.HttpActionResults;
using Newtonsoft.Json;
using System.Web.Http.ExceptionHandling;

namespace alxbrn_api.ExceptionHandlers
{
    public class UnhandledExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
#if DEBUG
            string content = JsonConvert.SerializeObject(context.Exception);
#else
            var content = @"{ ""message"" : ""Oops, something unexpected went wrong"" }";
#endif
            context.Result = new ErrorContentResult(content, "application/json", context.Request);
        }
    }
}