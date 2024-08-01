using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Experience
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        
        public string Duration { get; set; }
        public string Company { get; set; }
        public string desc { get; set; }
    }
}
