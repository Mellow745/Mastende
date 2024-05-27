using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mastende.Models
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Surname { get; set; }
        public string? Fullname { get; set; }

        [Display(Name = "Street Name")]
        [Required]
        public string? StreetName { get; set; }

        [Required]
        public string? Area { get; set; }

        [Required]
        public string? Town { get; set; }

        public string? Location { get; set; }

        public float Bedrooms { get; set; }

        public float Bathrooms { get; set; }

        public float Garage { get; set; }

        [Display(Name = "Cellphone Number")]
        [Required(ErrorMessage = "Cellphone number is required")]
        [RegularExpression(@"^\+27\d{9}$", ErrorMessage = "Please enter a valid South African cellphone number")]
        public string? CellNumber { get; set; }

        [Required]
        public string? Description { get; set; }

        [Display(Name ="Upload Pictures of the place")]
        [NotMapped]
        public List<IFormFile>? Pictures { get; set; }

        [Display(Name = "Pictures file path")]
        public string? PicturesFile { get; set; }

        [Required]
        public decimal Price { get;set; }

        public string GetLocation()
        {
            Location = StreetName + "," + Area + "," + Town;
            return Location;
        }

        public string GetFullName()
        {
            Fullname = Name + " " + Surname;
            return Fullname;
        }
    }
}
