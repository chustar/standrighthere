using System.Collections.ObjectModel;
using System.ComponentModel;

namespace standrighthere.ViewModels
{
    public partial class ChallengeListViewModel
    {
#if DESIGN
        private void _WireDesignerData()
        {
            if (!DesignerProperties.IsInDesignTool) return;

            NearbyChallenges = new ChallengeViewModel();
            NewChallenges = new ChallengeViewModel();
            TopChallenges = new ChallengeViewModel();
        }
#endif
    }
}
