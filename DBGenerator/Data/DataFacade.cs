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

        public async Task<Database> GetDatabaseWithContent(int databaseId)
        {
            return await _db.Databases
                .AsNoTracking()
                .Include(d => d.Tables)
                    .ThenInclude(t => t.Columns)
                .Include(d => d.Tables)
                    .ThenInclude(t => t.Datas)
                .Include(d => d.Tables)
                    .ThenInclude(t => t.ForeignKeys)
                .FirstOrDefaultAsync(d => d.Id == databaseId);
        }

        public async Task<Database> GetDatabase(int id)
        {
            return await _db.Databases.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Database> GetDatabaseWithTables(int id)
        {
            return await _db.Databases
                .AsNoTracking()
                .Include(d => d.Tables)
                .FirstOrDefaultAsync(d => d.Id == id);
        }


        public Task<List<Ads>> GetAds(Position position, bool onlyVisible, bool order)
        {
            var result = _db.Ads.Where(a => a.Position == position);

            if (onlyVisible)
                result = result.Where(a => a.IsVisible);

            if (order)
                result = result.OrderBy(a => a.Order);

            return result.ToListAsync();
        }

        public Task<List<Ads>> GetAllAds()
        {
            return _db.Ads.ToListAsync();
        }

        public async Task<List<Database>> GetDatabases()
        {
            return await _db.Databases.ToListAsync();
        }

        public async Task Save(Database db)
        {
            if(db.Id == 0)
            {
                _db.Databases.Add(db);
            }
            else
            {
                var oldDb = _db.Databases.First(d => d.Id == db.Id);
                oldDb.Name = db.Name;
                oldDb.Version = db.Version;
                oldDb.CreateDate = db.CreateDate;
                oldDb.Description = db.Description;
                oldDb.IsVisible = db.IsVisible;
            }

            await _db.SaveChangesAsync();
        }

        public async Task Clone(int id)
        {
            var original = await GetDatabaseWithContent(id);

            if (original == null) return;

            var clone = new Database
            {
                Name = original.Name,
                Version = original.Version + 1,
                IsVisible = false,
                Description = original.Description,
                CreateDate = DateTime.Now.Date,
                Tables = new List<Table>()
            };

            foreach(var table in original.Tables)
            {
                var newTable = new Table
                {
                    Name = table.Name,
                    Columns = new List<Column>(),
                    Datas = new List<Datas>(),
                    ForeignKeys = new List<ForeignKey>()
                };
                foreach(var data in table.Datas)
                {
                    newTable.Datas.Add(new Datas
                    {
                        Value = data.Value
                    });
                }
                foreach(var fk in table.ForeignKeys)
                {
                    newTable.ForeignKeys.Add(new ForeignKey
                    {
                        ColumnFkName = fk.ColumnFkName,
                        TablePkName = fk.TablePkName
                    });
                }
                foreach(var col in table.Columns)
                {
                    newTable.Columns.Add(new Column
                    {
                        Name = col.Name,
                        DataType = col.DataType,
                        Precision = col.Precision
                    });
                }

                clone.Tables.Add(newTable);
            }

            _db.Databases.Add(clone);

            await _db.SaveChangesAsync();
        }

        public async Task<Table> GetTable(int id)
        {
            return await _db.Tables.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task Save(Table table)
        {
            if(table.Id == 0)
            {
                var db = _db.Databases.First(d => d.Id == table.Database.Id);
                db.Tables.Add(table);
            }
            else
            {
                var oldTable = _db.Tables.First(t => t.Id == table.Id);
                oldTable.Name = table.Name;
            }
            await _db.SaveChangesAsync();
        }

        public async Task Save(Ads ads)
        {
            var adsOld = _db.Ads.First(a => a.Id == ads.Id);
            adsOld.PromoPrice = ads.PromoPrice;
            adsOld.Price = ads.Price;
            adsOld.Order = ads.Order;
            adsOld.BackgroundColor = ads.BackgroundColor;
            adsOld.EndPromotion = ads.EndPromotion;
            adsOld.Description = ads.Description;
            adsOld.Title = ads.Title;
            adsOld.IsPromotion = ads.IsPromotion;
            adsOld.DestinationUrl = ads.DestinationUrl;
            adsOld.ImageUrl = ads.ImageUrl;
            adsOld.IsVisible = ads.IsVisible;

            await _db.SaveChangesAsync();
        }
    }
}
