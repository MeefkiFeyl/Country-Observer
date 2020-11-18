using Country_Observer.Models;
using System.Linq;

namespace Country_Observer.Data
{
    public class CountriesRepository : ICountriesRepository
    {
        public CODBContext context;
        public CountriesRepository(CODBContext ctx)
        {
            context = ctx;
        }

        public IQueryable<City> Cities => context.Cities;
        public IQueryable<Region> Regions => context.Regions;
        public IQueryable<CountryDb> Countries => context.Countries;
    }
}
