using System;
using System.Collections.Generic;
using System.Linq;
using AssetManager.Models;

namespace AssetManager.DataUtils
{
    public class DataProcessorAnalytics : DataProcessorBase
    {
        public DataProcessorAnalytics(DataContext database) : base(database)
        {
        }

        public List<AssetAnalytic> AssetAnalytics => Database.AssetAnalytics.ToList();

        public override void AddElement(object element)
        {
            throw new NotImplementedException();
        }

        public override void RemoveElement(object element)
        {
            throw new NotImplementedException();
        }
        
        public AssetAnalytic FindAnalyticByName(string name)
        {
            return AssetAnalytics.FirstOrDefault(a => a.AssetName.ToLower() == name.ToLower());
        }
    }
}