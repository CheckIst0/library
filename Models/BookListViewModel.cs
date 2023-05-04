namespace CourseWork.Models
{
    public class BookListViewModel
    {
        public IEnumerable<Book> Books { get; }
        public PageViewModel PageViewModel { get; }

        public BookListViewModel(IEnumerable<Book> books, PageViewModel viewModel) {
            Books = books;
            PageViewModel = viewModel;
        }
    }
}