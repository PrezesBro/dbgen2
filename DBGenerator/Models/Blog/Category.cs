namespace DBGenerator.Models.Blog
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Post> Posts { get; set; } = new List<Post>();
    }
}
