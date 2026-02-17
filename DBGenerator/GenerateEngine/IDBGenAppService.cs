using DBGenerator.Models;

namespace DBGenerator.GenerateEngine
{
    public interface IDBGenAppService
    {
        Task<DBGenViewModel> GetDbGenViewModelAsync();
    }
}
