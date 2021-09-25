using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using AssetManager.Models;

namespace AssetManager.DataUtils
{
    public class DataProcessorLogs : DataProcessorBase
    {
        private readonly int _userId;
        
        public DataProcessorLogs(DataContext database, int userId) : base(database)
        {
            if (!Database.Users.Any(user => user.Id == userId))
                throw new ObjectNotFoundException("User id was not found");
            
            _userId = userId;
        }

        public List<Log> Logs => Database.Logs.ToList(); 

        public override void AddElement(object element)
        {
            if (!(element is Log log))
                throw new ArgumentException("Element type is not operation");

            log.UserId = _userId;
            
            Database.Logs.Add(log);
            Save();
        }

        public override void RemoveElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}