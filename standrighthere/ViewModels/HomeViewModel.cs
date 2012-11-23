using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace standrighthere.ViewModels
{
    public partial class HomeViewModel
    {
        public HomeViewModel()
        {
#if DESIGN
            _WireDesignerData();
#endif
        }

        private MobileServiceCollectionView<Challenge> Challenges { get; set; }

        private IMobileServiceTable<Challenge> challengeTable = App.MobileService.GetTable<Challenge>();

        public bool IsDataLoaded
        {
            get; private set;
        }

        public void LoadData()
        {
            Challenges = challengeTable.ToCollectionView();
            IsDataLoaded = true;
        }

    }
}
