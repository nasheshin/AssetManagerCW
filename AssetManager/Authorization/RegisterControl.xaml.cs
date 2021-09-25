using System.Windows;
using System.Windows.Controls;

namespace AssetManager.Authorization
{
    public partial class RegisterControl
    {
        private readonly RegisterControlVm _viewModel;
        
        public RegisterControl()
        {
            InitializeComponent();

            _viewModel = new RegisterControlVm();
            DataContext = _viewModel;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var password = ((PasswordBox) sender).Password;
            _viewModel.Password = password;
        }

        private void OnPasswordRepeatChanged(object sender, RoutedEventArgs e)
        {
            var password = ((PasswordBox) sender).Password;
            _viewModel.PasswordRepeat = password;
        }
    }
}