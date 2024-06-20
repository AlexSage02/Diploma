namespace DAL
{
    public class Event : BaseEntity<Guid>
    { 
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Address { get; set; }
        public int Quantity { get; set; }
        public bool Available { get; set; }
        public DateTime Time { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
    }
}