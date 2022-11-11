using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace SQLiteTool.Help
{
    public static class SQLiteHelper
    {
        public static SqliteCommand GetSqliteCommand(string path)
        {
            string connStr = string.Format("Data Source={0}", path);
            SqliteConnection sqliteConnection = new(connStr);
            sqliteConnection.Open();
            SqliteCommand sqliteCmd = sqliteConnection.CreateCommand();
            return sqliteCmd;
        }
    }
}
