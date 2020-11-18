using Country_Observer.Converters;
using Country_Observer.Data;
using Country_Observer.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;

namespace Country_Observer.Data
{
    public static class QueryHelper
    {
        public static void SaveCountry(CODBContext context, ObservableCollection<CountryAPI> Countries)
        {
            foreach (CountryAPI country in Countries)
            {
                if (context.Cities.Where(c => c.Name == country.Capital).FirstOrDefault() == null)
                    context.Cities.Add(new City { Name = country.Capital });
                if (context.Regions.Where(r => r.Name == country.Region).FirstOrDefault() == null)
                    context.Regions.Add(new Region { Name = country.Region });

                context.SaveChanges();

                var targetCountry = context.Countries
                        .Where(w => w.Name == country.Name).FirstOrDefault();

                if (targetCountry != null)
                {
                    targetCountry.Alpha3Code = country.Alpha3Code;
                    targetCountry.Area = country.Area;
                    targetCountry.Capital = context.Cities.Where(w => w.Name == country.Capital).Select(s => s.Id).FirstOrDefault();
                    targetCountry.Population = country.Population;
                    targetCountry.Region = context.Regions.Where(w => w.Name == country.Region).Select(s => s.Id).FirstOrDefault();
                }
                else
                {
                    context.Countries.Add(new CountryDb
                    {
                        Name = country.Name,
                        Alpha3Code = country.Alpha3Code,
                        Capital = context.Cities.Local.Where(c => c.Name == country.Capital).Select(s => s.Id).FirstOrDefault(),
                        Area = country.Area,
                        Population = country.Population,
                        Region = context.Regions.Local.Where(r => r.Name == country.Region).Select(s => s.Id).FirstOrDefault()
                    });
                }  
            }

            context.SaveChanges();
        }

        public static ObservableCollection<CountryAPI> GetCountry(ICountriesRepository context) =>
            ConverterCSTypes.ToObservableCollection(context.Countries.Join(
                context.Cities,
                cd => cd.Capital,
                c => c.Id,
                (cd, c) => new CountryAPI
                {
                    Name = cd.Name,
                    Alpha3Code = cd.Alpha3Code,
                    Area = cd.Area,
                    Capital = c.Name,
                    Population = cd.Population,
                    Region = context.Regions.Where(w => w.Id == cd.Region).Select(s => s.Name).FirstOrDefault()
                }));

        public static ObservableCollection<CountryAPI> GetCountry(HttpWebRequest httpWebRequest)
        {

            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "GET";

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var res = streamReader.ReadToEnd();

                    ObservableCollection<CountryAPI> result = JsonConvert.DeserializeObject<ObservableCollection<CountryAPI>>(res);
                    return result;
                }
            }
            catch (WebException e)
            {
                return new ObservableCollection<CountryAPI>();
            }
        }
    }
}
