using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using AssetManager.Models;

namespace AssetManager.DataUtils
{
    public class DataProcessorUsers : DataProcessorBase, IValidate
    {
        public DataProcessorUsers(DataContext database) : base(database)
        {
        }

        public List<User> Users => Database.Users.ToList();

        public override void AddElement(object element)
        {
            if (!(element is User user))
                throw new ArgumentException("Element type is not user");
            
            if (!Validate(user))
                throw new ValidationException("Element is not correct");

            Database.Users.Add(user);
            Save();
        }

        public override void RemoveElement(object element)
        {
            throw new NotImplementedException();
        }

        public bool Validate(object element)
        {
            var user = (User) element;
            
            const string pattern = "^[a-z0-9_]{3,16}$";
            return Regex.IsMatch(user.Name, pattern, RegexOptions.IgnoreCase) &&
                   Regex.IsMatch(user.Password, pattern, RegexOptions.IgnoreCase);
        }
    }
}