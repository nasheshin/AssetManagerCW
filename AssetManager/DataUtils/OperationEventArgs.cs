using System;
using AssetManager.Models;

namespace AssetManager.DataUtils
{
    public class OperationEventArgs : EventArgs
    {
        public OperationEventArgs(Operation operation, OperationCommandType commandType)
        {
            Operation = operation;
            CommandType = commandType;
        }
        
        public Operation Operation { get; }
        public OperationCommandType CommandType { get; }
    }
}