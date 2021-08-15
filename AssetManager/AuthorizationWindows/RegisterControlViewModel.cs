using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using AssetManager.Annotations;
using AssetManager.Models;

namespace AssetManager.AuthorizationWindows
{
    public class RegisterControlViewModel : INotifyPropertyChanged
    {
        private readonly DataContext _database;
        
        private string _userName;
        private string _password;
        private string _passwordRepeat;
        
        private RelayCommand _registerCommand;

        public RegisterControlViewModel()
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
        
        public string PasswordRepeat
        {
            get => _passwordRepeat;
            set
            {
                _passwordRepeat = value;
                OnPropertyChanged(nameof(PasswordRepeat));
            }
        }
        
        public RelayCommand RegisterCommand
        {
            get
            {
                return _registerCommand ?? (_registerCommand = new RelayCommand(obj => Register()));
            }
        }

        private void Register()
        {
            if (_password != _passwordRepeat)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }
            
            var foundUserName = _database.Users.FirstOrDefault(user => user.Name == _userName);
            if (foundUserName != null)
            {
                MessageBox.Show("Данное имя пользователя занято");
                return;
            }

            _database.Users.Add(new User {Name = _userName, Password = _password});
            _database.SaveChanges();

            MessageBox.Show("Регистрация прошла успешно");
        }
        
        
        
        
        
        
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}