using Parse;
using standrighthere.Utilities;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace standrighthere.ViewModels
{
    public partial class ChallengeViewModel
    {
        public ChallengeViewModel(ParseObject challengeObject)
        {
            _challengeObject = challengeObject;
        }

        private UserViewModel _user;
        public UserViewModel User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserViewModel(_challengeObject.Get<ParseUser>("user"));
                }
                return _user;
            }
        }

        public string Username
        {
            get
            {
                return User.Username;
            }
        }

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
                return TimeAgo.GetTimeAgo(_challengeObject.CreatedAt.Value);
            }
        }
        
        private ParseObject _challengeObject;
        
        async Task FetchData()
        {
            SolvedCount = await (from challenge in ParseObject.GetQuery("Challenges")
                                 where challenge.Get<ParseUser>("user") == _challengeObject.Get<ParseUser>("user")
                                 select challenge).CountAsync();
            NotifyPropertyChanged("SolvedCount");
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