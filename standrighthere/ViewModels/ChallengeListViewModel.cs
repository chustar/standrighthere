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
using standrighthere.Interfaces;

namespace standrighthere.ViewModels
{
    public partial class ChallengeListViewModel : ILoadableViewModel
    {
        public ChallengeListViewModel()
        {
            Challenges = new ObservableCollection<ChallengeViewModel>();
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
        /// Begins loading new challenges asynchronously.
        /// </summary>
        /// <param name="skipCount">The number of challenges to skip.</param>
        /// <returns>An awaitable task.</returns>
        protected async override Task LoadDataImpl(bool forceReload = false)
        {
            var geoposition = await Utilities.GeoLocationHelper.GetLocation();
            var geoPoint = new ParseGeoPoint(geoposition.Coordinate.Latitude, geoposition.Coordinate.Longitude);
            var query = ParseObject.GetQuery("Challenge");
            query.WhereNear("location", geoPoint).Limit(20).Skip(CurrentlyLoaded);

            List<Task> challengeTasks = new List<Task>();
            foreach (var challenge in await query.FindAsync())
            {
                var challengeViewModel = new ChallengeViewModel(challenge);
                Challenges.Add(challengeViewModel);
                challengeTasks.Add(challengeViewModel.LoadData());
            }
            await Task.WhenAll(challengeTasks.ToArray());

            CurrentlyLoaded += 20;
        }
    }
}
