using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;

using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using CurrencyData.Controllers;
using CurrencyData.Models;
using CurrencyConverter.Helpers;
using CurrencyConverter.Models;
using System.Reflection;
using CurrencyConverter.Services;
using Currency.Core.Helpers;
using Currency.Core.Services;

namespace CurrencyConverter.ViewModels
{
    /// <summary>
    /// Main view model
    /// </summary>
    public class MasterViewModel : BaseBindable
    {
        private readonly HttpClient httpClient = new HttpClient();
        private string URPCurrencyApi = "https://api.exchangeratesapi.io/latest";

        private readonly DialogConfirmService DialogService = new DialogConfirmService();

        #region "Constructor"
        /// <summary>
        /// Constructir, initialize events and variables
        /// </summary>
        public MasterViewModel()
        {
            URPCurrencyApi = ConfigurationManager.AppSettings["CurrencyAPI"].ToString();


            ResetCommand = new RelayCommand(this.Reset);
            FilterCommand = new RelayCommand(this.FilterByCurrency);
            RefreshCommand = new RelayCommand(this.LoadCurrencyRates);
            UpdateRateCommand = new RelayCommand(this.UpdateRate);

            CancelCommand = new RelayCommand(this.Cancel);
            DeleteCommand = new RelayCommand(this.Delete);
            SaveCommand = new RelayCommand(this.Save);

            Mediator.Instance.Register(this);

            LoadCurrencyRates();
        }

        ~MasterViewModel()
        {
            Mediator.Instance.Unregister(this);
        }
        #endregion

        #region "Properties"

        private List<IsoCurrencyModel> CachedData
        {
            get => AllCurrencies.Where(x => x.InUse == true).ToList();
        }

        private string _dataMessage;
        /// <summary>
        /// The message to be displayed in the ctatus bar
        /// </summary>
        public string DataMessage
        {
            get => _dataMessage;
            set => SetProperty(ref _dataMessage, value);
        }

        private List<IsoCurrencyModel> _allCurrencies;
        /// <summary>
        /// All available currencies
        /// </summary>
        public List<IsoCurrencyModel> AllCurrencies
        {
            get => _allCurrencies;
            set => SetProperty(ref _allCurrencies, value);
        }

        private List<IsoCurrencyModel> _inusecurrencies;
        /// <summary>
        /// List of currency in use to be displayed
        /// </summary>
        public List<IsoCurrencyModel> InUseCurrencies
        {
            get => _inusecurrencies;
            set => SetProperty(ref _inusecurrencies, value);
        }

        private IsoCurrencyModel _selectedUserCurrency;
        /// <summary>
        /// Selected user currency for editing
        /// </summary>
        public IsoCurrencyModel SelectedUserCurrency
        {
            get => _selectedUserCurrency;
            set
            {
                SetProperty(ref _selectedUserCurrency, value);
                this.RecordSelected = this.CanSave = (_selectedUserCurrency != null);
                this.EditorCurrency =
                    _selectedUserCurrency == null ? new CurrencyEditorModel() : new CurrencyEditorModel { ISOCode = _selectedUserCurrency.ISOCode, Currency = _selectedUserCurrency.Currency, Rate = _selectedUserCurrency.Rate, Base = _selectedUserCurrency.Base };
                this.ResetNew();
            }
        }

        private bool _iputValid;
        public bool InputValid
        {
            get => _iputValid;
            set => SetProperty(ref _iputValid, value);
        }

        private bool _isSelected;
        public bool RecordSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        private bool _isNew = true;
        public bool IsNew
        {
            get => _isNew;
            set => SetProperty(ref _isNew, value);
        }

        private CurrencyEditorModel _editorCurrency = new CurrencyEditorModel();
        /// <summary>
        /// Selected currency for editing purposes
        /// </summary>
        public CurrencyEditorModel EditorCurrency
        {
            get => _editorCurrency;
            set => SetProperty(ref _editorCurrency, value);
        }

        private IsoCurrencyModel _selectedFilterCurrency;
        /// <summary>
        /// Selected currency for filtering purposes
        /// </summary>
        public IsoCurrencyModel SelectedFilterCurrency
        {
            get => _selectedFilterCurrency;
            set => SetProperty(ref _selectedFilterCurrency, value);
        }

        private bool _isrefreshenabled;
        /// <summary>
        /// Enable/Disable reload button if the execution of the api service takes to long
        /// </summary>
        public bool IsRefreshEnabled
        {
            get => _isrefreshenabled;
            set => SetProperty(ref _isrefreshenabled, value);
        }

        private bool _canSave;
        /// <summary>
        /// Enable/Disable reload button if the execution of the api service takes to long
        /// </summary>
        public bool CanSave
        {
            get => _canSave;
            set => SetProperty(ref _canSave, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        private int? _inUseCount;
        [MediatorConnection("InUseCountMessage")]
        public int? InUseCount
        {
            get => _inUseCount;
            set => SetProperty(ref _inUseCount, value);
        }

        private string _rateDate;
        public string RateDate
        {
            get => _rateDate;
            set => SetProperty(ref _rateDate, value);
        }
        #endregion

        #region "Relays"
        public RelayCommand CancelCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand RefreshCommand { get; }
        public RelayCommand ResetCommand { get; }
        public RelayCommand FilterCommand { get; }
        public RelayCommand UpdateRateCommand { get; }
        #endregion

        #region "Private"
        public bool SaveCommand_CanExecute()
        {
            if (EditorCurrency != null && string.IsNullOrEmpty(EditorCurrency.ISOCode))
                return false;
            else
                return true;
        }

        private void Cancel()
        {
            this.SelectedUserCurrency = null;
            this.IsNew = true;
        }

        private void ResetNew()
        {
            this.IsNew = false;
        }

        private void Reset()
        {
            SelectedFilterCurrency = null;
            this.InUseCurrencies = CachedData;
            this.DataMessage = CurrencyCountMessage();
        }

        private void Save()
        {
            string key = string.Empty;
            if (SelectedUserCurrency != null)
            {
                key = SelectedUserCurrency.ISOCode;
            }
            IsoCurrencyModel currency = IsoCurrencyCodes.SaveCurrency(key, new IsoCurrencyModel { ISOCode = EditorCurrency.ISOCode, Currency = EditorCurrency.Currency, Rate = EditorCurrency.Rate, Base = EditorCurrency.Base });

            if (currency != null)
            {
                LoadCurrencyRates();
            }
            EditorCurrency = new CurrencyEditorModel();
            this.IsNew = true;
        }

        private void Delete()
        {
            if (DialogService.ConfirmDialog("Are you sure you want to delete this currency?"))
            {
                if (SelectedUserCurrency != null)
                {
                    string key = SelectedUserCurrency.ISOCode;
                    bool bDel = IsoCurrencyCodes.DeleteCurrency(key);
                    if (bDel)
                    {
                        LoadCurrencyRates();
                    }
                }
            }
        }

        private void UpdateRate()
        {
            string ISOCode = this.EditorCurrency.ISOCode;
            this.CanSave = !string.IsNullOrEmpty(ISOCode);

            IsoCurrencyModel currencyModel = null;
            var lst = AllCurrencies.Where(x => x.ISOCode == ISOCode).ToList();
            try
            {
                currencyModel = lst.SingleOrDefault();
            }
            catch
            {
                currencyModel = new IsoCurrencyModel(ISOCode);
            }

            if (currencyModel != null)
            {
                this.EditorCurrency.Currency = currencyModel.Currency;
                this.EditorCurrency.Rate = currencyModel.Rate;
            }
            else
            {
                this.EditorCurrency.Currency = string.Empty;
                this.EditorCurrency.Rate = 0;
            }
        }

        private void FilterByCurrency()
        {
            if (SelectedFilterCurrency != null)
            {
                this.InUseCurrencies = CachedData.Where(c => c.Currency == SelectedFilterCurrency.Currency).ToList();
                this.DataMessage = "FILTERED DATA (CURRENCY " + SelectedFilterCurrency.Currency + ") " + CurrencyCountMessage();
            }
        }

        /// <summary>
        /// Retrieve async data from api service
        /// </summary>
        private async void LoadCurrencyRates()
        {
            try
            {
                this.IsRefreshEnabled = false;

                try
                {
                    var response = await httpClient.GetStringAsync(URPCurrencyApi);
                    this.IsRefreshEnabled = true;

                    var deserializedCurencyRates = JsonConvert.DeserializeObject<CurrencyRates>(response);
                    this.RateDate = deserializedCurencyRates.RatesDate.ToShortDateString();
                    PushRates(deserializedCurencyRates);
                }
                catch (Exception ex)
                {
                    //log error and continue loading
                    Logger.Log.Error(ex);
                }

                //load currency last date from db
                this.AllCurrencies = IsoCurrencyCodes.AllCurrencies().ToList();
                this.InUseCurrencies = AllCurrencies.Where(x => x.InUse == true).ToList();

                this.DataMessage = CurrencyCountMessage();
                this.IsRefreshEnabled = true;
                this.InUseCount = this.InUseCurrencies?.Count;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                this.DataMessage = "An error has been occurred. Please try again.";
                this.IsRefreshEnabled = true;
            }
        }

        /// <summary>
        /// Push currency rates to DB for current day
        /// </summary>
        /// <param name="Rates"></param>
        private void PushRates(CurrencyRates Rates)
        {
            List<CurrencyRateModel> lstCurrency = new List<CurrencyRateModel>();
            //if data was already retrieved for the day skip loading
            if (IsoCurrencyCodes.CurrencyDateLoaded(Rates.RatesDate))
            {
                return;
            }
            Type myClassType = Rates.DailyRates.GetType();
            PropertyInfo[] properties = myClassType.GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                double nRate = 0;
                CurrencyRateModel currRate = new CurrencyRateModel(Rates.CurrencyBase, Rates.RatesDate);
                currRate.Currency = pi.Name;

                double.TryParse(pi.GetValue(Rates.DailyRates, null).ToString(), out nRate);
                currRate.Rate = nRate;
                lstCurrency.Add(currRate);
            }

            if (lstCurrency.Count > 0)
            {
                IsoCurrencyCodes.WriteToBase(lstCurrency);
            }

        }

        /// <summary>
        /// Displays a message with number of currency in the list
        /// </summary>
        /// <returns></returns>
        private string CurrencyCountMessage()
        {
            return this.InUseCurrencies.Count + " currencie(s) available.";
        }
        #endregion
    }
}
