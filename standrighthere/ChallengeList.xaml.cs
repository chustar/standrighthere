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
using System.Windows.Data;

namespace standrighthere
{
    public partial class ChallengeList : PhoneApplicationPage
    {
        public ChallengeList()
        {
            InitializeComponent();

            DataContext = _viewModel;
        }

        private ChallengeListViewModel _viewModel = new ChallengeListViewModel();
        private async void LongListSelector_ItemRealized(object sender, ItemRealizationEventArgs e)
        {
            if (!_viewModel.IsLoading && Challenges.ItemsSource != null && Challenges.ItemsSource.Count >= _viewModel.CurrentlyLoaded)
            {
                if (e.ItemKind == LongListSelectorItemKind.Item)
                {
                    if ((e.Container.Content as ChallengeViewModel).Equals(Challenges.ItemsSource[Challenges.ItemsSource.Count - _viewModel.CurrentlyLoaded]))
                    {
                        await _viewModel.LoadData(_viewModel.CurrentlyLoaded);
                    }
                }
            }
        }

        private void Challenges_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as LongListSelector).SelectedItem != null)
            {
                App.CurrentChallenge = (sender as LongListSelector).SelectedItem as ChallengeViewModel;
                NavigationService.Navigate(new System.Uri(string.Format("/Challenge.xaml"), System.UriKind.Relative));
                (sender as LongListSelector).SelectedItem = null;
            }
        }
    }
}