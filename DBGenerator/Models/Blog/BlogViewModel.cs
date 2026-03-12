namespace DBGenerator.Models.Blog
{
    public class BlogViewModel
    {
        public Post MainPost { get; set; }
        public List<Post> PromoPosts { get; set; }
        public PostPageViewModel Posts { get; set; }
    }
}
