using Parse;
using standrighthere.Utilities;
using System;

namespace standrighthere.ViewModels
{
    public partial class ChallengeViewModel
    {
        public ChallengeViewModel(ParseObject challengeObject)
        {
            _challengeObject = challengeObject;
        }

        public string Title
        {
            get
            {
                return _challengeObject.Get<string>("title");
            }
        }

        public string Description
        {
            get
            {
                return _challengeObject.Get<string>("description");
            }
        }

        public Uri Image
        {
            get
            {
                return _challengeObject.Get<ParseFile>("image").Url;
            }
        }

        public UserViewModel UserDetails
        {
            get
            {
                return new UserViewModel(_challengeObject.Get<ParseUser>("user"));
            }
        }
        
        public ParseGeoPoint GeoPoint
        {
            get
            {
                return _challengeObject.Get<ParseGeoPoint>("geoPoint");
            }
        }
        
        public DateTime Created
        {
            get
            {
                return _challengeObject.Get<DateTime>("createdAt");
            }
        }

        public string CreatedRelative
        {
            get
            {
                return TimeAgo.GetTimeAgo(_challengeObject.Get<DateTime>("createdAt"));
            }
        }

        private ParseObject _challengeObject;
    }
}