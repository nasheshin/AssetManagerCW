using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using AssetManager.Annotations;
using AssetManager.DataUtils;
using AssetManager.Models;
using AssetManager.Utils;

namespace AssetManager.Authorization
{
    public class RegisterControlVm : INotifyPropertyChanged
    {
        private readonly DataProcessorUsers _dataProcessorUsers;
        
        private string _userName;
        private string _password;
        private string _passwordRepeat;
        
        private RelayCommand _registerCommand;

        public RegisterControlVm()
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
            try
            {
                if (_password != _passwordRepeat)
                {
                    MessageBox.Show(Localization.Message.DifferentPasswords);
                    return;
                }

                var foundUserName = _dataProcessorUsers.Users.FirstOrDefault(user => user.Name == _userName);
                if (foundUserName != null)
                {
                    MessageBox.Show(Localization.Message.UsernameEngaged);
                    return;
                }

                _dataProcessorUsers.AddElement(new User {Name = _userName, Password = _password});

                MessageBox.Show(Localization.Message.RegistrationSuccessful);
            }
            catch (ValidationException)
            {
                MessageBox.Show(Localization.Message.NotCorrectRegisterFields);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                MessageBox.Show(Localization.Error.Standard);
            }
        }
        
        
        
        
        
        
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}