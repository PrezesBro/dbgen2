namespace DBGenerator.Models.Blog
{
    public class EditPostViewModel
    {
        public Post Post { get; set; }
        public List<PostElementViewModel> PostElements { get; set; } = new List<PostElementViewModel>();
    }
}
