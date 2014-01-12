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
using System.ComponentModel;

namespace standrighthere.Controls
{
    public partial class ChallengeListControl : UserControl
    {
        public ChallengeListControl()
        {
            InitializeComponent();
        }

        private async void LongListSelector_ItemRealized(object sender, ItemRealizationEventArgs e)
        {
            if (!DesignerProperties.IsInDesignTool)
            {
                var challengeListViewModel = App.ViewModel.ChallengeListViewModel;
                if (!challengeListViewModel.IsDataLoading && Challenges.ItemsSource != null && Challenges.ItemsSource.Count >= challengeListViewModel.CurrentlyLoaded)
                {
                    if (e.ItemKind == LongListSelectorItemKind.Item)
                    {
                        if ((e.Container.Content as ChallengeViewModel).Equals(Challenges.ItemsSource[Challenges.ItemsSource.Count - challengeListViewModel.CurrentlyLoaded]))
                        {
                            await challengeListViewModel.LoadData(true);
                        }
                    }
                }
            }
        }

        private void Challenges_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as LongListSelector).SelectedItem != null)
            {
                App.CurrentChallenge = (sender as LongListSelector).SelectedItem as ChallengeViewModel;
                App.RootFrame.Navigate(new System.Uri(string.Format("/Views/Challenge.xaml"), System.UriKind.Relative));
                (sender as LongListSelector).SelectedItem = null;
            }
        }
    }

}

namespace Test
{
    public class DesignTimeData
    {
        public FakeChallengeList ChallengeListViewModel { get; set; }


        public DesignTimeData()
        {
            ChallengeListViewModel = new FakeChallengeList();
        }
    }

    public class FakeChallengeList
    {
        public FakeChallengeList()
        {
            Challenges = LoadCats();
        }
        public List<FakeCat> Challenges { get; set; }

        List<FakeCat> LoadCats()
        {
            return new List<FakeCat>
                       {
                           new FakeCat {Title = "Name goes here", Image = "../Assets/fakeCat.png"},
                           new FakeCat {Title = "Name goes here", Image = "../Assets/fakeCat.png"},
                           new FakeCat {Title = "Name goes here", Image = "../Assets/fakeCat.png"},
                           new FakeCat {Title = "Name goes here", Image = "../Assets/fakeCat.png"},
                       };
        }
    }

    public class FakeCat
    {
        public string Title { get; set; }
        public string Image { get; set; }
    }
}