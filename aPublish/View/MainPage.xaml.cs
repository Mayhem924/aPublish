using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Core;
using Windows.Storage.Search;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace aPublish.View
{
    public sealed partial class MainPage : Windows.UI.Xaml.Controls.Page
    {
        public static event Action OnNewClick;
        public static event Action OnClearClick;
        public static event Action OnLoadClick;

        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("newpost", null),
            ("home", typeof(HomePage)),
            ("favorites", typeof(FavoritesPage)),
            ("settings", typeof(SettingsPage)),
        };

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavigationView_OnLoaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigated += OnNavigated;

            NavigationView.SelectedItem = NavigationView.MenuItems[0];

            Navigate("home", new EntranceNavigationTransitionInfo());

            var goBack = new KeyboardAccelerator { Key = Windows.System.VirtualKey.GoBack };
            goBack.Invoked += BackInvoked;

            this.KeyboardAccelerators.Add(goBack);

            var altLeft = new KeyboardAccelerator
            {
                Key = Windows.System.VirtualKey.Left,
                Modifiers = Windows.System.VirtualKeyModifiers.Menu
            };

            altLeft.Invoked += BackInvoked;
            this.KeyboardAccelerators.Add(altLeft);
        }

        private void NavigationView_OnSelectionChanged(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (args.SelectedItemContainer != null)
            {
                var navItemTag = args.SelectedItemContainer.Tag.ToString();
                Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void OnNavigated(object sender, NavigationEventArgs args)
        {
            NavigationView.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(SettingsPage))
            {
                NavigationView.SelectedItem = (muxc.NavigationViewItem) NavigationView.SelectedItem;
                NavigationView.Header = "Settings";
            }
            else if (ContentFrame.SourcePageType != null)
            {
                var item = _pages.FirstOrDefault(p => p.Page == args.SourcePageType);

                NavigationView.SelectedItem = NavigationView.MenuItems
                    .OfType<muxc.NavigationViewItem>()
                    .First(n => n.Tag.Equals(item.Tag));

                //NavigationView.Header =
                //    ((muxc.NavigationViewItem) NavigationView.SelectedItem)?.Content?.ToString();
            }
        }

        private bool OnBackRequested()
        {
            if (!ContentFrame.CanGoBack)
                return false;

            if (NavigationView.IsPaneOpen &&
                (NavigationView.DisplayMode == muxc.NavigationViewDisplayMode.Compact ||
                 NavigationView.DisplayMode == muxc.NavigationViewDisplayMode.Minimal))
                return false;

            ContentFrame.GoBack();
            return true;
        }

        private void BackInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            OnBackRequested();
            args.Handled = true;
        }

        private void Navigate(string itemTag, NavigationTransitionInfo transitionInfo)
        {
            Type _page = null;

            if (itemTag.Equals("settings"))
            {
                _page = typeof(SettingsPage);
            }
            else
            {
                var item = _pages.FirstOrDefault(p => p.Tag.Equals(itemTag));
                _page = item.Page;
            }

            var prevPageType = ContentFrame.CurrentSourcePageType;

            if (!(_page is null))
               // && prevPageType.GetType().Equals(_page.GetType()))
            {
                ContentFrame.Navigate(_page, null, transitionInfo);
            }
        }

        private void InfoBar_Click(object sender, RoutedEventArgs e)
        {
            //PostSendMessage1.IsOpen = !PostSendMessage1.IsOpen;
        }

        private void NavigationView_OnBackRequested(muxc.NavigationView sender, muxc.NavigationViewBackRequestedEventArgs args)
        {
            OnBackRequested();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is AppBarButton button)
            {
                switch (button.Label)
                {
                    case "New":
                        OnNewClick?.Invoke();
                        break;
                    case "Clear":
                        OnClearClick?.Invoke();
                        break;
                    case "Load":
                        OnLoadClick?.Invoke();
                        break;
                }
            }
        }
    }
}