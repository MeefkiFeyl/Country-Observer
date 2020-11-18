namespace Country_Observer.Models
{
    public class CountryDb : CountryBase
    {
        public int Id { get; set; }
        public int? Region { get; set; }
        public int? Capital{ get; set; }
    }
}
