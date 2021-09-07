using System.Linq;
using AssetManager.Models;

namespace AssetManager.SocialPage
{
    public partial class PostControl
    {
        public PostControl(Post post)
        {
            InitializeComponent();
            
            var database = new DataContext();
            
            User.Content = database.Users.ToList().FirstOrDefault(user => user.Id == post.UserId)?.Name;
            Text.Text = post.Text;
            Datetime.Content = post.Datetime;
        }
    }
}