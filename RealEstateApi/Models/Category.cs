using System.ComponentModel.DataAnnotations;

namespace RealEstateApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name Can Not Be NULL or Empty")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Image Url Can Not Be NULL or Empty")]
        public string ImageUrl { get; set; }

        public ICollection<Property> Properties { get; set; }
    }
}
