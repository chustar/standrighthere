using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Parse;
using Windows.Devices.Geolocation;
using System.Windows;

namespace standrighthere.ViewModels
{
    public partial class HomeViewModel
    {
        private ObservableCollection<ChallengeViewModel> Challenges { get; set; }

        public bool IsDataLoaded
        {
            get; private set;
        }

        public async void LoadData()
        {
            var geoposition = await Utilities.GeoLocationHelper.GetLocation();
            var geoPoint = new ParseGeoPoint(geoposition.Coordinate.Latitude, geoposition.Coordinate.Longitude);
            var query = ParseObject.GetQuery("Challenges");
            query.WhereNear("location", geoPoint);
            query.Limit(20);
            foreach (ParseObject challenge in await query.FindAsync())
            {
                Challenges.Add(new ChallengeViewModel(challenge));
            }

            IsDataLoaded = true;
        }
    }
}
