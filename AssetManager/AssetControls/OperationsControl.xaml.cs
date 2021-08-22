using System.Windows;

namespace AssetManager.AssetControls
{
    public partial class OperationsControl
    {
        private readonly OperationsControlViewModel _viewModel;
        
        public OperationsControl()
        {
            InitializeComponent();

            _viewModel = new OperationsControlViewModel();
            DataContext = _viewModel;
        }
    }
}