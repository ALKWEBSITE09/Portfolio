using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Resume
    {
        [Key]
        public int Id { get; set; }
        public string url { get; set; }
    }
}
