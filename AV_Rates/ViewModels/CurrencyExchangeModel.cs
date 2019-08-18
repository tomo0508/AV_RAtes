using AV_Rates.NotifyHelper;

namespace AV_Rates.ViewModels
{
    public class CurrencyExchangeModel : Notifier
    {
        private Currency from;
        private Currency to;

        public Currency From
        {
            get { return from; }
            set
            {
                if (from == null || from.Code != value.Code)
                {
                    from = value;
                    if (from != null && to != null)
                    {
                        OnPropertyChange("From");
                    }
                }
            }
        }

        public Currency To
        {
            get { return to; }
            set
            {
                if (to == null || to.Code != value.Code)
                {
                    to = value;
                    if (from != null && to != null)
                    {
                        OnPropertyChange("To");
                    }
                }
            }
        }
    }
}