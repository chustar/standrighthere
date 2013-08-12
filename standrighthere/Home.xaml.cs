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
using Parse;
using Microsoft.Phone.Tasks;

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
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!HomeViewModel.IsDataLoaded)
            {
                SystemTray.ProgressIndicator = new ProgressIndicator();
                SystemTray.ProgressIndicator.IsVisible= true;
                SystemTray.ProgressIndicator.IsIndeterminate = true;
                SystemTray.ProgressIndicator.Text = "Loading...";
                await HomeViewModel.LoadData();
                SystemTray.ProgressIndicator.IsVisible= false;
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

        public UserViewModel UserViewModel { get; private set; }

        private void Home_Loaded(object sender, RoutedEventArgs e)
        {
            if (ParseUser.CurrentUser == null)
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

        private void Begin_PhotoChooserTask(object sender, RoutedEventArgs e)
        {
            var photoTask = new PhotoChooserTask();
            photoTask.PixelHeight = 480;
            photoTask.PixelWidth = 480;
            photoTask.ShowCamera = true;
            photoTask.Completed += Complete_PhotoChooserTask;

            photoTask.Show();
        }

        void Complete_PhotoChooserTask(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                App.PhotoResult = e;
                NavigationService.Navigate(new Uri("/NewChallenge.xaml", UriKind.Relative));
            }
        }
    }
}