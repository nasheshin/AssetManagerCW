using System;

namespace AssetManager.DataUtils
{
    public class ErrorEventArgs : EventArgs
    {
        public ErrorEventArgs(string message)
        {
            Message = message;
        }
        
        public string Message { get; }
    }
}