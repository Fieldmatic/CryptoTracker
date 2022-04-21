using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CryptoTracker
{
    public class APi
    {
        private readonly string[] digitalCurrencies = { "1ST", "2GIVE", "808", "AAVE", "ABY", "AC", "ACT", "ADA", "ADT", "ADX", "AE", "AEON", "AGI", "AGRS", "AI", "AID", "AION", "AIR", "AKY", "ALGO", "ALIS", "AMBER", "AMP", "AMPL", "ANC", "ANT", "APPC", "APX", "ARDR", "ARK", "ARN", "AST", "ATB", "ATM", "ATOM", "ATS", "AUR", "AVAX", "AVT", "B3", "BAND", "BAT", "BAY", "BBR", "BCAP", "BCC", "BCD", "BCH", "BCN", "BCPT", "BCX", "BCY", "BDL", "BEE", "BELA", "BET", "BFT", "BIS", "BITB", "BITBTC", "BITCNY", "BITEUR", "BITGOLD", "BITSILVER", "BITUSD", "BIX", "BLITZ", "BLK", "BLN", "BLOCK", "BLZ", "BMC", "BNB", "BNT", "BNTY", "BOST", "BOT", "BQ", "BRD", "BRK", "BRX", "BSV", "BTA", "BTC", "BTCB", "BTCD", "BTCP", "BTG", "BTM", "BTS", "BTSR", "BTT", "BTX", "BURST", "BUSD", "BUZZ", "BYC", "BYTOM", "C20", "CAKE", "CANN", "CAT"};
        private readonly string[] physicalCurrencies = { "AED", "AFN", "ALL", "AMD", "ANG", "AOA", "ARS", "AUD", "AWG", "AZN", "BAM", "BBD", "BDT", "BGN", "BHD", "BIF", "BMD", "BND", "BOB", "BRL", "BSD", "BTN", "BWP", "BZD", "CAD", "CDF", "CHF", "CLF", "CLP", "CNH", "CNY", "COP", "CUP", "CVE", "CZK", "DJF", "DKK", "DOP", "DZD", "EGP", "ERN", "ETB", "EUR", "FJD", "FKP", "GBP", "GEL", "GHS", "GIP", "GMD", "GNF", "GTQ", "GYD", "HKD", "HNL", "HRK", "HTG", "HUF", "ICP", "IDR", "ILS", "INR", "IQD", "IRR", "ISK", "JEP", "JMD", "JOD", "JPY", "KES", "KGS", "KHR", "KMF", "KPW", "KRW", "KWD", "KYD", "KZT", "LAK", "LBP", "LKR", "LRD", "LSL", "LYD", "MAD", "MDL", "MGA", "MKD", "MMK", "MNT", "MOP", "MRO", "MRU", "MUR", "MVR", "MWK", "MXN", "MYR", "MZN", "NAD", "NGN", "NOK", "NPR", "NZD", "OMR", "PAB", "PEN", "PGK", "PHP", "PKR", "PLN", "PYG", "QAR", "RON", "RSD", "RUB", "RUR", "RWF", "SAR", "SBD", "SCR", "SDG", "SDR", "SEK", "SGD", "SHP", "SLL", "SOS", "SRD", "SYP", "SZL", "THB", "TJS", "TMT", "TND", "TOP", "TRY", "TTD", "TWD", "TZS", "UAH", "UGX", "USD", "UYU", "UZS", "VND", "VUV", "WST", "XAF", "XCD", "XDR", "XOF", "XPF", "YER", "ZAR", "ZMW", "ZWL"};
        public dynamic getData (string function, string crypto, string market)
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function={function}&symbol={crypto}&market={market}&apikey=Q4323R2VPQQ6J7QQ";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                dynamic json_data = js.Deserialize(client.DownloadString(queryUri), typeof(object));
                return json_data;
            }
        }

        public string[] getDigitalCurrencies()
        {
            return digitalCurrencies;
        }

        public string[] getPhysicalCurrencies()
        {
            return physicalCurrencies;
        }
    }
}
