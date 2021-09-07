using System.Linq;
using AssetManager.Models;

namespace AssetManager.SocialPage
{
    public partial class SocialPageControl
    {
        public SocialPageControl()
        {
            InitializeComponent();
            
            var database = new DataContext();

            foreach (var post in database.Posts)
            {
                Posts.Children.Add(new PostControl(post));
            }

            DataContext = new SocialPageControlVm(Posts);
        }
    }
}