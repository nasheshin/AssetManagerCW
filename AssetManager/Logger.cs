using System;
using AssetManager.DataUtils;
using AssetManager.Models;

namespace AssetManager
{
    public static class Logger
    {
        private static DataProcessorLogs DataProcessorLogs => App.DataProcessorLogs;

        public static void LogException(Exception exception)
        {
            DataProcessorLogs?.AddElement(new Log {Datetime = DateTime.Now, Message = exception.Message});
        }
    }
}