using System.Collections.ObjectModel;

using standrighthere.Models;
using System.Collections.Generic;
using Parse;
using System.Threading.Tasks;

namespace standrighthere.ViewModels
{
    public partial class ChallengeListViewModel
    {
        public ChallengeListViewModel()
        {
        }

        public IEnumerable<ParseObject> NearbyChallenges { get; private set; }

        public bool IsDataLoaded
        {
            get; private set;
        }
        
        public async Task LoadData()
        {
            var query = ParseObject.GetQuery("Challenges");
            query.WhereNear("location", ParseUser.CurrentUser.Get<ParseGeoPoint>("location"));
            query.Limit(20);
            NearbyChallenges = await query.FindAsync();
            IsDataLoaded = true;
        }
    }
}