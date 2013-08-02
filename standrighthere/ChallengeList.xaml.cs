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
using System.Collections.ObjectModel;
using Parse;

namespace standrighthere
{
    public partial class ChallengeList : PhoneApplicationPage
    {
        public ChallengeList()
        {
            InitializeComponent();

            LoadData();
        }
        
        public ObservableCollection<ChallengeViewModel> Challenges { get; private set; }

        public bool IsDataLoaded { get; private set; }

        public async void LoadData()
        {
            var query = ParseObject.GetQuery("Challenges");
            query.WhereNear("location", ParseUser.CurrentUser.Get<ParseGeoPoint>("location"));
            query.Limit(20);
            var challenges = await query.FindAsync();
            foreach (var challenge in challenges)
            {
                Challenges.Add(new ChallengeViewModel(challenge));
            }

            IsDataLoaded = true;
        }
    }
}