namespace Newse2.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Post> Posts { get; }
        public PageViewModel PageViewModel { get; }
        public IndexViewModel(IEnumerable<Post> posts, PageViewModel viewModel)
        {
            Posts = posts;
            PageViewModel = viewModel;
        }
    }
}
