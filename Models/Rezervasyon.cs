using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestoranRezervasyonu.Models
{
    public class Rezervasyon
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    
        public string Description { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan ReservationTime { get; set; } // Rezervasyon saati

        [Required]
        public int GuestCount { get; set; } // Katılacak kişi sayısı

        [Required]
       public string ContactNumber { get; set; } // Rezervasyon için iletişim numarası

        [Required]
       public int TableNumber { get; set; } // Rezerve edilen masa numarası


        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "The price cannot be less than zero.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; } // Rezervasyon ücreti

     
        [ValidateNever]
        public int RezervasyonTuruId { get; set; }
        [ForeignKey("RezervasyonTuruId")]

        [ValidateNever]
        public RezervasyonTuru RezervasyonTuru { get; set; }

   


    }
}
