using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using AssetManager.Annotations;
using AssetManager.DataUtils;
using AssetManager.Models;
using AssetManager.Utils;

namespace AssetManager.SocialPage
{
    public class SocialPageControlVm : INotifyPropertyChanged
    {
        private readonly DataProcessorPosts _dataProcessorPosts;
        private readonly StackPanel _postsPanel;
        
        private string _postText;

        private RelayCommand _addPostCommand;
        
        public SocialPageControlVm(StackPanel postsPanel)
        {
            _dataProcessorPosts = App.DataProcessorPosts;
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
            try
            {
                var post = new Post {Text = PostText, Datetime = DateTime.Now};

                _dataProcessorPosts.AddElement(post);
                _postsPanel.Children.Insert(0, new PostControl(post));
            }
            catch (ValidationException)
            {
                MessageBox.Show(Localization.Message.NotCorrectPost);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                MessageBox.Show(Localization.Error.Standard);
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}