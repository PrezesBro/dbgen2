using DBGenerator.Models.Blog;

namespace DBGenerator.Models.Blog
{
    public class PostElementViewModel
    {
        public PostElement PostElement { get; set; }
        public bool Content2Visibility { get; set; } = false;
        public bool Content3Visibility { get; set; } = false;
        public bool Content4Visibility { get; set; } = false;
    }
}