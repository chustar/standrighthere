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
