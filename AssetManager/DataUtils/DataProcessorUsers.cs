using System;
using System.Collections.Generic;
using System.Linq;
using AssetManager.Models;

namespace AssetManager.DataUtils
{
    public class DataProcessorUsers : DataProcessorBase
    {
        public DataProcessorUsers(DataContext database) : base(database)
        {
        }

        public List<User> Users => Database.Users.ToList();

        public override void AddElement(object element)
        {
            if (!(element is User user))
                throw new ArgumentException("Element type is not operation");

            Database.Users.Add(user);
            Save();
        }

        public override void RemoveElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}