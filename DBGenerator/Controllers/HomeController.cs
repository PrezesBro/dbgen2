using DBGenerator.GenerateEngine;
using DBGenerator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Net.Sockets;

namespace DBGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IDBGenAppService _dbGenAppService;
        private IGenDataFactory _dbGenDataFactory;

        public HomeController(ILogger<HomeController> logger, IDBGenAppService dBGenAppService, IGenDataFactory genDataFactory)
        {
            _logger = logger;
            _dbGenAppService = dBGenAppService;
            _dbGenDataFactory = genDataFactory;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _dbGenAppService.GetDbGenViewModelAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GenerateDatabase(DBGenViewModel model, string actionType)
        {
            var genData = _dbGenDataFactory.Create(model.SelectedEngine.Value);
            var script = await genData.GenerateAsync(model.SelectedDatabase.Value);

            if (actionType == "generate")
            {
                var newModel = await _dbGenAppService.GetDbGenViewModelAsync();

                newModel.Script = script;

                newModel.SelectedEngine = model.SelectedEngine;
                newModel.SelectedDatabase = model.SelectedDatabase;

                return View("Index", newModel);
            }

            var file = System.Text.Encoding.UTF8.GetBytes(script);
            return File(file, "application/sql", $"skrypt.sql");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
