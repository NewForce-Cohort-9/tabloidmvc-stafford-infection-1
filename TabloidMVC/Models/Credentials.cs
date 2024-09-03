using System.ComponentModel.DataAnnotations;

namespace TabloidMVC.Models
{
    public class Credentials
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string DisplayName { get; set; }


    }
}
