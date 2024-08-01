using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Models
{
    public class Technology
    {
        [Key]
        public int Id { get; set; }
        public string image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
    }
}
