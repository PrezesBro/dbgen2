namespace DBGenerator.Models.Blog
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string NameUrl { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public DateTime PublishDate { get; set; }
        public Position Position { get; set; } = 0;
        public Status Status { get; set; }
        public string ImageUrl { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<PostElement> Elements { get; set; } = new List<PostElement>();
        public virtual Metas Metas { get; set; }
    }
}