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
                case DataProcessorType.AssetAnalytics:
                    return new DataProcessorAnalytics(_database);
                case DataProcessorType.Brokers:
                    return new DataProcessorBrokers(_database);
                case DataProcessorType.Operations:
                    return new DataProcessorOperations(_database, userId);
                case DataProcessorType.Users:
                    return new DataProcessorUsers(_database);
                case DataProcessorType.Posts:
                    return new DataProcessorPosts(_database, userId);
                case DataProcessorType.NewsItems:
                    return new DataProcessorNewsItems(_database);
                case DataProcessorType.Logs:
                    return new DataProcessorLogs(_database, userId);
            }

            return null;
        }
    }
}