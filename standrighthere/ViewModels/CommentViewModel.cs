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
    public partial class CommentViewModel
    {
        public CommentViewModel(ParseObject commentObject)
        {
            _commentObject = commentObject;
        }

        private UserViewModel _user;
        public UserViewModel User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserViewModel(_commentObject.Get<ParseUser>("user"));
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

        public string CommentText
        {
            get
            {
                return _commentObject.Get<string>("commentText");
            }
        }

        public DateTime Created
        {
            get
            {
                return _commentObject.CreatedAt.Value;
            }
        }

        public string CreatedRelative
        {
            get
            {
                return TimeAgo.GetTimeAgo(Created);
            }
        }
        
        private ParseObject _commentObject;
    }
}