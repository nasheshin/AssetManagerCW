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
        public static DataProcessorOperations DataProcessorOperations;
        public static DataProcessorUsers DataProcessorUsers;

        private readonly DataProcessorFactory _dataProcessorFactory;
        
        public App()
        {
            _dataProcessorFactory = new DataProcessorFactory(new DataContext());
            CreateAuthWindowProcessors();
        }

        private void CreateAuthWindowProcessors()
        {
            DataProcessorUsers = (DataProcessorUsers) _dataProcessorFactory.CreateProcessor(DataProcessorType.Users);
        }

        private void CreateMainWindowProcessors()
        {
            
        }
    }
}