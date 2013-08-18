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
            var task = LoadData();
        }

        /// <summary>
        /// An observable collection of challenges.
        /// </summary>
        public ObservableCollection<ChallengeViewModel> Challenges { get; set; }

        /// <summary>
        /// The number of challenges that have been loaded.
        /// </summary>
        public int CurrentlyLoaded { get; set; }

        /// <summary>
        /// True when the VM is actively loading challenges.
        /// </summary>
        public bool IsLoading { get; set; }

        /// <summary>
        /// Begins loading new challenges asynchronously.
        /// </summary>
        /// <param name="skipCount">The number of challenges to skip.</param>
        /// <returns>An awaitable task.</returns>
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
