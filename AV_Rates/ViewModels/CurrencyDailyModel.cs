using AV_Rates.NotifyHelper;

namespace AV_Rates.ViewModels
{
    public class CurrencyDailyModel : Notifier
    {

        private Currency symbol;
        private Currency market;

        public Currency Symbol
        {
            get { return symbol; }
            set
            {
                if (symbol == null || symbol.Code != value.Code)
                {
                    symbol = value;
                    if (symbol != null && market != null)
                    {
                        OnPropertyChange("Symbol");
                    }
                }
            }
        }

        public Currency Market
        {
            get { return market; }
            set
            {
                if (market == null || market.Code != value.Code)
                {
                    market = value;
                    if (symbol != null && market != null)
                    {
                        OnPropertyChange("Market");
                    }
                }
            }
        }
    }
}
