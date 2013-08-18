using System.Collections.Generic;

using Parse;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using standrighthere.Utilities;
using System.ComponentModel;

namespace standrighthere.ViewModels
{
    public partial class UserViewModel : INotifyPropertyChanged
    {
        public UserViewModel(ParseUser user)
        {
            _user = user;
            var task = LoadData();
        }

        public string Username
        {
            get
            {
                return _user.Username;
            }
        }

        public int SolvedCount { get; set; }

        public int SubmittedCount { get; set; }
        
        public string CreatedRelative
        {
            get
            {
                return TimeAgo.GetTimeAgo(_user.CreatedAt.Value);
            }
        }

        async Task LoadData()
        {
            SolvedCount = await (from challenge in ParseObject.GetQuery("Challenges")
                                 where challenge.Get<ParseUser>("user") == _user
                                 select challenge).CountAsync();
            NotifyPropertyChanged("SolvedCount");

            SubmittedCount = await (from challenge in ParseObject.GetQuery("UserChallengesSolved")
                                    where challenge.Get<ParseUser>("user") == _user
                                    select challenge).CountAsync();
            NotifyPropertyChanged("SubmittedCount");
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

        private ParseUser _user;
    }
}