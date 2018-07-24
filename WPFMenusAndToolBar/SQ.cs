using System;
using System.Data.SQLite;

namespace Database
{
    public class SQ
    {
        private SQLiteConnection sqlite_conn;
        private SQLiteCommand sqlite_cmd;

        public SQ()
        {
            this.sqlite_conn = new SQLiteConnection("Data Source=filewatcher.db;Version=3;New=True;Compress=True;");
            //filewatacher.db is stored in the /bin/Debug folder of this project.
            this.sqlite_conn.Open();
            this.sqlite_cmd = sqlite_conn.CreateCommand();
            this.sqlite_cmd.CommandText = "CREATE TABLE if not exists FileInfo (fname varchar(100), apath varchar(100), action varchar(10), ext varchar(10), datetime varchar(25));";
            this.sqlite_cmd.ExecuteNonQuery();
        }

        public SQ(string fname)
        {
            this.sqlite_conn = new SQLiteConnection("Data Source=" + fname + ";Version=3;New=True;Compress=True;");
            //.db file is stored in the /bin/Debug folder of this project.
            this.sqlite_conn.Open();
            this.sqlite_cmd = sqlite_conn.CreateCommand();
            this.sqlite_cmd.CommandText = "CREATE TABLE if not exists FileInfo (fname varchar(100), apath varchar(100), action varchar(10), ext varchar(10), datetime varchar(25));";
            this.sqlite_cmd.ExecuteNonQuery();
        }

        public void writeDB(string fname, string apath, string action, string ext, string datetime)
        {
            this.sqlite_cmd.CommandText = "INSERT INTO FileInfo (fname, apath, action, ext, datetime) VALUES ('" + fname + "', '" + apath + "', '" + action + "', '" + ext + "', '" + datetime + "');";
            this.sqlite_cmd.ExecuteNonQuery();
        }

        public void closeDB()
        {
            sqlite_conn.Close();
        }

        public void clearDB()
        {
            this.sqlite_cmd.CommandText = "DELETE FROM FileInfo";
            this.sqlite_cmd.ExecuteNonQuery();
        }

        public SQLiteConnection getConn()
        {
            return this.sqlite_conn;
        }

    }
}
