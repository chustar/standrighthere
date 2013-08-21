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

namespace standrighthere.ViewModels
{
    public partial class ChallengeViewModel : INotifyPropertyChanged
    {
        public ChallengeViewModel(ParseObject challengeObject)
        {
            _challengeObject = challengeObject;
            Comments = new ObservableCollection<CommentViewModel>();

            var task = LoadData();
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
        
        public string Distance
        {
            get
            {
                return Location.RelativeDistanceTo(GeoLocationHelper.CachedLocation.ToParseGeoPoint());
            }
        }

        public int SolvedCount { get; set; }
        
        public ObservableCollection<CommentViewModel> Comments { get; set; }
        
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
        
        public bool IsLoadingComments { get; set; }

        public int CurrentlyLoadedComments { get; set; }

        /// <summary>
        /// Load the ChallengeViewModels' data.
        /// </summary>
        /// <returns>An awaitable task.</returns>
        public async Task LoadData()
        {
            User = new UserViewModel(await _challengeObject.Get<ParseUser>("user").FetchAsync() as ParseUser);
            NotifyPropertyChanged("User");
            NotifyPropertyChanged("Username");

            SolvedCount = await (from challenge in ParseObject.GetQuery("Challenge")
                                 where challenge.Get<ParseUser>("user") == _challengeObject.Get<ParseUser>("user")
                                 select challenge).CountAsync();
            NotifyPropertyChanged("SolvedCount");

            var task = LoadComments();
        }

        /// <summary>
        /// Loads the challenges comments asynchronously.
        /// </summary>
        /// <param name="skipCount">How many comments to skip. Defaults to 0.</param>
        /// <returns>And awaitable task.</returns>
        public async Task LoadComments(int skipCount = 0)
        {
            IsLoadingComments = true;
            var query = ParseObject.GetQuery("Comment");
            query.Limit(20).Skip(skipCount);
            foreach (var comment in await query.FindAsync())
            {
                Comments.Add(new CommentViewModel(comment));
            }
            CurrentlyLoadedComments = skipCount + 20;
            IsLoadingComments = false;
        }
        
        private ParseObject _challengeObject;

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