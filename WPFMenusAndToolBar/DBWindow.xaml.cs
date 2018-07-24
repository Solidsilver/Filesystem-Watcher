using Database;
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
using System.Data.SQLite;
using System.Data;

namespace WPFMenusAndToolBar
{
    /// <summary>
    /// Interaction logic for DBWindow.xaml
    /// </summary>
    public partial class DBWindow : Window
    {
        private int counter;
        //private SQ dbase;
        private string sAttrib, sVal;
        public DBWindow()
        {
            InitializeComponent();
            //dbase = new SQ("filewatcher.tmpdb");
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            this.FillDataGrid("");
        }

        private void FillDataGrid(string search)
        {
        string CmdString = string.Empty;

            using (SQLiteConnection con = new SQLiteConnection("Data Source=filewatcher.tmpdb;Version=3;New=True;Compress=True;"))
            {
                CmdString = "SELECT * from FileInfo " + search + "";
                string cmpCmdString = "SELECT * from FileInfo WHERE instr(ext, 'pf') > 0";
                int diff = string.Compare(CmdString, cmpCmdString);
                SQLiteCommand cmd = new SQLiteCommand(CmdString, con);
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                DataTable dt = new DataTable("FileInfo");
                sda.Fill(dt);
                grdDatabase.ItemsSource = dt.DefaultView;
            }
        }

        private void cbxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(cbxSort.SelectedIndex)
            {
                case -1:
                    break;
                case 0:
                    this.sAttrib = "fname";
                    break;
                case 1:
                    this.sAttrib = "apath";
                    break;
                case 2:
                    this.sAttrib = "action";
                    break;
                case 3:
                    this.sAttrib = "ext";
                    break;
                case 4:
                    this.sAttrib = "datetime";
                    break;

            }
            if (cbxSort.SelectedIndex == 0)
            this.sAttrib = cbxSort.SelectedItem.ToString();
        }

        private void btnDbSearch_Click(object sender, RoutedEventArgs e)
        {
            this.sVal = this.tbxSearch.Text;
            if (this.sAttrib == null)
            {
                MessageBox.Show("Please enter valid search parameters");
            } else
            {
                Search(this.sAttrib, this.sVal);
            }
        }

        private void tbxSearch_GotMouseCapture(object sender, MouseEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void btnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            this.cbxSort.SelectedIndex = -1;
            this.tbxSearch.Text = "";
            FillDataGrid();
        }

        private void Search(string column, string term)
        {
            FillDataGrid("WHERE "+column+" LIKE '%"+term+"%'");
        }
    }
}
