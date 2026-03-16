using DBGenerator.Blog;
using DBGenerator.Models.Blog;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System;
using System.Text.Json;
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

        public async Task<IActionResult> ShowPost(string post_name)
        {
            var model = await _blogAppService.GetPostWithElements(post_name);
            //SetMetas(model.Metas); 
            return View(model);
        }
        public async Task<IActionResult> EditBlog(int p)
        {
            if (p == 0) p = 1;
            var model = await _blogAppService.GetBlogVM(p);
            return View(model);
        }
        public async Task<IActionResult> EditPost(string post_name)
        {
            var model = await _blogAppService.GetPostWithElements(post_name);                    
            ViewBag.NameUrl = model.NameUrl;
            return View("EditPost", model); 
        }       
    }
}

//w liscie elementów dodać i usuwać elementy. w zależności od typu to rózna ilość elementów:
//zawsze 4 a puste mają się nie zapisywać 
//jak wczytuje elementy juz istnieją to ma ich być tyle ile jest
//a jak tworzysz nowy to wszystkie 4 
//type ma być rozwijany film youtube = 1 contect
//zdjecie 1 kontekt 
//type 0 3 i 4 wyświetlam 2 kontenty 
//zrobić PostElementViewModel z bool czy content jest widoczny 


//zakłądka edutyj w _postelement
//zakładka edytuj blog -> lista blog postów który post do edycji -> edycja postu,
//wszystko z posts i metas z bd, lista rozwijana z kategorii z categories,
//_blogAppService do bd
//w edycji posty posegregowany od najnowszego 


//< div class= "col-xxl-6" >   
//                        < img src = "@Model.ImageUrl" style = "width:100%" /> TUTAJ DO WYSWIETLENIA MALEGO ZDJECIA
//                    </ div >
//                    < div class= "col-xxl-6" >
//                        < div class= "form-group mb-3" >
//                            < label asp -for= "ImageUrl" class= "control-label" ></ label >
//                            < input asp -for= "ImageUrl" class= "form-control" />
//                            < span asp - validation -for= "ImageUrl" class= "text-danger" ></ span >
//                        </ div >
//                        kontrolka do edycji zdjecia  