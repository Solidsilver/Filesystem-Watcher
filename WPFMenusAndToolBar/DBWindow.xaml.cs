using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.SQLite;
using System.Data;
using Database;

namespace WPFMenusAndToolBar
{
    /// <summary>
    /// Interaction logic for DBWindow.xaml
    /// </summary>
    public partial class DBWindow : Window
    {
        private int counter;
        private SQ dbase;
        private string sAttrib, sVal;
        public DBWindow()
        {
            InitializeComponent();
            dbase = new SQ();
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            this.FillDataGrid("");
        }

        private void FillDataGrid(string search)
        {
        string CmdString = string.Empty;

            using (SQLiteConnection con = new SQLiteConnection("Data Source=filewatcher.db;Version=3;New=True;Compress=True;"))
            {
                CmdString = "SELECT * from FileInfo " + search + "";
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
                    this.sAttrib = "Filename";
                    break;
                case 1:
                    this.sAttrib = "Path";
                    break;
                case 2:
                    this.sAttrib = "Action";
                    break;
                case 3:
                    this.sAttrib = "Extension";
                    break;
                case 4:
                    this.sAttrib = "DateTime";
                    break;

            }
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

        private void muiClearDB_Click(object sender, RoutedEventArgs e)
        {
            this.dbase.clearDB();
            FillDataGrid();
        }

        private void Search(string column, string term)
        {
            FillDataGrid("WHERE "+column+" LIKE '%"+term+"%'");
        }
    }
}
