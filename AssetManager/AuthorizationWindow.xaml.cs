using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using AssetManager.DataProcessors;
using AssetManager.Models;

namespace AssetManager
{
    public partial class LoginWindow
    {
        private UserDataProcessor _userDataProcessor;
        private BrokerDataProcessor _brokerDataProcessor;

        private List<UserDataModel> _users;
        private List<BrokerDataModel> _brokers;
        
        public LoginWindow()
        {
            if (DataConnector.State != ConnectionState.Open)
            {
                MessageBox.Show("Не удалось подключиться к базе данных. Пожалуйста проверьте подключение");
                Close();
                return;
            }

            _userDataProcessor = new UserDataProcessor();
            _brokerDataProcessor = new BrokerDataProcessor();
            
            _users = _userDataProcessor.Select().Select(user => (UserDataModel)user).ToList();
            
            InitializeComponent();
        }

        private void OnLoginWindowClosed(object sender, EventArgs e)
        {
            DataConnector.CloseConnection();
        }
    }
}