using DBGenerator.Data;
using DBGenerator.Models;
using DBGenerator.Models.Ads;
using Microsoft.EntityFrameworkCore;

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
        public async Task DeleteDatabase(int id)
        {
            await _data.Delete(id);
        }

        public async Task<List<Column>> GetColumns(int tableId)
        {
            return await _data.GetColumn(tableId);
        }
        public async Task Save(List<Column> columns)
        {
            await _data.Save(columns); 
        }
    }
}
