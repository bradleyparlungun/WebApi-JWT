using System.Diagnostics;
using System.Web.Http.ExceptionHandling;

namespace alxbrn_api.Loggers
{
    public class UnhandledExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            string log = context.Exception.Message;
            Debug.WriteLine($"EXCEPTION LOGGED: {log}");
        }
    }
}