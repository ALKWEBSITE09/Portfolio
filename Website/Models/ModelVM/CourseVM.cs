using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Models.ModelVM
{
    public class CourseVM
    {
        public int Id { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateOnly Date { get; set; }
        public double Price { get; set; }
        public string Certificate { get; set; }

    }
}
