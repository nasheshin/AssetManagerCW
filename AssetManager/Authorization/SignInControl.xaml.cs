namespace AssetManager.Authorization
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