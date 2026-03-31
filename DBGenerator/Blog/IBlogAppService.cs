using DBGenerator.Models.Blog;

namespace DBGenerator.Blog
{
    public interface IBlogAppService
    {
        Task<BlogViewModel> GetBlogVM(int page);
        Task<Post> GetPostWithElements(string post_name);
        Task<List<Post>> GetPostsOrderedByPublishDateAsync(int page, int size);
        Task UpdatePost(Post model); 
        Task UpdatePostElements(List<PostElement> elements); 
        Task<Post> GetPostWithMetasById(int postId);
        Task<Post> UpdateMetas(Post post);
        Task ClonePost(int id);
        Task DeletePost(int id);
        Task<EditPostViewModel> GetEditPostVM(int id);
        Task<PostPageViewModel> GetPostPageVM(int page);
        Task SaveNewPost(Post post);
    }
}