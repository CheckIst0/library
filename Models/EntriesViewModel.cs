namespace CourseWork.Models
{
    public class EntriesViewModel
    {
        public IEnumerable<EntryHistory> EntryHistories { get; }
        public PageViewModel PageViewModel { get; }

        public EntriesViewModel(IEnumerable<EntryHistory> entries, PageViewModel viewModel)
        {
            EntryHistories = entries;
            PageViewModel = viewModel;
        }
    }
}