using Microsoft.WindowsAzure.MobileServices;
using System.Collections.ObjectModel;

namespace standrighthere.ViewModels
{
    public partial class ChallengeListViewModel
    {
        public ChallengeListViewModel()
        {
#if DESIGN
            _WireDesignerData();
#endif
        }

        public ChallengeViewModel NearbyChallenges { get; private set; }
        public ChallengeViewModel NewChallenges { get; private set; }
        public ChallengeViewModel TopChallenges { get; private set; }

        public bool IsDataLoaded
        {
            get; private set;
        }
        
        public void LoadData()
        {
            NearbyChallenges.LoadData();
            NewChallenges.LoadData();
            TopChallenges.LoadData();

            IsDataLoaded = true;
        }

    }
}