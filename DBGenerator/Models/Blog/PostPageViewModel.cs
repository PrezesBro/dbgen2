namespace DBGenerator.Models.Blog
{
    public class PostPageViewModel
    {
        public List<Post> Posts { get; set; }
        public int Page {  get; set; }
        public int TotalPages { get; set; }
        public List<int> PageList { get; set; } = new List<int>();
    }
}
