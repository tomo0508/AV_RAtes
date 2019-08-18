using AV_Rates.ViewModels;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace AV_Rates.Parsers
{
    public class JsonParser : IParser<string, List<CryptoCurrencyDaily>>
    {

        public List<CryptoCurrencyDaily> ParseCurrency(string input)
        {
            var obj = JObject.Parse(input);
            var seriesCrypto = obj["Time Series (Digital Currency Daily)"];

            if (seriesCrypto == null)
            {
                return new List<CryptoCurrencyDaily>();
            }

            return seriesCrypto.Select(x => new CryptoCurrencyDaily
            {
                DateTime = ((JProperty)x).Name,
                Price = (double)x.Values().Select(y => (JProperty)y).First(y => y.Name.Contains("open")).Value,
                Volume = (double)x.Values().Select(y => (JProperty)y).First(y => y.Name.Contains("volume")).Value,
                MarketCap = (double)x.Values().Select(y => (JProperty)y).First(y => y.Name.Contains("market cap")).Value
            }).ToList();
        }

    }
}