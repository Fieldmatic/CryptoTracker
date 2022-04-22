using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker
{
    public class Data
    {
        public string Date { get;set; }
        public double Value { get;set; }

        public Data() { }

        public Data(string date, double value)
        {
            Date = date;
            Value = value;
        }
    }
}
