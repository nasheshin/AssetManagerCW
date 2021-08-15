namespace AssetManager.AuthorizationWindows
{
    public partial class SignInControl
    {
        public SignInControl()
        {
            InitializeComponent();
            
            DataContext = new SignInControlViewModel();
        }
    }
}