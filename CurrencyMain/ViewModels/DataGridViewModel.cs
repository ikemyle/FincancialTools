using System.Collections.ObjectModel;
using Prism.Mvvm;
using Currency.Core.Services;
using CurrencyData.Models;

namespace Currency.ViewModels
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
            this.Countries.AddRange(service.GetAllCountries());
        }
    }
}
