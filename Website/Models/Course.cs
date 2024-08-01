using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateOnly Date { get; set; }
        public double Price { get; set;}
        public string Certificate { get; set; }
    }
}
