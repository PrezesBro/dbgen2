using DBGenerator.Models;
using DBGenerator.Models.Ads;
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




        public Task<List<Ads>> GetAds(Position position, bool onlyVisible, bool order)
        {
            var result = _db.Ads.Where(a => a.Position == position);

            if (onlyVisible)
                result.Where(a => a.IsVisible);

            if (order)
                result.OrderBy(a => a.Order);

            return result.ToListAsync();
        }

        public Task<List<Ads>> GetAllAds()
        {
            return _db.Ads.ToListAsync();
        }
    }
}
