using DBGenerator.Models;
using DBGenerator.Models.Ads;

namespace DBGenerator.Admin
{
    public interface IAdminAppService
    {
        Task<List<Database>> GetDatabases();
        Task<Database> GetDatabase(int id);
        Task Save(Database db);
        Task Clone(int id);
        Task<Database> GetDatabaseWithTables(int id);
        Task<Table> GetTable(int id);
        Task Save(Table table);
        Task<List<Ads>> GetAds();
        Task Save(Ads ads);
    }
}
