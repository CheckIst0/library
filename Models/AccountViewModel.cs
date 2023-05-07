namespace CourseWork.Models
{
    public class AccountViewModel
    {
        public User User { get; }
        public IEnumerable<IssueHistory> IssueHistories { get; }
        public PageViewModel PageViewModel { get; }

        public AccountViewModel(IEnumerable<IssueHistory> issues, PageViewModel viewModel, User user)
        {
            User = user;
            IssueHistories = issues;
            PageViewModel = viewModel;
        }
    }
}