﻿namespace AssetManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            DataConnector.OpenConnection(Config.ConnectionString);
        }
    }
}