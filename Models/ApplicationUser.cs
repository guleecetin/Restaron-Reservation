using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RestoranRezervasyonu.Models
{
    public class ApplicationUser: IdentityUser
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email field is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone field is required.")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [Display(Name = "Telephone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password field is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password Again")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

