using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using standrighthere.ViewModels;
using System.Threading.Tasks;

namespace standrighthere
{
    public partial class Home : PhoneApplicationPage
    {
        public Home()
        {
            InitializeComponent();

            DataContext = HomeViewModel;
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!HomeViewModel.IsDataLoaded)
            {
                HomeViewModel.LoadData();
            }

            if (UserDetails == null)
            {
                UserDetails.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                Join.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private HomeViewModel homeViewModel;
        private HomeViewModel HomeViewModel
        {
            get
            {
                if (homeViewModel == null)
                {
                    homeViewModel = new HomeViewModel();
                }
                return homeViewModel;
            }
        }

        private void Home_Loaded(object sender, RoutedEventArgs e)
        {
            if (UserDetails == null)
            {
                Join.Visibility = System.Windows.Visibility.Visible;
                MessageBoxResult m = MessageBox.Show(
                    "You do not seem to be registered on stand right here.\n" +
                    "Would you like to register?",
                    "Register",
                    MessageBoxButton.OKCancel
                    );

                if (m == MessageBoxResult.OK)
                {
                    NavigationService.Navigate(new Uri("/NewUser.xaml", UriKind.Relative));
                }
            }
            else
            {
                UserDetails.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}