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
        
        public Dictionary<string, double> getData (string period, string crypto, string market, string type)
        {
            string function = getFunctionByPeriod(period);
            Uri queryUri = new Uri(getQueryUrl(function, period, crypto, market, type));
            Dictionary<string, double> result = new Dictionary<string, double>();
            using (WebClient client = new WebClient())
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                dynamic json_data = js.Deserialize(client.DownloadString(queryUri), typeof(object));
                Dictionary<string, object> data_dict = (Dictionary<string, object>)json_data;
                try
                {
                    Dictionary<string, object> data = (Dictionary<string, object>)data_dict[data_dict.ElementAt(1).Key];
                    result = parseData(data, type, market, function);
                    return result;

                }
                catch(ArgumentOutOfRangeException)
                {
                    return result;
                    
                }            
            }
        }

        private string getQueryUrl(string function, string period, string crypto, string market, string type)
        {
            if (function == "CRYPTO_INTRADAY")
                return $"https://www.alphavantage.co/query?function={function}&symbol={crypto}&market={market}&interval={period}&apikey=Q4323R2VPQQ6J7QQ";
            else
                return $"https://www.alphavantage.co/query?function={function}&symbol={crypto}&market={market}&apikey=Q4323R2VPQQ6J7QQ";

        }

        private Dictionary<string,double> parseData(Dictionary<string,object> data, string type, string market, string function)
        {
            Dictionary<string,double> result = new Dictionary<string, double>();
            foreach (string key in data.Keys)
            {
                Dictionary<string, object> subData = (Dictionary<string, object>)data[key];            
                foreach (string data_type in subData.Keys)
                {
                    if (function != "CRYPTO_INTRADAY" && data_type.Contains(type) && data_type.Contains(market))
                            result[key] = Double.Parse(subData[data_type].ToString());

                    else if (data_type.Contains(type) && function == "CRYPTO_INTRADAY")
                            result[key] = Double.Parse(subData[data_type].ToString());
                }

            }
            return result;
        }


        private string getFunctionByPeriod(string period)
        {
            switch (period)
            {
                case "Day": return "DIGITAL_CURRENCY_DAILY";
                case "Week": return "DIGITAL_CURRENCY_WEEKLY";
                case "Month": return "DIGITAL_CURRENCY_MONTHLY";
                default: return "CRYPTO_INTRADAY";
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
