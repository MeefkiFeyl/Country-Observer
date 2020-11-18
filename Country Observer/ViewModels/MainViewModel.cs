using Country_Observer.Command;
using Country_Observer.Data;
using Country_Observer.Models;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace Country_Observer.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Properties
        public CODBContext Context { get; set; }

        public Visibility MessageVisibility { get; set; }
        public Visibility SaveButtonVisibility { get; set; }
        public string MessageText { get; set; }
        public string MessageColor { get; set; }

        private string _country;
        public string _Country
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CountryAPI> countries;
        public ObservableCollection<CountryAPI> Countries
        {
            get => countries;
            set
            {
                countries = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public MainViewModel()
        {
            Countries = new ObservableCollection<CountryAPI>();
            MessageVisibility = Visibility.Hidden;
            SaveButtonVisibility = Visibility.Hidden;
            Context = new CODBContext();

            Context.Messages.Load();
            Context.Cities.Load();
            Context.Regions.Load();
            Context.Countries.Load();
        }

        #region Commands

        public ICommand GetCountryDb => new DelegateCommand(obj =>
        {
            Countries = QueryHelper.GetCountry(new CountriesRepository(Context));
            OnPropertyChanged(nameof(Countries));

            MessageManager(false, "green", Context.Messages.Find(3).Value);
        });

        public ICommand AddCountriesToDb => new DelegateCommand(obj =>
        {
            if (Countries == null) return;

            QueryHelper.SaveCountry(Context, Countries);

            MessageManager(false, "red", Context.Messages.Find(5).Value);
        });

        public ICommand GetCountryAPI => new DelegateCommand(obj =>
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create($"https://restcountries.eu/rest/v2/name/{_Country}");

            Countries = QueryHelper.GetCountry(httpWebRequest);

            MessageManager(Countries.Count != 0, "red", Context.Messages.Find(1).Value);
        });

        #endregion

        #region Helps

        void MessageManager(bool value, string color, string text)
        {
            if (value)
            {
                MessageVisibility = Visibility.Hidden;
                SaveButtonVisibility = Visibility.Visible;
            }
            else
            {
                MessageColor = color;
                MessageText = text;
                MessageVisibility = Visibility.Visible;
                SaveButtonVisibility = Visibility.Hidden;
            }
            OnPropertyChanged(nameof(MessageColor));
            OnPropertyChanged(nameof(MessageText));
            OnPropertyChanged(nameof(MessageVisibility));
            OnPropertyChanged(nameof(SaveButtonVisibility));
        }

        #endregion
    }
}
