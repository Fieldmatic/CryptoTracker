using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryptoTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private APi api = new APi();
        public MainWindow()
        {
            InitializeComponent();
            PopulateComboBoxes();
            Dictionary<string, double> data = api.getData("5min", "BTC", "EUR", "low");
        }

        private void PopulateComboBoxes()
        {
            PopulateDigitalCurrencies();
            PopulatePhysicalCurrencies();
            PopulatePeriods();

        }

        private void PopulateDigitalCurrencies()
        {
            Assets.ItemsSource = api.getDigitalCurrencies();
            Assets.SelectedIndex = 83;

        }

        private void PopulatePhysicalCurrencies()
        {
            Markets.ItemsSource = api.getPhysicalCurrencies();
            Markets.SelectedIndex = 42;

        }

        private void PopulatePeriods()
        {
            Periods.ItemsSource = new string[] { "1min", "5min","15min", "30min", "60min", "Day","Week", "Month"};
            Periods.SelectedIndex = 6;
        }
    }
}
