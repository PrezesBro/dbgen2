using DBGenerator.Blog;
using DBGenerator.Models;
using DBGenerator.Models.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol;
using System;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

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

        public async Task<IActionResult> ShowPost(string nameUrl)
        {
            var model = await _blogAppService.GetPostWithElements(nameUrl);
            return View(model);
        }
        public async Task<IActionResult> EditPost(int id)
        {
            var model = await _blogAppService.GetEditPostVM(id);
            model.Categories = await _blogAppService.GetAllCategories();
            ViewBag.NameUrl = model.Post.NameUrl;

            return View("EditPost", model);
        }

        public async Task<IActionResult> GetAllPosts(int p)
        {
            if (p == 0) p = 1;
            var posts = await _blogAppService.GetPostPageVM(p);

            return View("EditBlog", posts);
        }

        [HttpPost]
        public async Task<IActionResult> SavePostChanges(Post model)
        {
            await _blogAppService.UpdatePost(model);
            TempData["UpdatedPostId"] = model.Id;
            ModelState.AddModelError(nameof(model.Category), "Błąd");
            if (ModelState.IsValid)
            {
            }
            return RedirectToAction("GetAllPosts");
        }

        [HttpPost]
        public async Task<IActionResult> SaveElementsChanges(EditPostViewModel model, string actionType)
        {
            if (actionType == "save")
            {
                var elements = model.PostElements.Select(e => e.PostElement).ToList();
                await _blogAppService.UpdatePostElements(elements);

                return RedirectToAction("GetAllPosts");
            }

            var pe = new PostElementViewModel
            {
                PostElement = new PostElement
                {
                    Post = new Post
                    {
                        Id = model.Post.Id
                    }
                }
            };
            pe.PostElement.Post.Id = model.Post.Id;
            model.PostElements.Add(pe);
            return View("EditPost", model);
        }

        public async Task<IActionResult> EditMetas(int postId)
        {
            var postWithMetas = await _blogAppService.GetPostWithMetasById(postId);
            return View(postWithMetas);
        }

        [HttpPost]
        public async Task<IActionResult> EditMetass(Post post)
        {
            var postMetas = await _blogAppService.UpdateMetas(post);
            return RedirectToAction("GetAllPosts");
        }

        public async Task<IActionResult> CopyPost(int id)
        {
            await _blogAppService.ClonePost(id);
            return RedirectToAction("GetAllPosts");
        }
        public async Task<IActionResult> DeletePost(int id)
        {
            await _blogAppService.DeletePost(id);
            return RedirectToAction("GetAllPosts");
        }
        public async Task<IActionResult>  AddNewPost()
        {
            var categories = await _blogAppService.GetAllCategories();
            var vm = new PostCategoryViewModel
            {
                Post = new Post(),
                Categories = categories
            };
            
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> SaveNewPost(Post post)
        {
            await _blogAppService.SaveNewPost(post);
            if (post == null)
                return RedirectToAction("GetAllPosts");

            TempData["SuccessMessage"] = "Post został pomyślnie utworzony, uzupełnij elementy i metas!";
            return RedirectToAction("EditPost", new { id = post.Id });
        }
        public async Task<IActionResult> AddNewCategory()
        {
            var categories = await _blogAppService.GetCategoryDictionary();
            return View(categories);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCategory(string newCategory)
        {
            //dodać string
            await _blogAppService.AddNewCategory(newCategory);
            return RedirectToAction("AddNewCategory"); 
        }
    }
}
