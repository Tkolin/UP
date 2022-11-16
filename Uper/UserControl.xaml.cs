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

namespace Uper
{
    /// <summary>
    /// Логика взаимодействия для UserControl.xaml
    /// </summary>
    public partial class UserControl : UserControl
    {
        public string name;
        public DateTime time;
        public List countFiles;
        public UserControl(string name, DateTime time,List countFiles)
        {
            InitializeComponent();
            this.name = name;
            this.time = time;
            this.countFiles = countFiles;
        }

    }
}
