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
using System.Windows.Shapes;

namespace WPFMenusAndToolBar
{
    /// <summary>
    /// Interaction logic for DBWindow.xaml
    /// </summary>
    public partial class DBWindow : Window
    {
        private int counter;
        public DBWindow()
        {
            InitializeComponent();
        }

        private void btnAddToListBox_Click(object sender, RoutedEventArgs e)
        {
            this.lstDBInfo.Items.Add("List Box entry number " + ++counter);
            this.lvDBInfo.Items.Add(new { First = "col1", Second = "col2", Third = "col3" });
        }
    }
}
