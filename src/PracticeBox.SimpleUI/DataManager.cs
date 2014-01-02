using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ServiceStack;
using ServiceStack.OrmLite;

namespace PracticeBox.SimpleUI
{
    public class DataManager
    {
        DataManager()
        {
            OrmLiteConfig.DialectProvider = SqliteDialect.Provider;
            ConnectionString = SqliteFileDb;
            updateTables();
        }

        private void updateTables()
        {
            using (var dbConn = OpenDbConnection())
            {
            }
        }
        public static string SqliteFileDb = "~/App_Data/db.sqlite".MapAbsolutePath();
        private static DataManager manager;
        public static DataManager Manager
        {

            get { return manager ?? (manager = new DataManager()); }
        }
        public IDbConnection InMemoryDbConnection { get; set; }
        public IDbConnection OpenDbConnection(string connString = null)
        {
            connString = ConnectionString;
            return connString.OpenDbConnection();
        }
        protected virtual string ConnectionString { get; set; }
        protected virtual string GetFileConnectionString()
        {
            var connectionString = SqliteFileDb;
            return connectionString;
        }
    }
}
