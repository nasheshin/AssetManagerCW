using System.Linq;
using AssetManager.Models;

namespace AssetManager.SocialPage
{
    public partial class PostControl
    {
        public PostControl(Post post)
        {
            InitializeComponent();
            
            var dataProcessorUsers = App.DataProcessorUsers;

            User.Content = dataProcessorUsers.Users.FirstOrDefault(user => user.Id == post.UserId)?.Name;
            Text.Text = post.Text;
            Datetime.Content = post.Datetime;
        }
    }
}