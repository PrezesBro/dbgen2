namespace DBGenerator.Models.Blog
{
    public class PostElement
    {
        public int Id { get; set; }
        public ElementType Type { get; set; }
        public string Content1 { get; set; }
        public string Content2 { get; set; }
        public string Content3 { get; set; }
        public string Content4 { get; set; }
        public int Order { get; set; }
        public virtual Post Post { get; set; }
    }
}
