using System;
using System.Collections.Generic;
using System.Linq;
using AssetManager.Models;

namespace AssetManager.DataUtils
{
    public class DataProcessorNewsItems : DataProcessorBase
    {
        public DataProcessorNewsItems(DataContext database) : base(database)
        {
        }

        public List<NewsItem> NewsItems => Database.NewsItems.ToList();

        public override void AddElement(object element)
        {
            throw new NotImplementedException();
        }

        public override void RemoveElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}