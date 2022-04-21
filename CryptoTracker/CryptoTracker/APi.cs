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
    }
}
