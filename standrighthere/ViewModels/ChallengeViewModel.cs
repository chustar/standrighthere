﻿using Microsoft.WindowsAzure.MobileServices;
using System.Collections.ObjectModel;

namespace standrighthere.ViewModels
{
    public partial class ChallengeViewModel
    {
        public ChallengeViewModel()
        {
        }

        public MobileServiceCollectionView<Challenge> Challenges { get; private set; }
        private IMobileServiceTable<Challenge> challengeTable = App.MobileService.GetTable<Challenge>();

        internal void LoadData()
        {
            Challenges = challengeTable.ToCollectionView();
        }
    }
}