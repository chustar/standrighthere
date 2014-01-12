namespace standrighthere.DesignData
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            User = new UserViewModel();
            ChallengeListViewModel = new ChallengeListViewModel();
        }

        public UserViewModel User { get; set; }

        public ChallengeListViewModel ChallengeListViewModel { get; set; }
    }
}
