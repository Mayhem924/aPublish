using Microsoft.UI.Xaml.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace aPublish.View
{
    public class PostData
    {
        public string title;
        public string content;
        public string author;
        public string lang;
        public List<string> tags;
    }

    public sealed partial class CreatePostDialog : ContentDialog
    {
        public static event RoutedEventHandler OnPostCreated;

        PostData post = new PostData();

        public CreatePostDialog()
        {
            InitializeComponent();
        }

        private async void SendButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var url = "http://apublish-test.herokuapp.com/api/post";

            post.title = postTitile.Text;
            post.content = postContent.Text;
            post.author = postAuthor.Text;
            post.lang = postLang.Text;

            string json = JsonConvert.SerializeObject(post);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(url, httpContent);
                var statusCode = response.StatusCode;

                if (statusCode.ToString().Equals("Created", StringComparison.OrdinalIgnoreCase))
                {
                    OnPostCreated?.Invoke(this, new RoutedEventArgs());
                }
            }
        }

        private void CancelButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //TODO Save post data to buffer
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (postTitile.Text != String.Empty && postContent.Text != String.Empty)
            {
                createPostDialog.IsPrimaryButtonEnabled = true;
            }
            else
            {
                createPostDialog.IsPrimaryButtonEnabled = false;
            }
        }
    }
}
