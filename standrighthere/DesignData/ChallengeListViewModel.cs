using System.Collections.ObjectModel;

namespace standrighthere.DesignData
{
    public partial class ChallengeListViewModel
    {
        public ChallengeListViewModel()
        {
            Challenges = LoadDesignData();
            CurrentlyLoaded = Challenges.Count;
        }

        public ObservableCollection<ChallengeViewModel> Challenges { get; set; }

        public int CurrentlyLoaded { get; set; }

        ObservableCollection<ChallengeViewModel> LoadDesignData()
        {
            return new ObservableCollection<ChallengeViewModel>()
            {
                new ChallengeViewModel(),
                new ChallengeViewModel(),
                new ChallengeViewModel(),
                new ChallengeViewModel(),
                new ChallengeViewModel(),
                new ChallengeViewModel()
            };
        }
    }
}
