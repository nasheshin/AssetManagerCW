using System.Collections.Generic;
using AssetManager.Models;

namespace AssetManager.DataProcessors
{
    internal abstract class DataProcessor
    {
        protected DataProcessor()
        {
        }

        public virtual IEnumerable<DataModel> Select()
        {
            return null;
        }

        public virtual void Insert()
        {
            return;
        }
    }
}