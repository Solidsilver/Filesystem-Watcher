using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Database;

namespace WPFMenusAndToolBar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileSystemWatcher watcher;
        private DBWindow dbWindow;
        private FileInfo curFi;
        private DirectoryInfo path;
        private SQ dbase, tmpdb;
        public MainWindow()
        {
            InitializeComponent();
            SetupWatcher();
            //dbase = new SQ();
            tmpdb = new SQ("filewatcher.tmpdb");
        }

        private void SetupWatcher()
        {
            watcher = new FileSystemWatcher(@"C:\");
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
           | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            watcher.IncludeSubdirectories = true;

            watcher.Changed += Watcher_Created;
            watcher.Created += Watcher_Created;
            watcher.Deleted += Watcher_Created;
            watcher.Renamed += Watcher_Created;

            path = new DirectoryInfo("C:\\");
        }
        private static bool IsBadDir(string path)
        {
            String curDir = Directory.GetCurrentDirectory().ToLower();
            path = path.ToLower();
            if (path == curDir + "\\filewatcher.tmpdb")
            {
                return true;
            }
            if (path == curDir + "\\filewatcher.tmpdb-journal")
            {
                return true;
            }
            if (path == curDir)
            {
                return true;
            }
            return false;

        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            if (!IsBadDir(e.FullPath))
            {
                curFi = new FileInfo(e.FullPath);
                tmpdb.writeDB(this.curFi.Name, e.FullPath, e.ChangeType.ToString(), curFi.Extension, System.DateTime.Now.ToString());
                Dispatcher.BeginInvoke(
                   (Action)(() =>
                   {
                   this.lvEvents.Items.Insert(0, new { Datetime = System.DateTime.Now.ToString(), EvType = e.ChangeType, Location = e.FullPath });
                   this.lvEvents.Items.MoveCurrentToPosition(lvEvents.Items.Count - 1);
               }));
            }
        }

        private void mnuFileExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("The program will now exit. The current log is not saved.\nWould you like to save it?", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result != MessageBoxResult.Cancel)
            {
                if (result == MessageBoxResult.Yes)
                {

                }
                if (result == MessageBoxResult.No)
                {

                }
                this.tmpdb.clearDB();
                Environment.Exit(0);
            }
            
            
        }

        private void mnuHelpAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Luke's modified menu-watcher app.\n .NET Framework version 4.6 64 bit");
        }

        private void mnuFileDBWindow_Click(object sender, RoutedEventArgs e)
        {
            this.dbWindow = new DBWindow();
            this.dbWindow.ShowDialog();
        }

        private void mnuFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStartWatcher_Click(object sender, RoutedEventArgs e)
        {
            if (path.Exists)
            {
                watcher.EnableRaisingEvents = true;
                this.btnStartWatcher.IsEnabled = false;
                this.btnStopWatcher.IsEnabled = true;
            } else
            {
                MessageBox.Show("Invalid PATH: " + path.ToString() + "\nPlease specify a valid path");
            }
            
        }

        private void btnStopWatcher_Click(object sender, RoutedEventArgs e)
        {
            watcher.EnableRaisingEvents = false;
            btnStartWatcher.IsEnabled = true;
            btnStopWatcher.IsEnabled = false;
        }

        private void btnSetPath_Click(object sender, RoutedEventArgs e)
        {
            path = new DirectoryInfo(txtEnterPath.Text);
            if (path.Exists)
            {
                lblValidPath.Visibility = Visibility.Hidden;
                watcher.Path = txtEnterPath.Text;
                lblValidPath.Content = "PATH set: " + txtEnterPath.Text;
            } else
            {
                lblValidPath.Content = "Invalid PATH: " + txtEnterPath.Text;
                MessageBox.Show("Invalid PATH: " + txtEnterPath.Text);
                txtEnterPath.Clear();
            }
            lblValidPath.Visibility = Visibility.Visible;
        }

        private void txtEnterPath_GotMouseCapture(object sender, MouseEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("The program will now exit. The current log is not saved.\nWould you like to save it?", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            } else
            {
                if (result == MessageBoxResult.Yes)
                {

                }
                this.tmpdb.clearDB();
            }
            
        }

        private void btnClearLog_Click(object sender, RoutedEventArgs e)
        {
            //txtWatcherEvents.Clear();
        }
    }
}
