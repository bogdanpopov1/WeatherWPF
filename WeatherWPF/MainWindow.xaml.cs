using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Win32;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
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
        private List<Weather> _listViewFilter = null;
        private List<Weather> _listViewSort = null;
        private Weather _selectedItem;
        private List<WeatherStatus> _statuses;
        private List<string> _sortItems = new List<string>()
        {
            "Сначала новые", "Сначала старые", "По возрастанию температуры", "По убыванию температуры",
        };
        public MainWindow()
        {
            InitializeComponent();

            _statuses = Data.DataContext.WeatherStatuses;
            _listView = Data.DataContext.Weathers;
            LstView.ItemsSource = _listView;
            Sort.ItemsSource = _sortItems;
            Filter.ItemsSource = _statuses;

            CalculteStates();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveBtn.IsEnabled = true;
            Info_SP.IsEnabled = true;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту информацию?",
                    "Удаление информации",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _listView.Remove(_selectedItem);
            }
            if (Sort.SelectedItem != null || Filter.SelectedItem != null)
            {
                Sort_DropDownClosed(sender, e);
                Filter_DropDownClosed(sender, e);
            }
            else
            {
                LstView.ItemsSource = _listView;
                LstView.Items.Refresh();
            }
            CancelBtn_Click(sender, e);
            CalculteStates();
        }



        private void LstView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedItem = (Weather)LstView.SelectedItem;

            if (_selectedItem == null)
                return;

            Date_TB.Text = _selectedItem.Date.ToShortDateString();
            Temp_TB.Text = _selectedItem.Temp.ToString();
            Status_TB.Text = _selectedItem.Status.Name;
            WindSpeed_TB.Text = _selectedItem.WindSpeed.ToString();
            RainAmount_TB.Text = _selectedItem.RainAmount.ToString();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(Date_TB.Text) || String.IsNullOrEmpty(Temp_TB.Text) || String.IsNullOrEmpty(Status_TB.Text) || String.IsNullOrEmpty(WindSpeed_TB.Text) || String.IsNullOrEmpty(RainAmount_TB.Text))
                    return;

                var newDate = DateTime.Parse(Date_TB.Text.Trim());
                var newTemp = Convert.ToInt32(Temp_TB.Text.Trim());
                WeatherStatus newStatus = null;
                foreach (var s in _statuses)
                {
                    if (s.Name.ToLower() == Status_TB.Text.ToLower().Trim())
                        newStatus = s;
                }
                var newWindSpeed = Convert.ToInt32(WindSpeed_TB.Text.Trim());
                var newRainAmount = Convert.ToDouble(RainAmount_TB.Text.Trim());

                if (_selectedItem != null)
                {
                    _selectedItem.Date = newDate;
                    _selectedItem.Temp = newTemp;
                    _selectedItem.Status = newStatus;
                    _selectedItem.WindSpeed = newWindSpeed;
                    _selectedItem.RainAmount = newRainAmount;

                    if (Sort.SelectedItem != null || Filter.SelectedItem != null)
                    {
                        Sort_DropDownClosed(sender, e);
                        Filter_DropDownClosed(sender, e);
                    }
                    else
                    {
                        LstView.ItemsSource = _listView;
                        LstView.Items.Refresh();
                    }
                    CancelBtn_Click(sender, e);
                    CalculteStates();
                    return;
                }

                Data.DataContext.Weathers.Add(new Weather(newDate, newTemp, newStatus, newWindSpeed, newRainAmount));
                _listView = Data.DataContext.Weathers;
                if (Sort.SelectedItem != null || Filter.SelectedItem != null)
                {
                    Sort_DropDownClosed(sender, e);
                    Filter_DropDownClosed(sender, e);
                    CancelBtn_Click(sender, e);
                    CalculteStates();
                }
                else
                {
                    LstView.ItemsSource = _listView;
                    LstView.Items.Refresh();
                    CancelBtn_Click(sender, e);
                    CalculteStates();
                }

            }
            catch
            {
                MessageBox.Show("Некорректные данные!");
            }

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            _selectedItem = null;
            Date_TB.Text = "";
            Temp_TB.Text = "";
            Status_TB.Text = "";
            WindSpeed_TB.Text = "";
            RainAmount_TB.Text = "";
            SaveBtn.IsEnabled = false;
            Info_SP.IsEnabled = false;
        }

        private void Sort_DropDownClosed(object sender, EventArgs e)
        {
            var type = Sort.SelectedItem;

            if (type == null)
                return;

            if (_listViewFilter != null)
                _listViewSort = _listViewFilter;

            if (type == "Сначала старые")
                _listViewSort = _listView.OrderBy(x => x.Date).ToList();
            else if (type == "Сначала новые")
                _listViewSort = _listView.OrderByDescending(x => x.Date).ToList();
            else if (type == "По возрастанию температуры")
                _listViewSort = _listView.OrderBy(x => x.Temp).ToList();
            else if (type == "По убыванию температуры")
                _listViewSort = _listView.OrderByDescending(x => x.Temp).ToList();

            LstView.ItemsSource = _listViewSort;
            LstView.Items.Refresh();
        }

        private void Filter_DropDownClosed(object sender, EventArgs e)
        {
            var selectedStatus = Filter.SelectedItem;

            if (selectedStatus == null)
                return;

            _listViewFilter = Data.DataContext.Weathers.Where(x => x.Status == selectedStatus).ToList();
            LstView.ItemsSource = _listViewFilter;
            LstView.Items.Refresh();
            CalculteStates();
        }

        private void SortResetBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_listViewFilter == null) 
                _listView = Data.DataContext.Weathers;
            else
                _listView = _listViewFilter;
            _listViewSort = null;
            Sort.SelectedItem = null;
            LstView.ItemsSource = _listView;
            LstView.Items.Refresh();
        }

        private void FilterResetBtn_Click(object sender, RoutedEventArgs e)
        {
            Filter.SelectedItem = null;
            _listViewFilter = null;
            _listView = Data.DataContext.Weathers;
            Sort_DropDownClosed(sender, e);
            LstView.ItemsSource = _listView;
            LstView.Items.Refresh();
            CalculteStates();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            _selectedItem = null;
            Date_TB.Text = "";
            Temp_TB.Text = "";
            Status_TB.Text = "";
            WindSpeed_TB.Text = "";
            RainAmount_TB.Text = "";
            SaveBtn.IsEnabled = true;
            Info_SP.IsEnabled = true;
        }

        private void CalculteStates()
        {
            if (_listViewFilter != null)
                _listView = _listViewFilter;

            if (_listView == null)
                return;

            MediumTemp_TB.Text = Math.Round(_listView.Average(x => x.Temp)).ToString() + " ℃";

            int maxTemp = _listView.Max(x => x.Temp);
            MaxTemp_TB.Text = maxTemp.ToString() + " ℃";
            List<string> maxTempDates = new List<string>();
            foreach (var t in _listView)
            {
                if (t.Temp == maxTemp)
                    maxTempDates.Add(t.Date.ToShortDateString());
            }
            MaxTempDays_TB.Text = string.Join("\n", maxTempDates);

            int minTemp = _listView.Min(x => x.Temp);
            MinTemp_TB.Text = minTemp.ToString() + " ℃";
            List<string> minTempDates = new List<string>();
            foreach (var t in _listView)
            {
                if (t.Temp == minTemp)
                    minTempDates.Add(t.Date.ToShortDateString());
            }
            MinTempDays_TB.Text = string.Join("\n", minTempDates);

            List<string> tempDrop = new List<string>();
            Weather itemDrop = _listView[0];
            for (var i = 2; i <= _listView.Count; i++)
            {
                if (itemDrop.Temp - _listView[i - 1].Temp > 9)
                {
                    tempDrop.Add(itemDrop.Date.ToShortDateString());
                    tempDrop.Add(" → ");
                    tempDrop.Add(_listView[i - 1].Date.ToShortDateString());
                    tempDrop.Add("   (");
                    tempDrop.Add(itemDrop.Temp.ToString());
                    tempDrop.Add("℃");
                    tempDrop.Add(" → ");
                    tempDrop.Add(_listView[i - 1].Temp.ToString());
                    tempDrop.Add("℃");
                    tempDrop.Add(")");
                    if (i < _listView.Count)
                        tempDrop.Add("\n");
                }
                itemDrop = _listView[i - 1];
            }
            TempDrop_TB.Text = string.Join("", tempDrop);

            List<string> tempRise = new List<string>();
            Weather itemRise = _listView[0];
            for (var i = 2; i <= _listView.Count; i++)
            {
                if (_listView[i - 1].Temp - itemRise.Temp > 9)
                {
                    tempRise.Add(_listView[i - 2].Date.ToShortDateString());
                    tempRise.Add(" → ");
                    tempRise.Add(_listView[i - 1].Date.ToShortDateString());
                    tempRise.Add("   (");
                    tempRise.Add(itemRise.Temp.ToString());
                    tempRise.Add("℃");
                    tempRise.Add(" → ");
                    tempRise.Add(_listView[i - 1].Temp.ToString());
                    tempRise.Add("℃");
                    tempRise.Add(")");
                    if (i < _listView.Count)
                        tempRise.Add("\n");
                }
                itemRise = _listView[i - 1];
            }
            TempRise_TB.Text = string.Join("", tempRise);
        }

        private void SaveAsTXTBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.FileName = "weather";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"{"Дата".PadRight(25)}{"Температура, ℃".PadRight(18)}{"Облачность".PadRight(17)}{"Ветер, м/с".PadRight(15)}{"Осадки, мм".PadRight(15)}");

                    foreach (var item in LstView.Items)
                    {
                        Weather data = item as Weather;
                        if (data != null)
                        {
                            writer.WriteLine($"{data.Date.ToString().PadRight(25)}{data.Temp.ToString().PadRight(18)}{data.Status.Name.PadRight(17)}{data.WindSpeed.ToString().PadRight(15)}{data.RainAmount.ToString().PadRight(15)}");
                        }
                    }

                    writer.WriteLine("\n\nПОКАЗАТЕЛИ");
                    writer.WriteLine("\nСредняя температура: " + MediumTemp_TB.Text);
                    writer.WriteLine("\nМаксимальная температура: " + MaxTemp_TB.Text);
                    writer.WriteLine("Дни максимальной температуры: " + MaxTempDays_TB.Text);
                    writer.WriteLine("\nМинимальная температура: " + MinTemp_TB.Text);
                    writer.WriteLine("Дни минимальной температуры: " + MinTempDays_TB.Text);
                    writer.WriteLine("\nАномальные спады температуры: " + TempDrop_TB.Text);
                    writer.WriteLine("Аномальные подъемы температуры: " + TempRise_TB.Text);
                }

                MessageBox.Show("Данные успешно сохранены!");
            }
        }
    }
}
