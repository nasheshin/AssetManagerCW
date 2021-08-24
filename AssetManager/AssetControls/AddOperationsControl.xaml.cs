namespace AssetManager.AssetControls
{
    public partial class AddOperationsControl
    {
        public AddOperationsControl()
        {
            InitializeComponent();

            ViewModel = new AddOperationsControlVm();
            DataContext = ViewModel;
        }
        
        public AddOperationsControlVm ViewModel { get; }
    }
}