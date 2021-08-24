using System.Windows;

namespace AssetManager.AssetControls
{
    public partial class OperationsControl
    {
        private readonly OperationsControlViewModel _viewModel;
        
        public OperationsControl()
        {
            var addOperationsControl = new AddOperationsControl();

            _viewModel = new OperationsControlViewModel(addOperationsControl.ViewModel);
            DataContext = _viewModel;
            
            InitializeComponent();
            
            WrapPanel.Children.Add(addOperationsControl);
        }
    }
}