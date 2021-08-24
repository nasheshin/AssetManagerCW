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
        
        public MainWindow(Window authorizationWindow)
        {
            _authorizationWindow = authorizationWindow;
            
            InitializeComponent();
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