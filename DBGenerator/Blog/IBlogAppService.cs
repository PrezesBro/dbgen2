using DBGenerator.Models.Blog;

namespace DBGenerator.Blog
{
    public interface IBlogAppService
    {
        Task<BlogViewModel> GetBlogVM(int page);
        Task<Post> GetPostWithElements(string post_name);
    }
}