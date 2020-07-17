using System;
using System.Collections.Generic;
using System.Net.Http;
using aPublish.View;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace aPublish.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Windows.UI.Xaml.Controls.Page
    {
        //List<Post> _posts = new List<Post>();

        public HomePage()
        {
            this.InitializeComponent();

            CreatePostDialog.OnPostCreated += OnPostCreated;

            GetPosts(this, null);
        }

        private async void GetPosts(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            var url = "http://apublish-test.herokuapp.com/";

            var response = await client.GetAsync($"{url}/api/0");
            response.EnsureSuccessStatusCode();

            string posts = await response.Content.ReadAsStringAsync();

            if (posts != null)
            {
                var post = Page.FromJson(posts);

                foreach (var item in post.Posts)
                {
                    PostsList.Items?.Add(item);
                }
            }

            client.Dispose();
        }

        private void ClearPostsList()
        {
            PostsList.Items?.Clear();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            CreatePostDialog createPostDialog = new CreatePostDialog();
            await createPostDialog.ShowAsync();
        }

        private void OnPostCreated(object sender, RoutedEventArgs args)
        {
            ClearPostsList();
            GetPosts(this, args);
            PostSendMessage.IsOpen = true;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ClearPostsList();
        }
    }
}
