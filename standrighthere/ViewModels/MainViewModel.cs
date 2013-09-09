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
    public partial class MainViewModel : ILoadableViewModel, INotifyPropertyChanged
    {
        public MainViewModel()
        {
            User = new CurrentUserViewModel(ParseUser.CurrentUser);
            ChallengeListViewModel = new ChallengeListViewModel();
        }

        public CurrentUserViewModel User { get; private set; }

        private CustomCommand _userCommand;
        public CustomCommand UserCommand
        {
            get
            {
                if (_userCommand == null)
                {
                    _userCommand = new CustomCommand(
                        (object parameter) =>
                        {
                            App.RootFrame.Navigate(new Uri("/User.xaml", UriKind.Relative));
                        },
                        (object parameter) =>
                        {
                            return true == User.IsLoggedIn;
                        });
                }
                return _userCommand;
            }
        }
        
        private CustomCommand _joinCommand;
        public CustomCommand JoinCommand
        {
            get
            {
                if (_joinCommand == null)
                {
                    _joinCommand = new CustomCommand(
                        (object parameter) =>
                        {
                            App.RootFrame.Navigate(new Uri("/NewUser.xaml", UriKind.Relative));
                        },
                        (object parameter) =>
                        {
                            return false == User.IsLoggedIn;
                        });
                }
                return _joinCommand;
            }
        }

        public ChallengeListViewModel ChallengeListViewModel { get; private set; }

        protected async override Task LoadDataImpl(bool forceReload = false)
        {
            IsDataLoading = true;
            var userTask = User.LoadData(forceReload);
            var challengeTask = ChallengeListViewModel.LoadData();
            await Task.WhenAll(userTask, challengeTask);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
