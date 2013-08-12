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
    public partial class ChallengeListViewModel
    {
        public ChallengeListViewModel()
        {
            Challenges = new ObservableCollection<ChallengeViewModel>();
            LoadData();
        }

        public int CurrentlyLoaded { get; set; }

        public ObservableCollection<ChallengeViewModel> Challenges { get; set; }

        public bool IsLoading { get; set; }

        public async Task LoadData(int skipCount = 0)
        {
            IsLoading = true;
            var geoposition = await Utilities.GeoLocationHelper.GetLocation();
            var geoPoint = new ParseGeoPoint(geoposition.Coordinate.Latitude, geoposition.Coordinate.Longitude);
            var query = ParseObject.GetQuery("Challenge");
            query.WhereNear("location", geoPoint).Limit(20).Skip(skipCount);
            foreach (var challenge in await query.FindAsync())
            {
                Challenges.Add(new ChallengeViewModel(challenge));
            }
            CurrentlyLoaded = skipCount + 20;
            IsLoading = false;
        }

    }
}
