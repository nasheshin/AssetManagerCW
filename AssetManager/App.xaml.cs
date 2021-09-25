using System;
using AssetManager.DataUtils;
using AssetManager.Models;

namespace AssetManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static DataProcessorUsers DataProcessorUsers;
        public static DataProcessorOperations DataProcessorOperations;
        public static DataProcessorBrokers DataProcessorBrokers;
        public static DataProcessorAnalytics DataProcessorAnalytics;
        public static DataProcessorPosts DataProcessorPosts;
        public static DataProcessorNewsItems DataProcessorNewsItems;
        public static DataProcessorLogs DataProcessorLogs;

        private static DataProcessorFactory _dataProcessorFactory;
        
        public App()
        {
            _dataProcessorFactory = new DataProcessorFactory(new DataContext());
            CreateAuthWindowProcessors();
        }

        public static void CreateMainWindowProcessors(int userId)
        {
            DataProcessorOperations = (DataProcessorOperations) _dataProcessorFactory.CreateProcessor(DataProcessorType.Operations, userId);
            DataProcessorBrokers = (DataProcessorBrokers) _dataProcessorFactory.CreateProcessor(DataProcessorType.Brokers);
            DataProcessorAnalytics = (DataProcessorAnalytics) _dataProcessorFactory.CreateProcessor(DataProcessorType.AssetAnalytics);
            DataProcessorPosts = (DataProcessorPosts) _dataProcessorFactory.CreateProcessor(DataProcessorType.Posts, userId);
            DataProcessorNewsItems = (DataProcessorNewsItems) _dataProcessorFactory.CreateProcessor(DataProcessorType.NewsItems);
            DataProcessorLogs = (DataProcessorLogs) _dataProcessorFactory.CreateProcessor(DataProcessorType.Logs, userId);
        }
        
        private void CreateAuthWindowProcessors()
        {
            DataProcessorUsers = (DataProcessorUsers) _dataProcessorFactory.CreateProcessor(DataProcessorType.Users);
        }
    }
}