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
        private ParseObject _challengeObject;
        
        public ChallengeViewModel(ParseObject challengeObject)
        {
            _challengeObject = challengeObject;
        }
        
        public UserViewModel User
        {
            get
            {
                return new UserViewModel(_challengeObject.Get<ParseUser>("user"));
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
    }
}