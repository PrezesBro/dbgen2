using DBGenerator.Admin;
using DBGenerator.GenerateEngine;
using DBGenerator.Models;
using DBGenerator.Models.Ads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
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
        public async Task<IActionResult> Delete(int id)
        {
            await _adminAppService.DeleteDatabase(id);
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
        
        public async Task<IActionResult> EditColumn(int tableId)
        {
            var model = await _adminAppService.GetColumns(tableId);
            if (model.Count == 0)
            {
                model.Add(new Column
                {
                    Table = new Table
                    {
                        Id = tableId
                    }
                });  
            }
            return View(model); 
            //dodać kolumny i usunąć 
            //dodać przycisk zapisz 
        }
        [HttpPost]
        public async Task<IActionResult> ColumnActions(List<Column> model, string actionType)
        {
            if (actionType == "add")
            {
                model.Add(new Column());               
            }
            else if(actionType == "save")
            {
                await _adminAppService.Save(model);
                return RedirectToAction("EditTable", model[0].Table.Id); 
            }

            return RedirectToAction("EditColumn", model);
        }




        public async Task<IActionResult> DeleteColumn(int tableId, int columnId)
        {
            var model = await _adminAppService.GetColumns(tableId);
            var columnToRemove = model.FirstOrDefault(c => c.Id == columnId);

            if (columnToRemove != null)
            {
                model.Remove(columnToRemove);
            }
            
            return View("EditColumn", model);
        }
    }
}
