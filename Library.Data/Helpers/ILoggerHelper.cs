using System;

namespace Library.Data.Helpers
{
    public interface ILoggerHelper
    {
        void LogError(string metodo, Exception e);
        void LogInfo(string metodo, string message);
    }
}
