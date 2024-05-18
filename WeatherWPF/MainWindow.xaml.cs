using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherWPF.Data;

namespace WeatherWPF
{
    public partial class MainWindow : Window
    {
        private List<Weather> _listView;
        public MainWindow()
        {
            InitializeComponent();

            _listView = Data.DataContext.Weathers;
            LstView.ItemsSource = _listView;        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
