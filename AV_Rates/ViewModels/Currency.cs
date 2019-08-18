using AV_Rates.Enums;

namespace AV_Rates.ViewModels
{
    public class Currency
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public CurrencyType Type { get; set; }
    }
}
