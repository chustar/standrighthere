using System.Collections.ObjectModel;

namespace standrighthere.ViewModels
{
    public partial class ChallengeViewModel
    {
        public ChallengeViewModel()
        {
            Challenges = new ObservableCollection<Challenge>();

#if DESIGN
            _WireDesignerData();
#endif
        }

        public ObservableCollection<Challenge> Challenges { get; private set; }
    }
}