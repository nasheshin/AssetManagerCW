using System;
using System.Windows;

namespace AssetManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Window _authorizationWindow;
        
        public MainWindow(Window authorizationWindow, int userId)
        {
            _authorizationWindow = authorizationWindow;
            App.CreateMainWindowProcessors(userId);
            
            InitializeComponent();
        }

        private void OnMainWindowClosed(object sender, EventArgs e)
        {
            _authorizationWindow.Show();
        }
    }
}