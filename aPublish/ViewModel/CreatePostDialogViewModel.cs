using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using aPublish.View;
using Newtonsoft.Json;
using Windows.UI.Xaml.Controls;

namespace aPublish.ViewModel
{
    public class CreatePostDialogViewModel : BaseViewModel
    {
        private string _author;
        private string _content;

        private bool _primaryButtonEnabled = false;

        

        public string Author
        {
            get => _author;
            set
            {
                if (_author == value) return;

                _author = value;
                NotifyPropertyChanged(nameof(Author));
            }
        }

        public string Content
        {
            get => _content;
            set
            {
                if (_content == value) return;

                _content = value;
                NotifyPropertyChanged(nameof(Content));
            }
        }

        public bool PrimaryButtonEnabled
        {
            get => _primaryButtonEnabled;
            set
            {
                _primaryButtonEnabled = value;
                NotifyPropertyChanged(nameof(PrimaryButtonEnabled));
            }
        }

        public async void SendPostAsync(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var url = "http://apublish-test.herokuapp.com/api/post";

            //post.title = postTitile.Text;
            //post.content = postContent.Text;
            //post.author = postAuthor.Text;
            //post.lang = postLang.Text;

            var post = new { Title = "123Test", Content = Content, Author = Author, Lang = "ru" };

            string json = JsonConvert.SerializeObject(post);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(url, httpContent);
                var statusCode = response.StatusCode;

                if (statusCode.ToString().Equals("Created", StringComparison.OrdinalIgnoreCase))
                {
                    //OnPostCreated?.Invoke(this, new RoutedEventArgs());
                }
            }
        }

        public void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            PrimaryButtonEnabled = Author != String.Empty && Content != String.Empty;
        }
    }
}
