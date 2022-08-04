using System.ComponentModel.DataAnnotations;

namespace rimCars_Api.Models
{
    public class AddRimDto
    {
        [Required]
        public int size { get; set; }
        [Required]
        public string color { get; set; }
        [Required]
        public double prize { get; set; }
    }
}
