namespace AssetManager.SocialPage
{
    public partial class SocialPageControl
    {
        public SocialPageControl()
        {
            InitializeComponent();

            var dataProcessorPosts = App.DataProcessorPosts;

            foreach (var post in dataProcessorPosts.Posts)
            {
                Posts.Children.Add(new PostControl(post));
            }

            DataContext = new SocialPageControlVm(Posts);
        }
    }
}