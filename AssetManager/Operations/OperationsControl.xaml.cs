namespace AssetManager.Operations
{
    public partial class OperationsControl
    {
        private readonly OperationsControlVm _vm;
        
        public OperationsControl()
        {
            _vm = new OperationsControlVm();
            DataContext = _vm;
            
            InitializeComponent();
        }
    }
}