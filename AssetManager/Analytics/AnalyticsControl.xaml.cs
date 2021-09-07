namespace AssetManager.Analytics
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