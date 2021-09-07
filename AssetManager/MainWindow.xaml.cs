using System;
using System.Windows;
using AssetManager.DataUtils;
using AssetManager.Models;

namespace AssetManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static DataProcessorOperations DataProcessorOperations;
        
        private readonly Window _authorizationWindow;
        
        public MainWindow(Window authorizationWindow, int userId)
        {
            _authorizationWindow = authorizationWindow;
            
            InitializeComponent();

            var dataProcessorFactory = new DataProcessorFactory(new DataContext());
            DataProcessorOperations = (DataProcessorOperations) dataProcessorFactory.CreateProcessor(DataProcessorType.Operations);

        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnMainWindowClosed(object sender, EventArgs e)
        {
            _authorizationWindow.Show();
        }
    }
}