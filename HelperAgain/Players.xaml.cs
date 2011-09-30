using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Cricketers.Data;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Cricketers
{
    public partial class Players : PhoneApplicationPage
    {
        public Players()
        {
            InitializeComponent();
        }
        private ProgressIndicator _progressIndicator;
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string country = "";
            if (NavigationContext.QueryString.TryGetValue("country", out country))
            {
                PageTitle.Text = country.ToLower();
            }
            PlayersList.ItemsSource = PlayersShortName.ShortNames(country);
        }

        private void PlayersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlayerDisplay player = PlayersList.SelectedItem as PlayerDisplay;
            if (player != null)
            {
                this.NavigationService.Navigate(new Uri("/Stats.xaml?id=" + player.Id, UriKind.Relative));
            }
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            ((TextBlock)sender).Foreground = (Brush)Application.Current.Resources["PhoneContrastBackgroundBrush"];
        }

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (null == _progressIndicator)
            {
                _progressIndicator = new ProgressIndicator();
                _progressIndicator.IsVisible = true;
                SystemTray.ProgressIndicator = _progressIndicator;
            }
            _progressIndicator.IsIndeterminate = true;
            ((TextBlock)sender).Foreground = (Brush)Application.Current.Resources["PhoneAccentBrush"];
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            if (_progressIndicator != null)
            {
                _progressIndicator.IsIndeterminate = false;
            }
        }

        private void ApplicationTitle_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }
    }
}