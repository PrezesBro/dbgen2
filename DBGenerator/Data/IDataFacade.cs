using DBGenerator.Models;
using DBGenerator.Models.Ads;

namespace DBGenerator.Data
{
    public interface IDataFacade
    {
        Task<List<Database>> GetNewestDatabases();
        Task<List<Database>> GetDatabases();

        Task<Database> GetDatabase(int id);
        Task<Database> GetDatabaseWithContent(int databaseId);

        Task<List<Ads>> GetAds(Position position, bool onlyVisible, bool order);
        Task<List<Ads>> GetAllAds();
        Task Save(Database db);
        Task Clone(int id);
        Task<Database> GetDatabaseWithTables(int id);
        Task<Table> GetTable(int id);
        Task Save(Table table);
        Task Save(Ads ads);
        Task Delete(int id);
        Task<List<Column>> GetColumn(int tableId);
        Task Save(List<Column> columns);
    }
}
