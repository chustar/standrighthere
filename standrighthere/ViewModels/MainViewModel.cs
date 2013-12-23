using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Parse;
using Windows.Devices.Geolocation;
using System.Windows;
using System.Net.Http;
using System.ComponentModel;
using standrighthere.Interfaces;
using System.Windows.Navigation;

namespace standrighthere.ViewModels
{
    public partial class MainViewModel : ILoadableViewModel
    {
        public MainViewModel()
        {
            User = new UserViewModel(ParseUser.CurrentUser);
            ChallengeListViewModel = new ChallengeListViewModel();
        }

        public UserViewModel User { get; private set; }

        private SimpleCommand _userCommand;
        public SimpleCommand UserCommand
        {
            get
            {
                if (_userCommand == null)
                {
                    _userCommand = new SimpleCommand(
                        (object parameter) =>
                        {
                            App.RootFrame.Navigate(new Uri("/User.xaml", UriKind.Relative));
                        });
                }
                return _userCommand;
            }
        }
        
        private SimpleCommand _joinCommand;
        public SimpleCommand JoinCommand
        {
            get
            {
                if (_joinCommand == null)
                {
                    _joinCommand = new SimpleCommand(
                        (object parameter) =>
                        {
                            App.RootFrame.Navigate(new Uri("/Views/NewUser.xaml", UriKind.Relative));
                        });
                }
                return _joinCommand;
            }
        }

        public ChallengeListViewModel ChallengeListViewModel { get; private set; }

        protected async override Task LoadDataImpl(bool forceReload = false)
        {
            var userTask = User.LoadData(forceReload);
            var challengeTask = ChallengeListViewModel.LoadData();
            await Task.WhenAll(userTask, challengeTask);
        }
    }
}
