using System.Collections.Generic;

using standrighthere.Models;
using Parse;
using System.Threading.Tasks;

namespace standrighthere.ViewModels
{
    public partial class UserDetailsViewModel
    {
        public ParseObject UserDetails { get; private set; }
        public IEnumerable<ParseObject> SubmittedChallenges { get; private set; }
        public IEnumerable<ParseObject> SolvedChallenges { get; private set; }

        public bool IsDataLoaded { get; private set; }

        public async Task LoadData()
        {
            UserDetails = ParseUser.CurrentUser;
            if (UserDetails != null)
            {
                SolvedChallenges = await (from challenge in ParseObject.GetQuery("Challenges")
                                          where challenge.Get<ParseUser>("user") == ParseUser.CurrentUser
                                          select challenge).FindAsync();

                SubmittedChallenges = await (from userChallenge in ParseObject.GetQuery("UserChallengesSolved")
                                             where userChallenge.Get<ParseUser>("user") == ParseUser.CurrentUser
                                             select userChallenge).FindAsync();
            }

            IsDataLoaded = true;
        }
    }
}