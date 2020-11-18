using Country_Observer.Models;
using System.Linq;

namespace Country_Observer.Data
{
    public interface ICountriesRepository
    {
        public IQueryable<CountryDb> Countries { get; }
        public IQueryable<Region> Regions { get; }
        public IQueryable<City> Cities { get; }
    }
}
