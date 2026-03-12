using DBGenerator.Data;
using DBGenerator.Models.Blog;

namespace DBGenerator.Blog
{
    public class BlogAppService : IBlogAppService
    {
        IDataFacade _data;
        public BlogAppService(IDataFacade data)
        {
            _data = data;
        }

        public async Task<BlogViewModel> GetBlogVM(int page)
        {
            var result = new BlogViewModel();

            result.MainPost = await _data.GetMainPost();
            result.PromoPosts = await _data.GetPromoPosts();
            result.Posts = new PostPageViewModel
            {
                Page = page,
                Posts = await _data.GetPosts(page, 12),
                TotalPages = 1+(await _data.CountPosts())/12
            };

            if (page > 1) page--;
            if (page > 1) page--;
            if (page > 1) result.Posts.PageList.Add(0);

            int cnt = result.Posts.PageList.Count + 5;

            var total = result.Posts.TotalPages;

            for(int i = 0; i < 5; i++)
            {
                if(result.Posts.PageList.Count < cnt && page <= total)
                {
                    result.Posts.PageList.Add(page);
                    page++;
                }
            }

            if(page <= total) result.Posts.PageList.Add(0);

            return result;
        }

        public async Task<Post> GetPost(string post_name)
        {
            var post = await _data.GetPost(post_name);
            post.Elements = post.Elements.OrderBy(p => p.Order).ToList();
            return post;
        }
    }
}
