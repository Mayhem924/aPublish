using System;
using System.Collections.Generic;
using System.Net.Http;
using aPublish.View;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace aPublish.View
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();

            MainPage.OnClearClick += ButtonBase_OnClick;
            MainPage.OnNewClick += Button_Click;
            MainPage.OnLoadClick += GetPosts;

            GetPosts();
        }

        private async void GetPosts()
        {
            HttpClient client = new HttpClient();
            var url = "http://apublish-test.herokuapp.com/";

            var response = await client.GetAsync($"{url}/api/0");
            response.EnsureSuccessStatusCode();

            string posts = await response.Content.ReadAsStringAsync();

            if (posts != null)
            {
                var post = Model.Page.FromJson(posts);

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

        private async void Button_Click()
        {
            CreatePostDialog createPostDialog = new CreatePostDialog();
            await createPostDialog.ShowAsync();
        }

        private void ButtonBase_OnClick()
        {
            ClearPostsList();
        }
    }
}
