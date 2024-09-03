using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TabloidMVC.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string? Subject { get; set; }

        [Required]
        public string? Content { get; set; }

        [DisplayName("Author")]
        public int UserProfileId { get; set; }
        public UserProfile? UserProfile { get; set; } //use UserProfile.DisplayName in Comment's Index.cshtml

        [DisplayName("Created on")]
        public DateTime CreateDateTime { get; set; }

        public Post? Post { get; set; } //use Post.Title in Comment's Index.cshtml
        public int PostId { get; set; }


    }
}
