using AV_Rates.Enums;
using AV_Rates.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AV_Rates.AvailableCurrencies
{
    public static class CodesHelper
    {
        public static IEnumerable<Currency> ReadCurrencies(string pathToCsv, CurrencyType currencyType)
        {
            return File.ReadAllLines(pathToCsv)
                  .Skip(1)
                  .Select(x => x.Split(','))
                  .Select(x => new Currency { Code = x[0], Name = x[1], Type = currencyType });
        }
    }
}