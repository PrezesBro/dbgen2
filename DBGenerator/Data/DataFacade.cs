using DBGenerator.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace DBGenerator.Data
{
    public class DataFacade : IDataFacade
    {
        private ApplicationDbContext _db;
        public DataFacade(ApplicationDbContext context)
        {
            _db = context;
        }

        public Task<List<Database>> GetNewestDatabases()
        {
            return _db.Databases.ToListAsync();

        }
    }
}
