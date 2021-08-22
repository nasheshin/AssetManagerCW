using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using AssetManager.Annotations;
using AssetManager.Models;
using AssetManager.Utils;

namespace AssetManager.Authorization
{
    public class SignInControlViewModel
    {
        private readonly DataContext _database;
        
        private string _userName;
        private string _password;
        
        private RelayCommand _signInCommand;

        public SignInControlViewModel()
        {
            _database = new DataContext();
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
            var foundUserName = _database.Users.FirstOrDefault(user => user.Name == _userName);
            if (foundUserName == null)
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }

            if (foundUserName.Password != _password)
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }

            SessionInfo.UserId = foundUserName.Id;
            
            var mainWindow = new MainWindow(Application.Current.MainWindow);
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