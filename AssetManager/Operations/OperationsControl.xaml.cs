namespace AssetManager.Operations
{
    public partial class OperationsControl
    {
        private readonly OperationsControlViewModel _viewModel;
        
        public OperationsControl()
        {
            _viewModel = new OperationsControlViewModel();
            DataContext = _viewModel;
            
            InitializeComponent();
        }
    }
}