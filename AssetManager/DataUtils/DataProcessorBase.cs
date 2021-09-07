using AssetManager.Models;

namespace AssetManager.DataUtils
{
    public abstract class DataProcessorBase
    {
        protected DataProcessorBase(DataContext database)
        {
            Database = database;
        }
        
        protected DataContext Database;

        public abstract void AddElement(object element);

        public abstract void RemoveElement(object element);

        protected void Save()
        {
            Database.SaveChanges();
        }
    }
}