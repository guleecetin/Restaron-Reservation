namespace RestoranRezervasyonu.Models
{
    public class Masa
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsOccupied { get; set; }
        public DateTime? LastUpdated { get; set; }


    }
}
