using System;
using System.Data.SQLite;

namespace Database
{
    public class SQ
    {
        private SQLiteConnection sqlite_conn;
        private SQLiteCommand sqlite_cmd;
        public Boolean isEmpty;

        public SQ()
        {
            this.sqlite_conn = new SQLiteConnection("Data Source=filewatcher.db;Version=3;New=True;Compress=True;");
            //filewatacher.db is stored in the /bin/Debug folder of this project.
            this.sqlite_conn.Open();
            this.sqlite_cmd = sqlite_conn.CreateCommand();
            this.sqlite_cmd.CommandText = "CREATE TABLE if not exists FileInfo (Filename varchar(100), Action varchar(10), Extension varchar(10), DateTime varchar(25), Path varchar(100));";
            this.sqlite_cmd.ExecuteNonQuery();
        }

        public SQ(string fname)
        {
            this.sqlite_conn = new SQLiteConnection("Data Source=" + fname + ";Version=3;New=True;Compress=True;");
            //.db file is stored in the /bin/Debug folder of this project.
            this.sqlite_conn.Open();
            this.sqlite_cmd = sqlite_conn.CreateCommand();
            this.sqlite_cmd.CommandText = "CREATE TABLE if not exists FileInfo (Filename varchar(100), Action varchar(10), Extension varchar(10), DateTime varchar(25), Path varchar(100));";
            this.sqlite_cmd.ExecuteNonQuery();
            clearDB();
        }

        public void writeDB(string fname, string apath, string action, string ext, string datetime)
        {
            this.sqlite_cmd.CommandText = "INSERT INTO FileInfo (Filename, Action, Extension, DateTime, Path) VALUES ('" + fname + "', '" + action + "', '" + ext + "', '" + datetime + "', '" + apath + "');";
            this.sqlite_cmd.ExecuteNonQuery();
            this.isEmpty = false;
        }

        public void closeDB()
        {
            sqlite_conn.Close();
        }

        public void clearDB()
        {
            this.sqlite_cmd.CommandText = "DELETE FROM FileInfo";
            this.sqlite_cmd.ExecuteNonQuery();
            this.isEmpty = true;
        }

        public SQLiteConnection getConn()
        {
            return this.sqlite_conn;
        }

        public void executeCommand(string cmd)
        {
            this.sqlite_cmd.CommandText = cmd;
            this.sqlite_cmd.ExecuteNonQuery();
        }

    }
}
