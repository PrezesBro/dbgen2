using DBGenerator.Models;
using DBGenerator.Models.Ads;

namespace DBGenerator.Data
{
    public interface IDataFacade
    {
        Task<List<Database>> GetNewestDatabases();



        Task<List<Ads>> GetAds(Position position, bool onlyVisible, bool order);
        public Task<List<Ads>> GetAllAds();
    }
}
