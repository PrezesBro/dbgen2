using DBGenerator.Blog;
using DBGenerator.Models.Blog;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace DBGenerator.Controllers
{
    public class BlogController : BaseController
    {
        private IBlogAppService _blogAppService;

        public BlogController(IBlogAppService blogAppService) 
        {
            _blogAppService = blogAppService;
        }

        public async Task<IActionResult> Index(int p)
        {
            if (p == 0) p = 1;
            var model = await _blogAppService.GetBlogVM(p);
            return View(model);
        }

        public async Task<IActionResult> ShowPost(string post_name)
        {
            var model = await _blogAppService.GetPost(post_name);
            return View(model);
        }
    }
}
