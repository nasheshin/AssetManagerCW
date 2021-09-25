using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using AssetManager.Annotations;
using AssetManager.DataUtils;
using AssetManager.Models;
using AssetManager.Utils;

namespace AssetManager.Authorization
{
    public class SignInControlVm
    {
        private readonly DataProcessorUsers _dataProcessorUsers;
        
        private string _userName;
        private string _password;
        
        private RelayCommand _signInCommand;

        public SignInControlVm()
        {
            _dataProcessorUsers = App.DataProcessorUsers;
        }
        
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        
        public RelayCommand SignInCommand
        {
            get
            {
                return _signInCommand ?? (_signInCommand = new RelayCommand(obj => SignIn()));
            }
        }

        private void SignIn()
        {
            User foundUserName;
            try
            {
                foundUserName = _dataProcessorUsers.Users.FirstOrDefault(user => user.Name == _userName);
                if (foundUserName == null || foundUserName.Password != _password)
                {
                    MessageBox.Show(Localization.Message.WrongUsernameOrPassword);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                MessageBox.Show(Localization.Error.Standard);
                return;
            }

            var mainWindow = new MainWindow(Application.Current.MainWindow, foundUserName.Id);
            mainWindow.Show();
            Application.Current.MainWindow?.Hide();
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}