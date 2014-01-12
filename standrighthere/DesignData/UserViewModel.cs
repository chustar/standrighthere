using System;
using standrighthere.Utilities;

namespace standrighthere.DesignData
{
    public partial class UserViewModel
    {
        public UserViewModel()
        {
            IsLoggedIn = true;
            Username = "chustar";
            SolvedCount = 23;
            SubmittedCount = 42;
            Created = DateTime.Now;
        }

        public bool IsLoggedIn { get; set; }

        public string Username { get; set; }

        public int SolvedCount{ get; set;}

        public int SubmittedCount { get; set; }

        public DateTime Created { get; set; }
        public string CreatedRelative { get { return TimeAgo.GetTimeAgo(Created); } }
    }
}