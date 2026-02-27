using DBGenerator.Admin;
using DBGenerator.GenerateEngine;
using DBGenerator.Models;
using DBGenerator.Models.Ads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DBGenerator.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IAdminAppService _adminAppService;

        public AdminController(IAdminAppService adminAppService) 
        {
            _adminAppService = adminAppService;
        }

        public async Task<IActionResult> Index() 
        {
            var model = new AdminViewModel();
            model.Databases = await _adminAppService.GetDatabases();
            model.Ads = await _adminAppService.GetAds();
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _adminAppService.GetDatabaseWithTables(id);
            return View("Add", model);
        }

        public async Task<IActionResult> Copy(int id)
        {
            await _adminAppService.Clone(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add() 
        {
            var model = new Database();
            model.CreateDate = DateTime.Now.Date;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveDatabase(Database db)
        {
            await _adminAppService.Save(db);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddTable(int databaseId)
        {
            var model = new Table();
            model.Database = new Database { Id = databaseId };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditTable(int id)
        {
            var model = await _adminAppService.GetTable(id);
            return View("AddTable", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTable(Table table)
        {
            await _adminAppService.Save(table);
            return RedirectToAction("EditTable", new { id = table.Id });
        }

        [HttpPost]
        public async Task<IActionResult> SaveAds(Ads ads)
        {
            await _adminAppService.Save(ads);
            return RedirectToAction("Index");
        }
    }
}
