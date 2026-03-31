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
        Task<List<Models.Blog.Post>> GetPosts(int page, int size);
        Task<List<Models.Blog.Post>> GetPromoPosts();
        Task<Models.Blog.Post> GetMainPost();
        Task<Models.Blog.Post> GetPost(int postId);
        Task<List<Models.Blog.Post>> GetAllPosts(); 
        Task<int> CountPosts();
        Task<Models.Blog.Post> GetPostWithElements(string name);
        Task<Models.Blog.Post> GetPost(string name); 
        Task UpdateAndSavePostChanges(Models.Blog.Post model);  
        Task<List<Models.Blog.PostElement>> GetPostElements(List<int> ids);
        Task<List<Models.Blog.PostElement>> GetPostElementsByPostId(int postId);
        Task<Models.Blog.Post> GetPostWithMetasById(int postId);
        Task<Models.Blog.Post> UpdateMetas(Models.Blog.Post post);
        Task Save(List<Models.Blog.PostElement> elements);
        Task<Models.Blog.Post> GetPostWithElements(int id);
        Task ClonePost(int id);
        Task<Models.Blog.Post> GetEntirePost(int postId);
        Task DeletePost(int id);
        Task SaveNewPost(Models.Blog.Post post);
        Task<List<Models.Blog.Post>> GetPagedAndFilteredPostsAsync(int page, int size);
    }
}
