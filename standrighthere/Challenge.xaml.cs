using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Data;
using standrighthere.ViewModels;
using Parse;

namespace standrighthere
{
    public partial class Challenge : PhoneApplicationPage
    {
        public Challenge()
        {
            InitializeComponent();

            DataContext = App.CurrentChallenge;
        }

        private void Pivot_LoadingPivotItem(object sender, PivotItemEventArgs e)
        {
            if (e.Item.Content != null)
            {
                return;
            }

            if (e.Item == (sender as Pivot).Items[2])
            {
                var longListSelector = new LongListSelector();
                longListSelector.ItemTemplate = Application.Current.Resources["ListBoxItemTemplate"] as DataTemplate;
                longListSelector.SelectionChanged += new SelectionChangedEventHandler((Object obj, SelectionChangedEventArgs args) => {

                });
                longListSelector.DataContext = DataContext;
                var binding = new Binding("Comments") { Source = (DataContext as ChallengeViewModel).Comments };
                longListSelector.SetBinding(LongListSelector.ItemsSourceProperty, binding);
                e.Item.Content = longListSelector;
            }
        }

        private async void Challenges_ItemRealized(object sender, ItemRealizationEventArgs e)
        {
            var challengeViewModel = DataContext as ChallengeViewModel;
            if (!challengeViewModel.IsLoadingComments && Challenges.ItemsSource != null && Challenges.ItemsSource.Count >= challengeViewModel.CurrentlyLoadedComments)
            {
                if (e.ItemKind == LongListSelectorItemKind.Item)
                {
                    if ((e.Container.Content as ChallengeViewModel).Equals(Challenges.ItemsSource[Challenges.ItemsSource.Count - challengeViewModel.CurrentlyLoadedComments]))
                    {
                        await challengeViewModel.LoadComments(challengeViewModel.CurrentlyLoadedComments);
                    }
                }
            }
        }

        private async void NewCommentText_ActionIconTapped(object sender, EventArgs e)
        {
            if (NewCommentText.Text.Trim() != "")
            {
                var comment = new ParseObject("Comment")
                {
                    {"user", ParseUser.CurrentUser},
                    {"title", NewCommentText.Text.Trim()},
                };
                await comment.SaveAsync();

                (DataContext as ChallengeViewModel).Comments.Add(new CommentViewModel(comment));
                (DataContext as ChallengeViewModel).CurrentlyLoadedComments++;
                NewCommentText.Text = "";
                this.Focus();
            }
        }
    }
}