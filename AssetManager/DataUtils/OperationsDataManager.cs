using System;
using System.Linq;
using AssetManager.Models;
using AssetManager.Utils;

namespace AssetManager.DataUtils
{
    public class OperationsDataManager
    {
        private readonly DataContext _database;

        public OperationsDataManager()
        {
            _database = new DataContext();
        }

        public void AddOperation(Operation operation)
        {
            if (operation.UserId != SessionInfo.UserId)
                OnErrorRaised(new ErrorEventArgs("Ошибка обработки запроса данных"));
            
            try
            {
                _database.Operations.Add((Operation)operation.Clone());
                _database.SaveChanges();
                
                var addedNewOperation = _database.Operations.ToList().Last();
            
                OnDatabaseChanged(new OperationEventArgs(addedNewOperation, OperationCommandType.Add));
            }
            catch
            {
                OnErrorRaised(new ErrorEventArgs("Произошла ошибка при добавлении операции в базу данных"));
            }
        }
        
        public void RemoveOperation(Operation operation)
        {
            if (operation.UserId != SessionInfo.UserId)
                OnErrorRaised(new ErrorEventArgs("Ошибка обработки запроса данных"));
            
            try
            {
                var operationRemove =
                    _database.Operations.ToList().First(curOperation => curOperation.Id == operation.Id);

                _database.Operations.Remove(operationRemove);
                _database.SaveChanges();

                OnDatabaseChanged(new OperationEventArgs(operation, OperationCommandType.Remove));
            }
            catch
            {
                OnErrorRaised(new ErrorEventArgs("Произошла ошибка при удалении операции из базы данных"));
            }
        }

        public event EventHandler ErrorRaised;
        protected virtual void OnErrorRaised(ErrorEventArgs args)
        {
            ErrorRaised?.Invoke(this, args);
        }
        
        public event EventHandler DatabaseChanged;
        protected virtual void OnDatabaseChanged(OperationEventArgs args)
        {
            DatabaseChanged?.Invoke(this, args);
        }
    }
}