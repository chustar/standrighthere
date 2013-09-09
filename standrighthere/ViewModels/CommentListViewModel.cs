using Parse;
using standrighthere.Utilities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

using standrighthere.Interfaces;

namespace standrighthere.ViewModels
{
    public partial class CommentListViewModel : ILoadableViewModel
    {
        public CommentListViewModel(ParseObject challengeObject)
        {
            Comments = new ObservableCollection<CommentViewModel>();
        }

        public ObservableCollection<CommentViewModel> Comments { get; set; }

        /// <summary>
        /// The number of challenges that have been loaded.
        /// </summary>
        public int CurrentlyLoaded { get; set; }

        /// <summary>
        /// Load the ChallengeViewModels' data.
        /// </summary>
        /// <returns>An awaitable task.</returns>
        protected async override Task LoadDataImpl(bool forceReload = false)
        {
            var query = ParseObject.GetQuery("Comment");
            query.Limit(20).Skip(20);
            foreach (var comment in await query.FindAsync())
            {
                Comments.Add(new CommentViewModel(comment));
            }
            CurrentlyLoaded += 20;
        }
    }
}