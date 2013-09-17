using System.Collections.Generic;

using Parse;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using standrighthere.Utilities;
using System.ComponentModel;
using standrighthere.Interfaces;

namespace standrighthere.ViewModels
{
    public partial class UserViewModel : ILoadableViewModel, INotifyPropertyChanged
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

        private int _solvedCount;
        public int SolvedCount
        {
            get
            {
                var task = LoadData();
                return _solvedCount;
            }
        }

        private int _submittedCount;
        public int SubmittedCount
        {
            get
            {
                var task = LoadData();
                return _submittedCount;
            }
        }
        
        public string CreatedRelative
        {
            get
            {
                return TimeAgo.GetTimeAgo(_user.CreatedAt.Value);
            }
        }
        
        protected async override Task LoadDataImpl(bool forceReload = false)
        {
            _submittedCount = await (from challenge in ParseObject.GetQuery("Challenge")
                                    where challenge.Get<ParseUser>("user") == _user
                                    select challenge).CountAsync();
            NotifyPropertyChanged("SubmittedCount");

            _solvedCount = await (from challenge in ParseObject.GetQuery("UserChallengeSolved")
                                 where challenge.Get<ParseUser>("user").Username == _user.Username
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

        protected ParseUser _user;
    }
}