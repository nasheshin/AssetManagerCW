using System.Windows.Controls;

namespace AssetManager.AssetControls
{
    public partial class AnalyticsControl
    {
        public AnalyticsControl()
        {
            InitializeComponent();

            DataContext = new AnalyticsControlVm();
        }
    }
}