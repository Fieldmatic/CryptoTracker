using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public SeriesCollection Series { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public string[] Labels { get; set; }

        public string type;
        public ObservableCollection<Data> DataValues { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            PopulateComboBoxes();
            Series = new SeriesCollection();
            low.IsChecked = true;
            
        }

        private void CheckedRDB(object sender, RoutedEventArgs e)
        {
            Dictionary<string, double> data = GetDataFromApi(sender);

            //Tabela
            DataValues = new ObservableCollection<Data>();
            PopulateDataCollection(data);


            data = data.Reverse().ToDictionary(x => x.Key, x => x.Value);

            //Chart
            ClearSeriesCollection();

            Series.Add(new LineSeries
            {
                Title = Assets.SelectedValue.ToString(),
                Values = new ChartValues<double>(data.Values),
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 9

            });
            Labels = data.Keys.ToArray();
            DataContext = Labels;
            DataContext = this;

        }

        private Dictionary<string, double> GetDataFromApi(object sender)
        {
            string selectedAsset = Assets.SelectedValue.ToString();
            string selectedMarket = Markets.SelectedValue.ToString();
            string selectedPeriod = Periods.SelectedValue.ToString();
            string viewType = (sender as RadioButton).Name;

            Dictionary<string, double> data = api.getData(selectedPeriod, selectedAsset, selectedMarket, viewType);
            return data;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var radioButtons = LogicalTreeHelper.GetChildren(Menu).OfType<RadioButton>();
            var selectedRdb = radioButtons.FirstOrDefault(x => (bool)x.IsChecked);
             if (selectedRdb != null) selectedRdb.IsChecked = false;
        }

        private void ClearSeriesCollection()
        {
            if (Series != null && Series.Count > 0)
            {
                Series.Clear();
            }
        }

        private void PopulateDataCollection(Dictionary<string, double> data)
        {
            foreach (var item in data)
            {
                DataValues.Add(new Data(item.Key, item.Value));
            }
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
