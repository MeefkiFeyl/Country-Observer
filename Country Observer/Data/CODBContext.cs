using Country_Observer.Models;
using System.Data.Entity;

namespace Country_Observer.Data
{
    public class CODBContext : DbContext
    {
        public CODBContext() : base("name=DefaultConnection")
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<CountryDb> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}
