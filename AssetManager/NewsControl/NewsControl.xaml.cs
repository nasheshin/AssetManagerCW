namespace AssetManager.NewsControl
{
    public partial class NewsControl
    {
        public NewsControl()
        {
            InitializeComponent();

            var dataProcessorNewsItems = App.DataProcessorNewsItems;

            foreach (var newsItem in dataProcessorNewsItems.NewsItems)
            {
                NewsItems.Children.Add(new NewsItemControl(newsItem));
            }
        }
    }
}