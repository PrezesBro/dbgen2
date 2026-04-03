using DBGenerator.Data;
using DBGenerator.Models;
using DBGenerator.Models.Blog;
using Microsoft.Extensions.Hosting;

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

            result.Posts.PageList = GetPageNumbers(page, result.Posts.TotalPages);

            return result;
        }

        public async Task<PostPageViewModel> GetPostPageVM(int page)
        {
            var result = new PostPageViewModel
            {
                Page = page,
                Posts = await GetPostsOrderedByPublishDateAsync(page, 10),
                TotalPages = 1 + (await _data.CountPosts()) / 10
            };

            result.PageList = GetPageNumbers(page, result.TotalPages);
            return result;
        }

        private List<int> GetPageNumbers(int page, int totalPages)
        {
            var result = new List<int>();


            if (page > 1) page--;
            if (page > 1) page--;
            if (page > 1) result.Add(0);

            int cnt = result.Count + 5;          

            for (int i = 0; i < 5; i++)
            {
                if (result.Count < cnt && page <= totalPages)
                {
                    result.Add(page);
                    page++;
                }
            }

            if (page <= totalPages) result.Add(0);

            return result;
        }
        public async Task<Post> GetPostWithElements(string post_name)
        {
            var post = await _data.GetPostWithElements(post_name);
            post.Elements = post.Elements.OrderBy(p => p.Order).ToList();
            return post;
        }  

        public async Task<EditPostViewModel> GetEditPostVM(int id)
        {
            var result = new EditPostViewModel();
            result.Post = await _data.GetPost(id);
            var elements = await _data.GetPostElementsByPostId(result.Post.Id);
            result.PostElements = elements.Select(e => new PostElementViewModel
            {
                PostElement = e,
                Content2Visibility = ContentVisibilityResolver(e.Type, 2),
                Content3Visibility = ContentVisibilityResolver(e.Type, 3),
                Content4Visibility = ContentVisibilityResolver(e.Type, 4)
            }).ToList();
            return result;
        }

        private bool ContentVisibilityResolver(ElementType type, int contentNumber) 
        {
            if (type == ElementType.Title && contentNumber == 2) return true;

            if (type == ElementType.LeftImage && contentNumber == 2) return true;

            if (type == ElementType.RightImage && contentNumber == 2) return true;
                 
            return false;
        }

        public async Task<List<Post>> GetPostsOrderedByPublishDateAsync(int page, int size) 
        {
             return await _data.GetPagedAndFilteredPostsAsync(page, size);                 
        }

        public async Task UpdatePost(Post model)
        {
             await _data.UpdateAndSavePostChanges(model);        
        }

        public async Task UpdatePostElements(List<PostElement> elements) 
        {
            await _data.Save(elements);
        }
        public async Task<Post> GetPostWithMetasById(int postId)
        {
            var post = await _data.GetPostWithMetasById(postId);
            return post;
        }
        public async Task<Post> UpdateMetas(Post post)
        {
            var postMetas = await _data.UpdateMetas(post);
            return postMetas;
        }
        public async Task ClonePost(int id)
        {
            await _data.ClonePost(id);

        }
        public async Task DeletePost(int id)
        {
            await _data.DeletePost(id);
        }
        public async Task SaveNewPost(Post post)
        {
            await _data.SaveNewPost(post);
        }

        public async Task AddNewCategory(string newCategory)
        {
            await _data.AddNewCategory(newCategory); 
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _data.GetAllCategories();
        }
    }
}
 