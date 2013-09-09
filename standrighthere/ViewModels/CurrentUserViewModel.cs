using System.Collections.Generic;

using Parse;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using standrighthere.Utilities;
using System.ComponentModel;
using standrighthere.Interfaces;

namespace standrighthere.ViewModels
{
    public partial class CurrentUserViewModel : UserViewModel
    {
        public CurrentUserViewModel(ParseUser user) : base(user)
        {
        }

        public bool IsLoggedIn
        {
            get
            {
                return null != _user;
            }
        }

        public string Username
        {
            get
            {
                if (null == _user)
                {
                    return "";
                }
                return base.Username;
            }
        }
    }
}