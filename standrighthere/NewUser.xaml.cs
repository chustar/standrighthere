using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;

using Parse;
using Windows.UI.ViewManagement;
using standrighthere.ViewModels;

namespace standrighthere
{
    public partial class NewUser : PhoneApplicationPage
    {
        public NewUser()
        {
            InitializeComponent();

            DataContext = new NewUserViewModel();

            InputPane.GetForCurrentView().Showing += NewUser_Showing;
        }

        private void NewUser_Showing(InputPane sender, InputPaneVisibilityEventArgs args)
        {
            ScrollView.ScrollToVerticalOffset((PivotControl.SelectedItem as PivotItem).ActualHeight);
        }
    }
}