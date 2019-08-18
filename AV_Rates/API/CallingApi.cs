using AV_Rates.Constants;
using AV_Rates.ViewModels;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AV_Rates.API
{
    public class CallingApi
    { 
        private readonly string EXCHANGE_URL = Const.BASE_URL + "?function=CURRENCY_EXCHANGE_RATE&from_currency={0}&to_currency={1}&apikey={2}";
        private readonly string CURRENCY_DAILY_URL = Const.BASE_URL + "?function=DIGITAL_CURRENCY_DAILY&symbol={0}&market={1}&apikey={2}";
        private string apiKey;

        
        public CallingApi(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task<string> GetCurrency(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {

                return await reader.ReadToEndAsync();
            }
        }

        public async Task<string> GetCurrencyExchange(CurrencyExchangeModel model)
        {
            var formedUrl = string.Format(this.EXCHANGE_URL, model.From.Code, model.To.Code, this.apiKey);
            return await this.GetCurrency(formedUrl);
        }

        public async Task<string> GetCurrencyDaily(CurrencyDailyModel model)
        {
            var formedUrl = string.Format(this.CURRENCY_DAILY_URL, model.Symbol.Code, model.Market.Code, this.apiKey);
            return await this.GetCurrency(formedUrl);
        }
    }
}
