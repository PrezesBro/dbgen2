using DBGenerator.Models.Blog;

namespace DBGenerator.Models.Blog
{
    public class PostElementViewModel
    {
        public PostElement PostElement { get; set; } = new PostElement();
        public bool Content2Visibility { get; set; } = true;
        public bool Content3Visibility { get; set; } = true;
        public bool Content4Visibility { get; set; } = true;
    }
}