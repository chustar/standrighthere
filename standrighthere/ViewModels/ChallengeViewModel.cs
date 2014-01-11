using Parse;
using standrighthere.Utilities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

using standrighthere.Interfaces;

namespace standrighthere.ViewModels
{
    public partial class ChallengeViewModel : ILoadableViewModel
    {
        public ChallengeViewModel(ParseObject challengeObject)
        {
            _challengeObject = challengeObject;
        }

        public UserViewModel User { get; set; }

        public string Title
        {
            get
            {
                return _challengeObject.Get<string>("title");
            }
        }

        public Uri Image
        {
            get
            {
                return _challengeObject.Get<ParseFile>("image").Url;
            }
        }

        public ParseGeoPoint Location
        {
            get
            {
                return _challengeObject.Get<ParseGeoPoint>("geoPoint");
            }
        }
        
        public double DistanceCount
        {
            get
            {
                return GeoLocationHelper.CachedLocation.ToParseGeoPoint().DistanceTo(Location).Miles;
            }
        }
        public string Distance
        {
            get
            {
                return Location.RelativeDistanceTo(GeoLocationHelper.CachedLocation.ToParseGeoPoint());
            }
        }

        public int SolvedCount { get; set; }

        public DateTime Created
        {
            get
            {
                return _challengeObject.CreatedAt.Value;
            }
        }

        public string CreatedRelative
        {
            get
            {
                return TimeAgo.GetTimeAgo(Created);
            }
        }

        public CommentListViewModel _commentListViewModel;
        public CommentListViewModel CommentListViewModel
        {
            get
            {
                var task = _commentListViewModel.LoadData();
                return _commentListViewModel;
            }
        }
        
        /// <summary>
        /// Load the ChallengeViewModels' data.
        /// </summary>
        /// <returns>An awaitable task.</returns>
        protected async override Task LoadDataImpl(bool forceReload = false)
        {
            IsDataLoading = true;

            User = new UserViewModel(_challengeObject.Get<ParseUser>("user") as ParseUser);
            await User.LoadData();//true);
            SolvedCount = await (from challenge in ParseObject.GetQuery("UserChallengesSolved")
                                   where challenge.Get<ParseUser>("user") == _challengeObject.Get<ParseUser>("user")
                                   select challenge).CountAsync();

            NotifyPropertyChanged("User");
            NotifyPropertyChanged("SolvedCount");

            IsDataLoading = false;
            IsDataLoaded = true;
        }

        private ParseObject _challengeObject;
    }
}