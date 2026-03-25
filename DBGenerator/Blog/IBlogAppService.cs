using DBGenerator.Models.Blog;

namespace DBGenerator.Blog
{
    public interface IBlogAppService
    {
        Task<BlogViewModel> GetBlogVM(int page);
        Task<Post> GetPostWithElements(string post_name);
        Task<List<Post>> GetPostsOrderedByPublishDateAsync();
        Task UpdatePost(Post model); 
        Task UpdatePostElements(List<PostElement> elements); 
        Task<EditPostViewModel> GetEditPostVM(string post_name);
        Task<Post> GetPostWithMetasById(int postId);
        Task<Post> UpdateMetas(Post post);
    }
}