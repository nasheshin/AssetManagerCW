using System.Windows.Controls;

namespace AssetManager.AssetControls
{
    public partial class PortfolioControl
    {
        public PortfolioControl()
        {
            InitializeComponent();

            DataContext = new PortfolioControlVm();
        }
    }
}