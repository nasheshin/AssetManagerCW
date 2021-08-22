namespace AssetManager.Authorization
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