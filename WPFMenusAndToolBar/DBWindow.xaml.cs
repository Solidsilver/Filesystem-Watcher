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
        private SQ dbase;
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

            using (SQLiteConnection con = dbase.getConn())
            {
                CmdString = "SELECT * FROM FileInfo '" + search + "'";
                SQLiteCommand cmd = new SQLiteCommand(CmdString, con);
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                DataTable dt = new DataTable("FileInfo");
                sda.Fill(dt);
                grdDatabase.ItemsSource = dt.DefaultView;
            }
        }

        private void SearchExt(string ext)
        {
            FillDataGrid("WHERE ext LIKE '%'" + ext + "'%'");
        }
    }
}
