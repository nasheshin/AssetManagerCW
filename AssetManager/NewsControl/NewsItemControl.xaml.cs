using AssetManager.Models;

namespace AssetManager.NewsControl
{
    public partial class NewsItemControl
    {
        public NewsItemControl(NewsItem newsItem)
        {
            InitializeComponent();

            Header.Content = newsItem.Header;
            Text.Text = newsItem.Text;
            Datetime.Content = newsItem.Datetime;
        }
    }
}