using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Cricketers.Database;
using System.Windows.Media;

namespace Cricketers {
    public partial class MainPage : PhoneApplicationPage {

        public MainPage() {           
            InitializeComponent();
            var countries = (from profile in App.DB.Profiles select profile.Country).Distinct<string>();
            CountriesList.ItemsSource = countries;
            //using (CricketersDataContext db = new CricketersDataContext(CricketersDataContext.DBConnectionString))
            //{
            //    if (db.DatabaseExists() == false)
            //    {
            //        //Create the database
            //        db.CreateDatabase();
            //    }
            //    new Cricketers.Data.Utility().Extract();
            //}
       }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e) {
            HyperlinkButton button = sender as HyperlinkButton;
            ((TextBlock)button.Content).Foreground = (Brush)Application.Current.Resources["PhoneAccentBrush"];
            this.NavigationService.Navigate(new Uri("/Players.xaml?country=" + button.Tag, UriKind.Relative));
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            ((TextBlock)sender).Foreground = (Brush)Application.Current.Resources["PhoneContrastBackgroundBrush"];
        }
    }
}