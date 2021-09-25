using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core;
using System.Linq;
using AssetManager.Models;

namespace AssetManager.DataUtils
{
    public class DataProcessorPosts : DataProcessorBase, IValidate
    {
        private readonly int _userId;
        
        public DataProcessorPosts(DataContext database, int userId) : base(database)
        {
            if (!Database.Users.Any(user => user.Id == userId))
                throw new ObjectNotFoundException("User id was not found");
            
            _userId = userId;
        }

        public List<Post> Posts => Database.Posts.ToList();

        public override void AddElement(object element)
        {
            if (!(element is Post post))
                throw new ArgumentException("Element type is not operation");
            
            if (!Validate(element))
                throw new ValidationException("Element is not correct");

            post.UserId = _userId;

            Database.Posts.Add(post);
            Save();
        }

        public override void RemoveElement(object element)
        {
            throw new NotImplementedException();
        }

        public bool Validate(object element)
        {
            var post = (Post) element;
            return post.Text.Length < 70 && post.Datetime <= DateTime.Now;
        }
    }
}