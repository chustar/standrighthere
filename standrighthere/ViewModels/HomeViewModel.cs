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

namespace standrighthere.ViewModels
{
    public partial class HomeViewModel : INotifyPropertyChanged
    {
        public HomeViewModel()
        {
            Challenges = new ObservableCollection<ChallengeViewModel>();
        }

        void User_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SolvedCount")
            {
                NotifyPropertyChanged("UserSolvedCount");
            }
            else if (e.PropertyName == "SubmittedCount")
            {
                NotifyPropertyChanged("UserSubmittedCount");
            }
        }

        public ObservableCollection<ChallengeViewModel> Challenges { get; set; }

        public bool IsDataLoaded
        {
            get; private set;
        }

        public UserViewModel _user;
        public UserViewModel User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                _user.PropertyChanged += User_PropertyChanged;
            }
        }

        public string Username
        {

            get
            {
                return User.Username;
            }
        }

        public int UserSolvedCount
        {
            get
            {
                return User.SolvedCount;
            }
        }

        public int UserSubmittedCount
        {
            get
            {
                return User.SubmittedCount;
            }
        }
        
        public string UserCreatedRelative
        {
            get
            {
                return User.CreatedRelative;
            }
        }

        public async Task LoadData()
        {
            var geoposition = await Utilities.GeoLocationHelper.GetLocation();
            var geoPoint = new ParseGeoPoint(geoposition.Coordinate.Latitude, geoposition.Coordinate.Longitude);
            var query = ParseObject.GetQuery("Challenge");
            var challenges = await query.WhereNear("location", geoPoint).Limit(4).FindAsync();
            foreach (var challenge in challenges)
            {
                Challenges.Add(new ChallengeViewModel(challenge));
            }

            IsDataLoaded = true;
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
