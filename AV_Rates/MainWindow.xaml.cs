using AV_Rates.API;
using AV_Rates.AvailableCurrencies;
using AV_Rates.Constants;
using AV_Rates.Enums;
using AV_Rates.Parsers;
using AV_Rates.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace AV_Rates
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private CallingApi apiCaller;
        private IParser<string, List<CryptoCurrencyDaily>> parser;

        public List<Currency> PhysicalCurrencies { get; private set; }
        public List<Currency> DigitalCurrencies { get; private set; }
        public List<Currency> Currencies { get; private set; }

        public CurrencyExchangeModel ExchangeModel { get; set; }
        public CurrencyDailyModel DailyModel { get; set; }

        public ObservableCollection<CryptoCurrencyDaily> CryptoDailyDisplays { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            InitializeObjects();
        }

        private void InitializeObjects()
        {
            apiCaller = new CallingApi(Const.API_KEY);

            PhysicalCurrencies = CodesHelper.ReadCurrencies(Const.PATH_TO_PHYSICAL, CurrencyType.Physical).ToList();
            DigitalCurrencies = CodesHelper.ReadCurrencies(Const.PATH_TO_DIGITAL, CurrencyType.Digital).ToList();
            Currencies = PhysicalCurrencies.Concat(DigitalCurrencies).ToList();

            DataContext = this;

            ExchangeModel = new CurrencyExchangeModel();
            ExchangeModel.PropertyChanged += ExchangeModel_PropertyChanged;

            DailyModel = new CurrencyDailyModel();
            DailyModel.PropertyChanged += DailyModel_PropertyChanged;

            parser = new JsonParser();
            CryptoDailyDisplays = new ObservableCollection<CryptoCurrencyDaily>();
        }

        private async void DailyModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var converted = sender as CurrencyDailyModel;

            lblDaily.Content = "Loading...";

            CryptoDailyDisplays.Clear();
            var daily = await apiCaller.GetCurrencyDaily(converted);

            var objectsToDisplay = parser.ParseCurrency(daily);
            foreach (var obj in objectsToDisplay)
            {
                CryptoDailyDisplays.Add(obj);
            }

            if (objectsToDisplay.Count == 0)
                lblDaily.Content = "Currently not available.";
            else
                lblDaily.Content = "";

        }

        private async void ExchangeModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var converted = sender as CurrencyExchangeModel;
            tbExchangeResult.Text = "Loading...";
            var exchange = await apiCaller.GetCurrencyExchange(converted);

            tbExchangeResult.Text = exchange;
        }


    }
}
