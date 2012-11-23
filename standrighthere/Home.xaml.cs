using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.WindowsAzure.MobileServices;
using standrighthere.ViewModels;

namespace standrighthere
{
    public partial class Home : PhoneApplicationPage
    {
        // Constructor
        public Home()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = HomeViewModel;
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!HomeViewModel.IsDataLoaded)
            {
                HomeViewModel.LoadData();
            }
        }

        private void viewfinderCanvas_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {

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
    }
}