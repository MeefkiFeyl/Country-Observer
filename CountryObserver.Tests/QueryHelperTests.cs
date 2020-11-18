using Country_Observer.Data;
using Country_Observer.Models;
using Country_Observer.Converters;
using Moq;
using System.Net;
using System.Linq;
using Xunit;
using System.Collections.ObjectModel;
using CountryObserver.Tests.Data;

namespace CountryObserver.Tests
{
    public class QueryHelperTests
    {
        [Fact]
        public void Can_Get_Countries_With_API()
        {
            //Arrrange
            HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create($"https://restcountries.eu/rest/v2/name/Ukraine");
            HttpWebRequest httpWebRequest2 = (HttpWebRequest)WebRequest.Create($"https://restcountries.eu/rest/v2/name/adfsdf");
            HttpWebRequest httpWebRequest3 = (HttpWebRequest)WebRequest.Create($"https://restcountries.eu/rest/v2/name/");

            //Act
            var result1 = QueryHelper.GetCountry(httpWebRequest1);
            var result2 = QueryHelper.GetCountry(httpWebRequest2);
            var result3 = QueryHelper.GetCountry(httpWebRequest3);

            //Assert
            Assert.Equal("Ukraine", result1[0].Name);
            Assert.True(result1.Count == 1);
            Assert.True(result2.Count == 0);
            Assert.True(result3.Count == 0);
        }

        [Fact]
        public void Can_Get_Countries_From_DB()
        {
            //Arrange
            Mock<ICountriesRepository> mock = new Mock<ICountriesRepository>();
            mock.Setup(m => m.Cities).Returns((new City[]
            {
                new City { Id = 1, Name = "Ct1" },
                new City { Id = 2, Name = "Ct2" },
                new City { Id = 3, Name = "Ct3" },
                new City { Id = 4, Name = "Ct4" }
            }).AsQueryable());
            mock.Setup(m => m.Regions).Returns((new Region[] 
            { 
                new Region { Id = 1, Name = "R1" },
                new Region { Id = 2, Name = "R2" },
                new Region { Id = 3, Name = "R3" }
            }).AsQueryable());
            mock.Setup(m => m.Countries).Returns((new CountryDb[] 
            { 
                new CountryDb 
                { 
                    Id = 1, 
                    Name = "C1", 
                    Region = mock.Object.Regions.Where(w => w.Id == 1).Select(s => s.Id).FirstOrDefault(), 
                    Capital = mock.Object.Cities.Where(w => w.Id == 1).Select(s => s.Id).FirstOrDefault() 
                },
                new CountryDb 
                { 
                    Id = 2, 
                    Name = "C2", 
                    Region = mock.Object.Regions.Where(w => w.Id == 2).Select(s => s.Id).FirstOrDefault(), 
                    Capital = mock.Object.Cities.Where(w => w.Id == 2).Select(s => s.Id).FirstOrDefault() 
                },
                new CountryDb 
                { 
                    Id = 3, 
                    Name = "C3", 
                    Region = mock.Object.Regions.Where(w => w.Id == 3).Select(s => s.Id).FirstOrDefault(), 
                    Capital = mock.Object.Cities.Where(w => w.Id == 3).Select(s => s.Id).FirstOrDefault() 
                },
                new CountryDb 
                { 
                    Id = 4, 
                    Name = "C4", 
                    Region = mock.Object.Regions.Where(w => w.Id == 1).Select(s => s.Id).FirstOrDefault(), 
                    Capital = mock.Object.Cities.Where(w => w.Id == 4).Select(s => s.Id).FirstOrDefault() 
                }
            }).AsQueryable());
            //Act
            var result = QueryHelper.GetCountry(mock.Object);

            //Assetr
            Assert.True(result.Count == 4);
            Assert.Equal(mock.Object.Regions.Where(w => w.Id == 1).Select(s => s.Name).FirstOrDefault(), result.Where(w => w.Name == "C1").Select(s => s.Region).FirstOrDefault());
            Assert.Equal(mock.Object.Regions.Where(w => w.Id == 2).Select(s => s.Name).FirstOrDefault(), result.Where(w => w.Name == "C2").Select(s => s.Region).FirstOrDefault());
            Assert.Equal(mock.Object.Cities.Where(w => w.Id == 3).Select(s => s.Name).FirstOrDefault(), result.Where(w => w.Name == "C3").Select(s => s.Capital).FirstOrDefault());
            Assert.Equal(mock.Object.Cities.Where(w => w.Id == 4).Select(s => s.Name).FirstOrDefault(), result.Where(w => w.Name == "C4").Select(s => s.Capital).FirstOrDefault());
        }
    }
}
