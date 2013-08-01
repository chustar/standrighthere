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

namespace standrighthere
{
    public partial class NewUser : PhoneApplicationPage
    {
        public NewUser()
        {
            InitializeComponent();
        }

        private async void Register(object sender, RoutedEventArgs args)
        {
            try
            {
                var user = new ParseUser()
                {
                    Email = RegisterEmail.Text,
                    Username = RegisterUsername.Text,
                    Password = RegisterPassword.Password
                };

                await user.SignUpAsync();
                var userDetails = new ParseObject("UserDetails")
                {
                    {"user", user}
                };
                await userDetails.SaveAsync();
                await App.UserDetails.LoadData();

                NavigationService.Navigate(new Uri("/UserDetailsManager.xaml", UriKind.Relative));
            }
            catch (Exception)
            {
                MessageBox.Show("That username has already been taken. Please try another one.");
            }
        }

        private async void Login(object sender, RoutedEventArgs args)
        {
            try
            {
                await ParseUser.LogInAsync(LoginUsername.Text, LoginPassword.Password);
                await App.UserDetails.LoadData();

                NavigationService.Navigate(new Uri("/UserDetailsManager.xaml", UriKind.Relative));
            }
            catch (Exception)
            {
                MessageBox.Show("The password is incorrect.");
            }
        }
    }
}