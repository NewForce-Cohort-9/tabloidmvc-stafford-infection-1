namespace TabloidMVC.Models.ViewModels
{
    public class CommentViewModel
    {
        public string PostTitle { get; set; }
        public int PostId { get; set; }
        public List<Comment> Comments { get; set; }
        public Comment Comment { get; set; }
        public int CurrentUserId { get; set; } // Add this to check if user is author of comment to edit/delete comment 
    }
}
