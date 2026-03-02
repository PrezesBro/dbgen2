using DBGenerator.Data;
using DBGenerator.Models;
using DBGenerator.Models.Ads;

namespace DBGenerator.Admin
{
    public class AdminAppService : IAdminAppService
    {
        IDataFacade _data;
        public AdminAppService(IDataFacade data) 
        {
            _data = data;
        }

        public async Task<Database> GetDatabase(int id)
        {
            return await _data.GetDatabase(id);
        }

        public async Task<Database> GetDatabaseWithTables(int id)
        {
            return await _data.GetDatabaseWithTables(id);
        }

        public async Task<List<Database>> GetDatabases()
        {
            return await _data.GetDatabases();
        }

        public async Task Save(Database db)
        {
            await _data.Save(db);
        }

        public async Task Clone(int id)
        {
            await _data.Clone(id);
        }

        public async Task<Table> GetTable(int id)
        {
            return await _data.GetTable(id);
        }

        public async Task Save(Table table)
        {
            await _data.Save(table);
        }

        public async Task<List<Ads>> GetAds()
        {
            return await _data.GetAllAds();
        }

        public async Task Save(Ads ads)
        {
            await _data.Save(ads);
        }
    }
}
