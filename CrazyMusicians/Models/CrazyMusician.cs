using System.ComponentModel.DataAnnotations;

namespace CrazyMusicians.Models
{
    public class CrazyMusician
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Muzisyen adi gereklidir")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Muzisyen adi 3 ile 50 karakter arasinda olmalidir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Meslek kismi bos gecilemez")]
        public string Job { get; set; }

        
        public string Description { get; set; }
    }
}
