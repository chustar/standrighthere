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

            //if (ParseUser.CurrentUser != null)
            //{
            //    HomeViewModel.User = new UserViewModel(ParseUser.CurrentUser);
            //}
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

        private MainViewModel homeViewModel;
        private MainViewModel HomeViewModel
        {
            get
            {
                if (homeViewModel == null)
                {
                    homeViewModel = new MainViewModel();
                }
                return homeViewModel;
            }
        }

        public UserViewModel User { get; private set; }

        private void Home_Loaded(object sender, RoutedEventArgs e)
        {
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

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedItem != null)
            {
                App.CurrentChallenge = (sender as ListBox).SelectedItem as ChallengeViewModel;
                NavigationService.Navigate(new System.Uri(string.Format("/Challenge.xaml"), System.UriKind.Relative));
                (sender as ListBox).SelectedItem = null;
            }
        }
        
        private async void LongListSelector_ItemRealized(object sender, ItemRealizationEventArgs e)
        {
            var challengeListViewModel = App.ViewModel.ChallengeListViewModel;
            if (!challengeListViewModel.IsDataLoading && Challenges.ItemsSource != null && Challenges.ItemsSource.Count >= challengeListViewModel.CurrentlyLoaded)
            {
                if (e.ItemKind == LongListSelectorItemKind.Item)
                {
                    if ((e.Container.Content as ChallengeViewModel).Equals(Challenges.ItemsSource[Challenges.ItemsSource.Count - challengeListViewModel.CurrentlyLoaded]))
                    {
                        await challengeListViewModel.LoadData(true);
                    }
                }
            }
        }

        private void Challenges_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as LongListSelector).SelectedItem != null)
            {
                App.CurrentChallenge = (sender as LongListSelector).SelectedItem as ChallengeViewModel;
                NavigationService.Navigate(new System.Uri(string.Format("/Challenge.xaml"), System.UriKind.Relative));
                (sender as LongListSelector).SelectedItem = null;
            }
        }
    }
}