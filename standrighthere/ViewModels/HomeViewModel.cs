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

        public ObservableCollection<ChallengeViewModel> Challenges { get; set; }

        public bool IsDataLoaded { get; private set; }

        public UserViewModel User { get; set; }

        public bool IsLoading { get; set; }

        public async Task LoadData()
        {
            IsLoading = true;
            var geoposition = await Utilities.GeoLocationHelper.GetLocation();
            var geoPoint = new ParseGeoPoint(geoposition.Coordinate.Latitude, geoposition.Coordinate.Longitude);

            var query = ParseObject.GetQuery("Challenge").Limit(4);
            foreach (var challenge in await query.FindAsync())
            {
                Challenges.Add(new ChallengeViewModel(challenge));
            }

            IsDataLoaded = true;
            IsLoading = false;
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
