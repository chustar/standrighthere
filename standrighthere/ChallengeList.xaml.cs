using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using standrighthere.ViewModels;

namespace standrighthere
{
    public partial class ChallengeList : PhoneApplicationPage
    {
        public ChallengeList()
        {
            InitializeComponent();
            DataContext = ChallengeListViewModel;
        }
        
        private ChallengeListViewModel challengeListViewModel;
        private ChallengeListViewModel ChallengeListViewModel
        {
            get
            {
                if (challengeListViewModel == null)
                {
                    challengeListViewModel = new ChallengeListViewModel();
                }
                return challengeListViewModel;
            }
        }
    }
}