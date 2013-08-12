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

namespace standrighthere.ViewModels
{
    public partial class HomeViewModel
    {
        public HomeViewModel()
        {
            Challenges = new ObservableCollection<ChallengeViewModel>();
        }

        public ObservableCollection<ChallengeViewModel> Challenges { get; set; }

        public bool IsDataLoaded
        {
            get; private set;
        }

        public async Task LoadData()
        {
            var geoposition = await Utilities.GeoLocationHelper.GetLocation();
            var geoPoint = new ParseGeoPoint(geoposition.Coordinate.Latitude, geoposition.Coordinate.Longitude);
            var query = ParseObject.GetQuery("Challenge");
            query.WhereNear("location", geoPoint).Limit(4);
            foreach (var challenge in await query.FindAsync())
            {
                Challenges.Add(new ChallengeViewModel(challenge));
            }

            IsDataLoaded = true;
        }
    }
}
