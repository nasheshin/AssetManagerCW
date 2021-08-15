using System.Windows.Controls;

namespace AssetManager.AuthorizationWindows
{
    public partial class RegisterControl
    {
        public RegisterControl()
        {
            InitializeComponent();

            DataContext = new RegisterControlViewModel();
        }
    }
}