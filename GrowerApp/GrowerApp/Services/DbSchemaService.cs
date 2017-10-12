using System;
using GrowerApp.Interfaces;
using GrowerApp.Model;
using GrowerApp.ViewModel;
using SQLite;
using Xamarin.Forms.PlatformConfiguration;

namespace GrowerApp.Services
{
    public class DbSchemaService : IDbSchemaService
    {
        private readonly SQLiteConnection conn;

        public DbSchemaService(IDbOperations dbOperations)
        {
            conn = dbOperations.GetDbConnection();
        }

        public void CreateDatabaseAndTables()
        {
            try
            {
                //check and create News table.
                TableExists("News", result => { if (result) conn.CreateTable<News>(); });
            }
            catch (Exception ex)
            {
                IocContainer.Container.GetInstance<ILogger>().LogError("CreateDatabaseAndTables", ex.Message);
            }
        }

        public void TableExists(string tableName, Action<bool> callbackAction)
        {
            var cmd = conn.CreateCommand($"SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = '{tableName}'");
            callbackAction(cmd.ExecuteScalar<int>() != 0);
        }
    }
}
