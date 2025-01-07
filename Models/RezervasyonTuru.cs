using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RestoranRezervasyonu.Models
{
    public class RezervasyonTuru
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Reservation type name cannot be left blank!")]
        [MaxLength(25)]
        [DisplayName("Reservation Type Name")]
        public string Name { get; set; }
       

    }
}
