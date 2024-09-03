namespace TabloidMVC.Models.ViewModels
{
    public class CommentViewModel
    {
        public string PostTitle { get; set; }
        public int PostId { get; set; }
        public List<Comment> Comments { get; set; }
        public Comment Comment { get; set; }
    }
}
