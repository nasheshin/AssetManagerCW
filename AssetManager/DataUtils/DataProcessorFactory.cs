using AssetManager.Models;

namespace AssetManager.DataUtils
{
    public class DataProcessorFactory
    {
        private readonly DataContext _database;

        public DataProcessorFactory(DataContext database)
        {
            _database = database;
        }

        public DataProcessorBase CreateProcessor(DataProcessorType type, int userId = -1)
        {
            switch (type)
            {
                case DataProcessorType.Operations:
                    return new DataProcessorOperations(_database, userId);
                case DataProcessorType.Users:
                    return new DataProcessorUsers(_database);
            }

            return null;
        }
    }
}