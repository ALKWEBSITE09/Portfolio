using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int profileId { get; set; }
        [ForeignKey("profileId")]
        public Profile profile { get; set; }
    }
}
