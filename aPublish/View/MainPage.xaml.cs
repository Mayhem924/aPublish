using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace aPublish
{
    public sealed partial class MainPage : Windows.UI.Xaml.Controls.Page
    {
        List<Page> posts;

        public MainPage()
        {
            this.InitializeComponent();
            posts = new List<Page>();

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
        }

        private async void GetPosts(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            var url = "http://apublish-test.herokuapp.com/";

            var webResponse = await client.GetAsync($"{url}/api/0");
            webResponse.EnsureSuccessStatusCode();

            string posts = await webResponse.Content.ReadAsStringAsync();
            
            if (posts != null)
            {
                var post = Page.FromJson(posts);

                foreach (var item in post.Posts)
                {
                    PostsList.Items.Add(item);
                }
            }
            
            client.Dispose();
        }

        private void ClearPostsList(object sender, RoutedEventArgs e)
        {
            PostsList.Items.Clear();
        }
    }
}