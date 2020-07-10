using System.Collections.ObjectModel;
using System.Linq;
using Prism.Mvvm;
using Currency.Core.Model;
using Currency.Core.Services;
using CurrencyData.Models;

namespace CurrencyConverter.ViewModels
{
    public class DataGridViewModel : BindableBase
    {
        private ObservableCollection<IsoCountryCodesModel> _countries;
        public ObservableCollection<IsoCountryCodesModel> Countries
        {
            get { return _countries; }
            set { SetProperty(ref _countries, value); }
        }

        public DataGridViewModel(ICountryService service)
        {
            this.Countries = new ObservableCollection<IsoCountryCodesModel>();
            this.Countries.AddRange(service.GetAllCountries().OrderBy(c => c.Country));
        }
    }
}
