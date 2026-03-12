using DBGenerator.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBGenerator.Controllers
{
    public class BaseController : Controller
    {
        protected void SetMetas(Metas metas)
        {
            ViewData["Og_Title"] = metas.Og_Title;
            ViewData["Og_Description"] = metas.Og_Description;
            ViewData["Og_Image"] = metas.Og_Image;
            ViewData["Og_Url"] = metas.Og_Url;
            ViewData["Title"] = metas.SiteTitle;
        }
    }
}
