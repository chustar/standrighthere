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

            DataContext = App.ViewModel;
        }

        // Load data for the ViewModel Items
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            (ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled= App.ViewModel.User.IsLoggedIn;
            if (!App.ViewModel.IsDataLoaded)
            {
                await App.ViewModel.LoadData();
            }
        }

        void Complete_PhotoChooserTask(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                App.PhotoResult = e;
                NavigationService.Navigate(new Uri("/NewChallenge.xaml", UriKind.Relative));
            }
        }

        private void NewChallenge_Click(object sender, EventArgs e)
        {
            var photoTask = new PhotoChooserTask();
            photoTask.PixelHeight = 480;
            photoTask.PixelWidth = 480;
            photoTask.ShowCamera = true;
            photoTask.Completed += Complete_PhotoChooserTask;

            photoTask.Show();
        }

        private async void Refresh_Click(object sender, EventArgs e)
        {
            await App.ViewModel.LoadData(true);
        }

        private void Settings_Click(object sender, EventArgs e)
        {

        }
    }
}