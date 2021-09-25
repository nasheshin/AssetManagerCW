using System.Windows;
using System.Windows.Controls;

namespace AssetManager.Authorization
{
    public partial class SignInControl
    {
        private readonly SignInControlVm _viewModel;
        
        public SignInControl()
        {
            InitializeComponent();

            _viewModel = new SignInControlVm();
            DataContext = _viewModel;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var password = ((PasswordBox) sender).Password;
            _viewModel.Password = password;
        }
    }
}