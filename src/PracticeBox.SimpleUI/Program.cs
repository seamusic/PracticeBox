using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using PracticeBox.SimpleUI.Entities;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;
using ServiceStack.OrmLite.Sqlite;

namespace PracticeBox.SimpleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            OrmLiteTest();

            var db = PetaDbAccess.GetInstance();
            var list = db.Fetch<dynamic>("select * from Author");
            var au = db.Single<dynamic>("select * from Author");
            var au2 = db.FirstOrDefault<dynamic>("select * from Author");

            Console.ReadKey();
        }

        private static void OrmLiteTest()
        {
            OrmLiteConfig.DialectProvider = SqliteOrmLiteDialectProvider.Instance;

            SqlExpression<Author> ev = OrmLiteConfig.DialectProvider.SqlExpression<Author>();

            using (IDbConnection db = GetFileConnectionString().OpenDbConnection())
            {
                db.DropTable<Author>();
                db.CreateTable<Author>();
                db.DeleteAll<Author>();

                var authors = new List<Author>();
                authors.Add(new Author()
                {
                    Name = "Demis Bellot",
                    Birthday = DateTime.Today.AddYears(-20),
                    Active = true,
                    Earnings = 99.9m,
                    Comments = "CSharp books",
                    Rate = 10,
                    City = "London"
                });

                db.InsertAll(authors);
                //db.Where<Author>(o => o.Birthday == "");
            }

            using (var db = DataManager.Manager.OpenDbConnection(GetFileConnectionString()))
            {

            }
        }

        private static string GetFileConnectionString()
        {
            var connectionString = "~/db.sqlite".MapAbsolutePath();
            if (File.Exists(connectionString))
                File.Delete(connectionString);

            return connectionString;
        }
    }
}
