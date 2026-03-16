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
                TotalPages = 1 + (await _data.CountPosts()) / 12
            };

            if (page > 1) page--;
            if (page > 1) page--;
            if (page > 1) result.Posts.PageList.Add(0);

            int cnt = result.Posts.PageList.Count + 5;

            var total = result.Posts.TotalPages;

            for (int i = 0; i < 5; i++)
            {
                if (result.Posts.PageList.Count < cnt && page <= total)
                {
                    result.Posts.PageList.Add(page);
                    page++;
                }
            }

            if (page <= total) result.Posts.PageList.Add(0);

            return result;
        }

        public async Task<Post> GetPostWithElements(string post_name)
        {
            var post = await _data.GetPostWithElements(post_name);
            post.Elements = post.Elements.OrderBy(p => p.Order).ToList();
            return post;
        }

        public async Task<EditPostViewModel> GetEditPostVM(string post_name)
        {
            var result = new EditPostViewModel();
            result.Post = await _data.GetPost(post_name);
            var elements = await _data.GetPostElements(result.Post.Id);
            result.PostElements = elements.Select(e => new PostElementViewModel
            {
                PostElement = e,
                Content2Visibility = ContentVisibilityResolver(e.Type, 2),
                Content3Visibility = ContentVisibilityResolver(e.Type, 3),
                Content4Visibility = ContentVisibilityResolver(e.Type, 4)
            }).ToList();
            //w widoku if(model.Content2Visibility) -> {html kontrolki edycji Content2} 
            return result;
        }

        private bool ContentVisibilityResolver(ElementType type, int contentNumber)
        {
            if (type == ElementType.Title && contentNumber == 2) return true;

            if (type == ElementType.LeftImage && contentNumber == 2) return true;

            if (type == ElementType.RightImage && contentNumber == 2) return true;


            return false;
        }
    }
}
