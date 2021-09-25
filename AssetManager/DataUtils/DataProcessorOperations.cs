using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text.RegularExpressions;
using AssetManager.Models;

namespace AssetManager.DataUtils
{
    public class DataProcessorOperations : DataProcessorBase, IValidate
    {
        private readonly int _userId;
        
        public DataProcessorOperations(DataContext database, int userId) : base(database)
        {
            if (!Database.Users.Any(user => user.Id == userId))
                throw new ObjectNotFoundException("User id was not found");
            
            _userId = userId;
        }

        public List<Operation> Operations => Database.Operations.Where(o => o.UserId == _userId).ToList();

        public override void AddElement(object element)
        {
            if (!(element is Operation operation))
                throw new ArgumentException("Element type is not operation");

            if (operation.UserId != _userId)
                throw new ArgumentException("Operation user id is not from current session");

            if (!Validate(element))
                throw new ValidationException("Element is not correct");

            Database.Operations.Add((Operation) operation.Clone());
            Save();

            var addedNewOperation = Operations.ToList().Last();

            OnDatabaseChanged(new OperationEventArgs(addedNewOperation, OperationCommandType.Add));
        }

        public override void RemoveElement(object element)
        {
            if (!(element is Operation operation))
                throw new ArgumentException("Element should be an operation");
            
            if (operation.UserId != _userId)
                throw new ArgumentException("Operation user id is not from current session");
            
            var operationRemove = Operations.FirstOrDefault(curOperation => curOperation.Id == operation.Id);
            if (operationRemove == null)
                throw new ObjectNotFoundException("Operation to remove wasn't found");

            Database.Operations.Remove(operationRemove);
            Save();

            OnDatabaseChanged(new OperationEventArgs(operation, OperationCommandType.Remove));
        }
        
        public bool Validate(object element)
        {
            var operation = (Operation) element;
            
            const string pattern = "^[A-Z0-9]{1,50}$";
            return Regex.IsMatch(operation.AssetTicker, pattern) && (operation.Datetime < DateTime.Now);
        }
        
        public event EventHandler DatabaseChanged;
        protected virtual void OnDatabaseChanged(OperationEventArgs args)
        {
            DatabaseChanged?.Invoke(this, args);
        }
    }
}