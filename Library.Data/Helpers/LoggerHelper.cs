using System;
using System.Diagnostics;

namespace Library.Data.Helpers
{
    public class LoggerHelper : ILoggerHelper
    {
       public void LogError(string metodo, Exception e)
        {
            Trace.TraceError(metodo + " >> [" + e + "]");
        }
        public void LogInfo(string metodo, string message)
        {
            Trace.TraceInformation(metodo + " >> [" + message+"]");
        }
    }
}
