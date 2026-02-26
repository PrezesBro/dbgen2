using DBGenerator.Data;
using DBGenerator.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DBGenerator.GenerateEngine
{
    public class DBGenAppService : IDBGenAppService
    {
        private IDataFacade _dataFacade;

        public DBGenAppService(IDataFacade dataFacade)
        {
            _dataFacade = dataFacade;
        }

        public async Task<DBGenViewModel> GetDbGenViewModelAsync()
        {
            var dbGenViewModel = new DBGenViewModel();

            var dbs = await _dataFacade.GetNewestDatabases();
            dbGenViewModel.EngineTypes = Enum.GetValues(typeof(EngineType)).Cast<EngineType>().Select(e => new SelectListItem(GetDisplayName(e), ((int)e).ToString()));
            dbGenViewModel.Databases = dbs.Select(d => new SelectListItem(d.Name, d.Id.ToString()));

            return dbGenViewModel;
        }

        private string GetDisplayName(Enum e)
        {
            // pobiera Display/Description lub nazwę enuma
            var type = e.GetType();
            var mem = type.GetMember(e.ToString()).FirstOrDefault();
            if (mem != null)
            {
                var display = mem.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false)
                                 .Cast<System.ComponentModel.DataAnnotations.DisplayAttribute>()
                                 .FirstOrDefault();
                if (display != null) return display.GetName();

                var desc = mem.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                              .Cast<System.ComponentModel.DescriptionAttribute>()
                              .FirstOrDefault();
                if (desc != null) return desc.Description;
            }
            return e.ToString();
        }
    }
}
