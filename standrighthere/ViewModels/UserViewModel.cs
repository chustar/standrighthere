using System.Collections.Generic;

using Parse;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using standrighthere.Utilities;
using System.ComponentModel;
using standrighthere.Interfaces;

namespace standrighthere.ViewModels
{
    public partial class UserViewModel : ILoadableViewModel
    {
        public UserViewModel(ParseUser user)
        {
            _user = user;
            var task = LoadData();
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
                return (null == _user ? "" : _user.Username);
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
            if (null == _user) return;
            var userFetchTask = _user.FetchIfNeededAsync();
            var submittedCountTask = (from challenge in ParseObject.GetQuery("Challenge")
                                    where challenge.Get<ParseUser>("user") == _user
                                    select challenge).CountAsync();

            var solvedCountTask = (from challenge in ParseObject.GetQuery("UserChallengeSolved")
                                 where challenge.Get<ParseUser>("user") == _user
                                 select challenge).CountAsync();

            await Task.WhenAll(userFetchTask, submittedCountTask, solvedCountTask);

            _user = userFetchTask.Result;
            _submittedCount = submittedCountTask.Result;
            _solvedCount = solvedCountTask.Result;
            NotifyPropertyChanged("Username");
            NotifyPropertyChanged("SubmittedCount");
            NotifyPropertyChanged("SolvedCount");
        }

        protected ParseUser _user;
    }
}