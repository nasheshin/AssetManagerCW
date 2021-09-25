using System;
using AssetManager.Models;

namespace AssetManager.DataUtils
{
    public abstract class DataProcessorBase : IDisposable
    {
        protected DataProcessorBase(DataContext database)
        {
            Database = database;
        }
        
        protected readonly DataContext Database;

        public abstract void AddElement(object element);

        public abstract void RemoveElement(object element);

        protected void Save()
        {
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database?.Dispose();
        }
    }
}