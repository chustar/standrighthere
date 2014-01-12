using System;
using standrighthere.Utilities;

namespace standrighthere.DesignData
{
    public class CommentViewModel
    {
        public CommentViewModel()
        {
            User = new UserViewModel();
            CommentText = "some comment text";
            Created = DateTime.Now;
        }

        public UserViewModel User { get; set; }

        public string Username
        {
            get
            {
                return User.Username;
            }
        }

        public string CommentText { get; set; }

        public DateTime Created { get; set; }

        public string CreatedRelative
        {
            get
            {
                return TimeAgo.GetTimeAgo(Created);
            }
        }
    }
}