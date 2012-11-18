using System.Collections.ObjectModel;

namespace standrighthere.ViewModels
{
    public class ChallengeViewModel
    {
        public ChallengeViewModel()
        {
            Challenges = new ObservableCollection<Challenge>();
        }

        public ObservableCollection<Challenge> Challenges { get; private set; }
    }
}