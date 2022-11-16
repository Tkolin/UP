using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Shapes;

namespace Uper
{
    /// <summary>
    /// Логика взаимодействия для ServisWindow.xaml
    /// </summary>
    public partial class ServisWindow : Window
    {
        public DataRowView rows { get; set; }
        public DataTable clientServiseTable { get; set; }   

        List<UserControl> listServis = new List<UserControl>();
        public ServisWindow(DataRowView _userRows, DataTable _serviseRows)
        {
            InitializeComponent();
            this.rows = _userRows;
            this.clientServiseTable = _serviseRows;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TimerStarter();

            FIOLable.Text = (rows.Row["FirstName"].ToString() +" "+ rows.Row["LastName"].ToString() + " "+ rows.Row["Patronymic"].ToString());
  
            IdLable.Text = rows.Row["id"].ToString();
            string date = rows.Row["Birthday"].ToString();
            BirthDateLable.Text = date.Substring(0, date.Length-8);
            if (rows.Row["GenderCode"].ToString() == "1")
                GenderLable.Text = "Мужчина";
            else
                GenderLable.Text = "Женщина";     
            try
            {
                Uri dts = new Uri(rows.Row["PhotoPath"].ToString().Replace(" ", ""), UriKind.Relative);
                imgLable.Source = (new BitmapImage(dts));
            }
            catch
            {
                MessageBox.Show("ошибка изображения");
            }

            da.ItemsSource = clientServiseTable.DefaultView;

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private void allBtn_Click(object sender, RoutedEventArgs e)
        //{

        //}
        private void TimerStarter()
        {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { DataTimerLable.Text = DateTime.Now.ToString(); };
            timer.Start();

        }
    }
}
