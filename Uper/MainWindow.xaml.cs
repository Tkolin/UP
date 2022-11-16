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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Collections.ObjectModel;
namespace Uper
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString;
        SqlDataAdapter adapter;
        SqlDataAdapter _adapter;
        DataTable clientTable;
        DataTable viewClientTable;

        DataTable serviseTable;

        public DataRow newRows;

        public DataRowView _returnRows;

        public int genderIndex;
        public int filterBirth;
        public int filterName;
        public int fitterDate;

        //ObservableCollection<Client> ClientDB;
        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DBConnects"].ConnectionString;
        }
        private void UpdateDB()
        {
            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter);
            adapter.Update(clientTable);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TimerStarter();
            string _sqlClient = "SELECT * FROM Client";
            clientTable = new DataTable();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
               SqlCommand command = new SqlCommand(_sqlClient, connection);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(clientTable);
                //viewClientTable = clientTable;
                viewClientTable = clientTable.Clone();
                clientGrid.ItemsSource = clientTable.DefaultView;
                //MessageBox.Show("База загружена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            UpdateCoutRows(clientTable.Rows.Count);
        }
        private void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            ClientWindows _clientWindows = new ClientWindows(null,false);
            _clientWindows.Owner = this;
            _clientWindows.Show();
            DataRowView df = _clientWindows.rows;
    }
        private void TimerStarter()
        {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { DateTimeLable.Text = DateTime.Now.ToString(); };
            timer.Start(); 
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }
        private void GenderFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            genderIndex = GenderFilter.SelectedIndex;
            //Сброс фильтра
            if (GenderFilter.SelectedIndex == 1)
            {
                GenderFilter.SelectedIndex = 0;
                clientGrid.ItemsSource = clientTable.DefaultView;
            }
            //Запуск фильтра
            if (GenderFilter.SelectedIndex > 1)
            {
 
                Filter();
            }
        }
        private void Filter()
        {
            //При опустошении филтров
            if (TextBox.Text.Length == 0 && GenderFilter.SelectedIndex < 2)
            {
                clientGrid.ItemsSource = clientTable.DefaultView;
                return;
            }
            
            if (viewClientTable != null)
            viewClientTable.Rows.Clear();
            string g = string.Format("(FirstName LIKE '%{0}%' OR LastName LIKE '%{0}%' OR Patronymic LIKE '%{0}%' OR Email LIKE '%{0}%' OR Phone LIKE '%{0}%')", TextBox.Text);
            //подключение фильтра
            if (GenderFilter.SelectedIndex > 1)
            {
                g = g + " AND  GenderCode LIKE '" + (GenderFilter.SelectedIndex - 1) + "'";
                
                //если пол пуст
                if (TextBox.Text.Length == 0){
                    g = "GenderCode = '"+ (GenderFilter.SelectedIndex - 1) + "'";
                }
            }

            DataRow[] filterRows = clientTable.Select(g);
            //Смена отоброжаемой таблицы
            if (filterRows.Length.ToString().Length > 0)
            {
                clientGrid.ItemsSource = viewClientTable.DefaultView;
                logs.Items.Clear();
                logs.Items.Add(($"Найдено: " + filterRows.Length + " Записей"));
                foreach (DataRow row in filterRows)
                {
                    viewClientTable.ImportRow(row);
                }
            }
            else
            {
                logs.Items.Clear();
                logs.Items.Add("Нет доступных записей");
            }
            UpdateCoutRows(filterRows.Length);
        }
        private void UpdateCoutRows(int t)
        {
            GridCount.Text = t.ToString() + " / " + clientTable.Rows.Count.ToString();
        }
        private void ResetSearch_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text = "";
            GenderFilter.SelectedIndex = 0;
            clientGrid.ItemsSource = clientTable.DefaultView;
        }
        //Редактирование
        private void BtnUpdateClient_Click(object sender, RoutedEventArgs e)
        {
            if (clientGrid.SelectedItems.Count == 0) return;
            
            ClientWindows _clientWindows = new ClientWindows((DataRowView)clientGrid.SelectedItems[0],true);
            _clientWindows.Owner = this;
            _clientWindows.Show();
        }
        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (clientGrid.SelectedItems != null)
            {
                for (int i = 0; i < clientGrid.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = clientGrid.SelectedItems[i] as DataRowView;
                    if (datarowView != null)
                    {
                        DataRow dataRow = (DataRow)datarowView.Row;
                        dataRow.Delete();
                    }
                }
            }
            UpdateDB();
        }
        private void filterBirtsd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filterBirth = filterBirtsd.SelectedIndex;
            switch (filterBirtsd.SelectedIndex)
            {
                case 1:
                    filterBirtsd.SelectedIndex = 0;
                    clientTable.DefaultView.Sort = "[ID] ASC";
                    break;
                case 2:
                    clientTable.DefaultView.Sort = "[Birthday] ASC";
                    filterFirstName.SelectedIndex = 0;
                    filterDateInp.SelectedIndex = 0;
                    break;
                case 3:
                    clientTable.DefaultView.Sort = "[Birthday] DESC";
                    filterFirstName.SelectedIndex = 0;
                    filterDateInp.SelectedIndex = 0;
                    break;
            }

        }
       

        private void filterDateInp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fitterDate = filterDateInp.SelectedIndex;
            switch (filterDateInp.SelectedIndex)
            {
                case 1:
                    filterDateInp.SelectedIndex = 0;
                    clientTable.DefaultView.Sort = "[ID] ASC";
                    break;
                case 2:
                    clientTable.DefaultView.Sort = "[FirstName] ASC";
                    filterBirtsd.SelectedIndex = 0;
                    filterFirstName.SelectedIndex = 0;
                    break;
                case 3:
                    clientTable.DefaultView.Sort = "[FirstName] DESC";
                    filterBirtsd.SelectedIndex = 0;
                    filterFirstName.SelectedIndex = 0;
                    break;
            }
        }
        private void filterFirstName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filterName = filterFirstName.SelectedIndex;
            switch (filterFirstName.SelectedIndex)
            {
                    case 1:
                    filterFirstName.SelectedIndex = 0;
                    clientTable.DefaultView.Sort = "[ID] ASC";
                    break;
                    case 2:
                    clientTable.DefaultView.Sort = "[FirstName] ASC";
                    filterBirtsd.SelectedIndex = 0;
                    filterDateInp.SelectedIndex=0;
                    break;
                case 3:
                    clientTable.DefaultView.Sort = "[FirstName] DESC";
                    filterBirtsd.SelectedIndex = 0;
                    filterDateInp.SelectedIndex = 0;
                    break;
            }
          
        }
        private void clientGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (clientGrid.SelectedItems.Count == 0) return;
            serviseTable = new DataTable();
            string _sqlTir = ("SELECT Service.*,  ClientService.StartTime from ClientService INNER JOIN Client ON ClientService.ClientID = Client.ID INNER JOIN[Service] ON ClientService.ServiceID = [Service].ID WHERE Client.ID = " + ((DataRowView)clientGrid.SelectedValue).Row["ID"].ToString());
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(_sqlTir, connection);
                adapter = new SqlDataAdapter(command);
                connection.Open();
                adapter.Fill(serviseTable);
                ServisWindow _servisWindows = new ServisWindow((DataRowView)clientGrid.SelectedItems[0], serviseTable);
                _servisWindows.Owner = this;
                _servisWindows.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }
        public void addRowsForDB(string firstName, string lastName, string patronymic, DateTime birthDat,
            DateTime registrationDate, string email, string phone, string photoPath, int genderCode)
        {
            Random random = new Random();
            int i = 0;
            bool t = true;
            while (t) {
                i = random.Next();
                if ((clientTable.Select(string.Format("Convert(Id, 'System.String') LIKE '{0}'", i)).Length == 0))
                {
                    t = false;
                    MessageBox.Show("Нашёл!");
                } 
            }
            clientTable.Rows.Add(new object[] { i, firstName, lastName, patronymic, birthDat, registrationDate, email, phone, genderCode.ToString(), photoPath }) ;
            UpdateDB();
        }
        public void updateRowsForDB(int id, string firstName, string lastName, string patronymic, DateTime birthDat,
            DateTime registrationDate, string email, string phone, string photoPath, int genderCode)
        {
            DataRow dr = clientTable.Select(string.Format("Id={0}",(int)id)).FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
            if (dr != null)
            {
                dr[1] = firstName;
                dr[2] = lastName;
                dr[3] = patronymic;
                dr[4] = birthDat;
                dr[5] = registrationDate;
                dr[6] = email;
                dr[7] = phone;
                dr[8] = genderCode.ToString();
                dr[9] = photoPath;//changes the Product_name
            }
            UpdateDB();
        }
    }
}
