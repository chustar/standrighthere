using Microsoft.Phone.Controls;
using Parse;
using standrighthere.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Windows.Devices.Geolocation;

namespace standrighthere.Views
{
    public partial class Challenge : PhoneApplicationPage
    {
        public Challenge()
        {
            InitializeComponent();

            DataContext = App.CurrentChallenge;

            var task = LoadLocation();
        }

        /// <summary>
        /// Sets the maps center to the current location.
        /// </summary>
        /// <returns>An awaitable task.</returns>
        private async Task LoadLocation()
        {
            Map.Center = (await Utilities.GeoLocationHelper.GetLocation()).Coordinate.ToGeoCoordinate();
        }

        private async void Pivot_LoadingPivotItem(object sender, PivotItemEventArgs e)
        {
            if (e.Item.Content != null)
            {
                return;
            }

            if (e.Item == (sender as Pivot).Items[2])
            {
                await (DataContext as ChallengeViewModel).CommentListViewModel.LoadData();
            }
        }

        private async void Challenges_ItemRealized(object sender, ItemRealizationEventArgs e)
        {
            var challengeViewModel = DataContext as ChallengeViewModel;
            if (Challenges.ItemsSource != null && Challenges.ItemsSource.Count >= challengeViewModel.CommentListViewModel.CurrentlyLoaded)
            {
                if (e.ItemKind == LongListSelectorItemKind.Item)
                {
                    if ((e.Container.Content as ChallengeViewModel).Equals(Challenges.ItemsSource[Challenges.ItemsSource.Count]))
                    {
                        await challengeViewModel.CommentListViewModel.LoadData(true);
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
                    {"commentText", NewCommentText.Text.Trim()},
                };
                await comment.SaveAsync();

                (DataContext as ChallengeViewModel).CommentListViewModel.Comments.Add(new CommentViewModel(comment));
                (DataContext as ChallengeViewModel).CommentListViewModel.CurrentlyLoaded++;
                NewCommentText.Text = "";
                this.Focus();
            }
        }
    }
}