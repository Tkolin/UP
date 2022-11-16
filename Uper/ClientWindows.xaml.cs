using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using Microsoft.Win32;

namespace Uper
{
/// <summary>
/// Логика взаимодействия для ClientWindows.xaml
/// </summary>
public partial class ClientWindows : Window
{
        int genderIndex;
    public DataRowView rows { get; set; }
        public bool type;
        //да - редактирование
        //нет - добавление
        string imagePacthDB;
        OpenFileDialog openFileDialog = new OpenFileDialog();
        string imgPatch = @"C:\Users\Grigoriy\source\repos\Uper\Uper\Клиенты\";
        public ClientWindows(DataRowView rows, bool type)
    {
        InitializeComponent();
        this.rows = rows;
            this.type = type;
            this.phoneLable.PreviewTextInput += new TextCompositionEventHandler(phoneLable_PreviewTextInput);
            this.firtLable.PreviewTextInput += new TextCompositionEventHandler(firtLable_PreviewTextInput);
            this.twoLable.PreviewTextInput += new TextCompositionEventHandler(twoLable_PreviewTextInput);
            this.friLable.PreviewTextInput += new TextCompositionEventHandler(friLable_PreviewTextInput);

        }
        private void phoneLable_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if ((!Char.IsDigit(e.Text, 0) || e.Text == "(" || e.Text == ")" || e.Text =="+" || e.Text == "-") && twoLable.Text.Length > 12) e.Handled = true;
        }
        private void firtLable_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Char.IsDigit(e.Text, 0) && twoLable.Text.Length > 49) e.Handled = true;
        }

        private void friLable_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Char.IsDigit(e.Text, 0) && twoLable.Text.Length > 49) e.Handled = true;
        }

        private void twoLable_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Char.IsDigit(e.Text, 0) && twoLable.Text.Length > 49) e.Handled = true;
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
    private void TimerStarter()
    {
        var timer = new System.Windows.Threading.DispatcherTimer();
        timer.Interval = new TimeSpan(0, 0, 1);
        timer.IsEnabled = true;
        timer.Tick += (o, t) => { DataTimerLable.Text = DateTime.Now.ToString(); };
        timer.Start();

    }
    
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
             
        TimerStarter();
            if (type)
            {
                firtLable.Text = rows.Row["FirstName"].ToString();
                twoLable.Text = rows.Row["LastName"].ToString();
                friLable.Text = rows.Row["Patronymic"].ToString();
                idText.Text = rows.Row["id"].ToString();
                dateLable.SelectedDate = (DateTime)rows.Row["Birthday"];
                GenderFilter.SelectedIndex = (Convert.ToInt32(rows.Row["GenderCode"]) + 1);

                phoneLable.Text = rows.Row["Phone"].ToString();
                emailLable.Text = rows.Row["Email"].ToString();
                try
                {
                    Uri dts = new Uri(rows.Row["PhotoPath"].ToString().Replace(" ", ""), UriKind.Relative);
                    imgLable.Source = (new BitmapImage(dts));
                }
                catch
                {
                    MessageBox.Show("ошибка изображения");
                }
            }


    }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!(firtLable.Text.Length < 0 || twoLable.Text.Length < 0 || friLable.Text.Length < 0 || dateLable.SelectedDate != null ||
  emailLable.Text.Length < 0 || phoneLable.Text.Length < 0 || phoneLable.Text.Length < 0 || GenderFilter.SelectedIndex != 0))
                return;
            if (!type)
            {
                ((MainWindow)Owner).addRowsForDB(firtLable.Text, twoLable.Text, friLable.Text, (DateTime)dateLable.SelectedDate
                    , DateTime.Now, emailLable.Text, phoneLable.Text, imagePacthDB, Convert.ToChar(GenderFilter.SelectedIndex - 1));
                this.Close();
            }
            else
            {
                ((MainWindow)Owner).updateRowsForDB(Convert.ToInt32(idText.Text),firtLable.Text, twoLable.Text, friLable.Text, (DateTime)dateLable.SelectedDate
    , DateTime.Now, emailLable.Text, phoneLable.Text, imagePacthDB, Convert.ToChar(GenderFilter.SelectedIndex - 1));
                this.Close();
            }
            }

            private void GenderFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                genderIndex = GenderFilter.SelectedIndex;
                if (GenderFilter.SelectedIndex == 1)
                    GenderFilter.SelectedIndex = 0;
            }

        private void DelImg_Click(object sender, RoutedEventArgs e)
        {
            imgLable.Source = null;
            imagePacthDB = null;
        }

        private void DownloadImg_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog.ShowDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            if (openFileDialog.FileName != null)
            {
                string fileName = openFileDialog.FileName;


                string[] subs = fileName.Split(Convert.ToChar(@"\"));
                string imgFilePatch = imgPatch + subs[subs.Length - 1];
                File.Move(fileName, imgFilePatch);

          
                imagePacthDB = @"Клиенты/" + subs[subs.Length - 1];
                try
                {
                    imgLable.Source = 
                    new BitmapImage(new Uri(imagePacthDB, UriKind.Relative));

                }
                catch
                {
                    MessageBox.Show("ошибка изображения");
                }

            
            }

        }

        private void SaveImg_Click(object sender, RoutedEventArgs e)
        {
            //по приколу доабвил
        }
    }

      

    }

                        