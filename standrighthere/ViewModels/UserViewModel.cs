using System.Collections.Generic;

using Parse;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using standrighthere.Utilities;

namespace standrighthere.ViewModels
{
    public partial class UserViewModel
    {
        public UserViewModel(ParseUser user)
        {
            _user = user;
        }

        public string Username
        {
            get
            {
                return _user.Username;
            }
        }

        public int SolvedCount
        {
            get
            {
                return (from challenge in ParseObject.GetQuery("Challenges")
                        where challenge.Get<ParseUser>("user") == _user
                        select challenge).CountAsync().Result;

            }
        }
        
        public int SubmittedCount
        {
            get
            {
                return (from challenge in ParseObject.GetQuery("UserChallengesSolved")
                        where challenge.Get<ParseUser>("user") == _user
                        select challenge).CountAsync().Result;
                
            }
        }
        
        public string CreatedRelative
        {
            get
            {
                return TimeAgo.GetTimeAgo(_user.CreatedAt.Value);
                
            }
        }
/*
        public ObservableCollection<ChallengeViewModel> SolvedChallenges { get; private set; }

        public ObservableCollection<ChallengeViewModel> SubmittedChallenges { get; private set; }

        public bool IsDataLoaded { get; private set; }

        public async Task LoadData()
        {
            var solvedChallenges = await (from challenge in ParseObject.GetQuery("Challenges")
                                      where challenge.Get<ParseUser>("user") == _user
                                      select challenge).FindAsync();
            foreach (var challenge in solvedChallenges)
            {
                SolvedChallenges.Add(new ChallengeViewModel(challenge));
            }

            var submittedChallenges = await (from userChallenge in ParseObject.GetQuery("UserChallengesSolved")
                                         where userChallenge.Get<ParseUser>("user") == _user
                                         select userChallenge).FindAsync();
            foreach (var challenge in submittedChallenges)
            {
                SubmittedChallenges.Add(new ChallengeViewModel(challenge));
            }
            IsDataLoaded = true;
        }
*/
        private ParseUser _user;
    }
}