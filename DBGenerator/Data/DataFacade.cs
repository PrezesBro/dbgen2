using DBGenerator.Models;
using DBGenerator.Models.Ads;
using DBGenerator.Models.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
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


        public Task<List<Ads>> GetAds(Models.Ads.Position position, bool onlyVisible, bool order)
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
            if (db.Id == 0)
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
        public async Task ClonePost(int id)
        {
            var orginal = await GetEntirePost(id);

            if (orginal == null) return;
            var ticks = DateTime.Now.Ticks;
            var clone = new Post
            {
                Title = orginal.Title + $"-{ticks}",
                NameUrl = orginal.NameUrl + $"-{ticks}",
                Description = orginal.Description,
                Tags = orginal.Tags,
                PublishDate = DateTime.Now.Date,
                Position = orginal.Position,
                Status = orginal.Status,
                ImageUrl = orginal.ImageUrl,
                CategoryId = orginal.CategoryId,
                Elements = new List<PostElement>(),
                Metas = new Metas
                {
                    Og_Title = orginal.Metas.Og_Title,
                    Og_Description = orginal.Metas.Og_Description,
                    Og_Image = orginal.Metas.Og_Image,
                    Og_Url = orginal.Metas.Og_Url,
                    SiteTitle = orginal.Metas.SiteTitle
                }
            };

            var clonedElements = orginal.Elements
                .Select(e => new PostElement
                {
                    Type = e.Type,
                    Content1 = e.Content1,
                    Content2 = e.Content2,
                    Content3 = e.Content3,
                    Content4 = e.Content4,
                    Order = e.Order,                  
                })
                .ToList();

            clone.Elements = clonedElements;
            _db.Posts.Add(clone);  
            await _db.SaveChangesAsync();
        }

        public async Task DeletePost(int id)
        {
            var post = await GetEntirePost(id);

            if (post != null)
            {
                _db.Remove(post);
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

            foreach (var table in original.Tables)        
            {
                var newTable = new Table
                {
                    Name = table.Name,
                    Columns = new List<Column>(),
                    Datas = new List<Datas>(),
                    ForeignKeys = new List<ForeignKey>()
                };
                foreach (var data in table.Datas)
                {
                    newTable.Datas.Add(new Datas                
                    {
                        Value = data.Value
                    });
                }
                foreach (var fk in table.ForeignKeys)
                {
                    newTable.ForeignKeys.Add(new ForeignKey
                    {
                        ColumnFkName = fk.ColumnFkName,
                        TablePkName = fk.TablePkName
                    });
                }
                foreach (var col in table.Columns)
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
            if (table.Id == 0)
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

        public async Task<List<Post>> GetAllPosts()
        {
            return await _db.Posts.ToListAsync();
        }

        public async Task<Post> GetPost(int postId)
        {
            return await _db.Posts.FirstOrDefaultAsync(p => p.Id == postId);
        }

        public async Task<Post> GetMainPost()
        {
            return await _db.Posts.Where(p => p.PublishDate < DateTime.Now && p.Position == Models.Blog.Position.Main && p.Status == Status.Public).OrderByDescending(p => p.PublishDate).FirstOrDefaultAsync();
        }

        public async Task<List<Post>> GetPromoPosts()
        {
            return await _db.Posts.Where(p => p.PublishDate < DateTime.Now && p.Position == Models.Blog.Position.Promo && p.Status == Status.Public).OrderByDescending(p => p.PublishDate).Take(3).ToListAsync();
        }

        public async Task<List<Post>> GetPosts(int page, int size)
        {
            return await _db.Posts.
                Skip((page - 1) * size) 
                .Take(size)
                .ToListAsync();
        }
        public async Task<List<Post>> GetPagedAndFilteredPostsAsync(int page, int size)
        {
            return await _db.Posts
                .OrderByDescending(x => x.PublishDate)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<int> CountPosts()
        {
            return await _db.Posts.Where(p => p.PublishDate < DateTime.Now && p.Status == Status.Public).CountAsync();
        }

        public async Task<Post> GetPostWithElements(string name)
        {
            return await _db.Posts.Include(p => p.Elements).FirstOrDefaultAsync(p => p.NameUrl == name);
        }
        public async Task<Post> GetPostWithElements(int id)
        {
            return await _db.Posts.Include(p => p.Elements).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Post> GetPost(string name)
        {
            return await _db.Posts.FirstOrDefaultAsync(p => p.NameUrl == name);
        }

        public async Task UpdateAndSavePostChanges(Post model)
        {
            var post = await GetPost(model.Id);

            post.Title = model.Title;
            post.Description = model.Description;
            post.Tags = model.Tags;
            post.PublishDate = model.PublishDate;
            post.Position = model.Position;
            post.Status = model.Status;
            post.ImageUrl = model.ImageUrl;

            await _db.SaveChangesAsync();
        }

        public Task<List<PostElement>> GetPostElements(List<int> ids)
        {
            return _db.PostElements
            .Where(e => ids.Contains(e.Id))
            .ToListAsync();
        }

        public Task<List<PostElement>> GetPostElementsByPostId(int postId)
        {
            return _db.PostElements
                .Where(e => e.Post.Id == postId)
                .ToListAsync();
        }
        public async Task Save(List<PostElement> elements) 
        {
            var post = await _db.Posts.Include(p => p.Elements).FirstOrDefaultAsync(p => p.Id == elements[0].Post.Id);
            foreach (var element in post.Elements)
            {
                var newElement = elements.FirstOrDefault(e => e.Id == element.Id);
                if (newElement == null)
                {
                    post.Elements.Remove(element);
                }
                else
                {
                    element.Content1 = newElement.Content1;
                    element.Content2 = newElement.Content2;
                    element.Content3 = newElement.Content3;
                    element.Content4 = newElement.Content4;
                    element.Type = newElement.Type;
                    element.Order = newElement.Order;
                }
            }
            post.Elements.AddRange(elements.Where(e => e.Id == 0));
            await _db.SaveChangesAsync();
        }
  
        public async Task<Post> GetPostWithMetasById(int postId)
        {
            var postWithMetas = await _db.Posts
                      .Include(p => p.Metas)
                      .FirstOrDefaultAsync(p => p.Id == postId);

            return postWithMetas;
        }
        public async Task<Post> GetEntirePost(int postId)
        {
            var postWithMetas = await _db.Posts
                      .Include(p => p.Metas)
                      .Include(e => e.Elements)                      
                      .FirstOrDefaultAsync(p => p.Id == postId);

            return postWithMetas;
        }
        public async Task<Post> UpdateMetas(Post post)
        {
            var postWithMetas = await GetPostWithMetasById(post.Id);
            if (postWithMetas != null)
            {

                postWithMetas.Metas.Og_Title = post.Metas.Og_Title;
                postWithMetas.Metas.Og_Description = post.Metas.Og_Description;
                postWithMetas.Metas.Og_Image = post.Metas.Og_Image;
                postWithMetas.Metas.SiteTitle = post.Metas.SiteTitle;
               
            
                _db.SaveChanges();
            }
            return postWithMetas; 
        }
        public async Task SaveNewPost(Post post)
        {
            post.CategoryId = 1;
            post.Metas = new Metas();           
            _db.Posts.Add(post);
            await _db.SaveChangesAsync();
        }
    }
}
