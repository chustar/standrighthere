using System.Collections.ObjectModel;

namespace standrighthere.DesignData
{
    public partial class CommentListViewModel
    {
        public CommentListViewModel()
        {
            Comments = LoadDesignData();
            CurrentlyLoaded = Comments.Count;
        }

        public ObservableCollection<CommentViewModel> LoadDesignData()
        {

            return new ObservableCollection<CommentViewModel>()
            {
                new CommentViewModel(),
                new CommentViewModel(),
                new CommentViewModel(),
                new CommentViewModel(),
                new CommentViewModel(),
                new CommentViewModel()
            };

        }
        public ObservableCollection<CommentViewModel> Comments { get; set; }

        public int CurrentlyLoaded { get; set; }
    }
}