using AssetManager.AssetControls;
using AssetManager.Models;

namespace AssetManager.NewsControl
{
    public partial class NewsControl
    {
        public NewsControl()
        {
            InitializeComponent();

            var database = new DataContext();

            foreach (var newsItem in database.NewsItems)
            {
                NewsItems.Children.Add(new NewsItemControl(newsItem));
            }
        }
    }
}