namespace LLCAcademy.Models
{
    public class Address
    {
        public int Id { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Line3 { get; set; }

        public string Town { get; set; }

        public string County { get; set; }

        public string Postcode { get; set; }

        public string GoogleMapsUrl { get; set; }

        public int AddressId { get; set; }
    }
}