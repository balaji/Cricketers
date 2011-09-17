using Microsoft.Phone.Controls;
using Cricketers.Database;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows;
using System;
using System.Windows.Media;
using Cricketers.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Cricketers {
    public partial class Players : PhoneApplicationPage {
        public Players() {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            string country = "";
            if (NavigationContext.QueryString.TryGetValue("country", out country)) {
                PageTitle.Text = country.ToLower();
            }
            PlayersList.ItemsSource = PlayersShortName.ShortNames(country);
        }
        
        private void PlayersList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            PlayerDisplay player = PlayersList.SelectedItem as PlayerDisplay;
            if (player != null) {
                this.NavigationService.Navigate(new Uri("/Stats.xaml?id=" + player.Id, UriKind.Relative));
            }
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            ((TextBlock)sender).Foreground = (Brush)Application.Current.Resources["PhoneContrastBackgroundBrush"];
        }

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ((TextBlock)sender).Foreground = (Brush)Application.Current.Resources["PhoneAccentBrush"];
        }
    }
}