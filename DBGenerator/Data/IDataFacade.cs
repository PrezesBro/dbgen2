using DBGenerator.Models;

namespace DBGenerator.Data
{
    public interface IDataFacade
    {
        Task<List<Database>> GetNewestDatabases();
    }
}
