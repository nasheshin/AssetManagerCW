using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using AssetManager.Annotations;
using AssetManager.Models;
using AssetManager.Utils;

namespace AssetManager.SocialPage
{
    public class SocialPageControlVm : INotifyPropertyChanged
    {
        private readonly DataContext _database;
        private StackPanel _postsPanel;
        
        private string _postText;

        private RelayCommand _addPostCommand;
        
        public SocialPageControlVm(StackPanel postsPanel)
        {
            _database = new DataContext();
            _postsPanel = postsPanel;
        }

        public string PostText
        {
            get => _postText;
            set
            {
                _postText = value;
                OnPropertyChanged(nameof(PostText));
            }
        }
        
        public RelayCommand AddPostCommand
        {
            get
            {
                return _addPostCommand ?? (_addPostCommand = new RelayCommand(obj => AddPost()));
            }
        }

        private void AddPost()
        {
            var post = new Post {Text = PostText, Datetime = DateTime.Now, UserId = SessionInfo.UserId};
            
            _database.Posts.Add(post);
            _database.SaveChanges();
            _postsPanel.Children.Insert(0, new PostControl(post));
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}