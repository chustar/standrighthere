using Parse;
using standrighthere.Utilities;
using System;


namespace standrighthere.DesignData
{
    public partial class ChallengeViewModel
    {
        public ChallengeViewModel()
        {
            User = new UserViewModel();
            Title = "some challenge";
            Image = new Uri("http://farm8.staticflickr.com/7343/11914204134_8de5a28ff7_z.jpg");
            Location = new ParseGeoPoint(34, 34);
            Distance = "3 miles";
            DistanceCount = 5;
            SolvedCount = 12;
            Created = DateTime.Now;
            CommentListViewModel = new CommentListViewModel();
        }

        public UserViewModel User { get; set; }

        public string Title { get; set; }

        public Uri Image { get; set; }

        public ParseGeoPoint Location { get; set; }

        public double DistanceCount { get; set; }

        public string Distance { get; set; }

        public int SolvedCount { get; set; }

        public DateTime Created { get; set; }

        public string CreatedRelative
        {
            get
            {
                return TimeAgo.GetTimeAgo(Created);
            }
        }

        public CommentListViewModel CommentListViewModel { get; set; }
    }
}