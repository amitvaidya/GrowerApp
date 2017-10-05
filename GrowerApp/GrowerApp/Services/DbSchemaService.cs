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
        private readonly SQLiteConnection _conn;

        public DbSchemaService(IDbOperations dbOperations)
        {
            _conn = dbOperations.GetDbConnection();
        }

        public void CreateDatabaseAndTables()
        {
            try
            {
                //check and create News table.
                var cmd = _conn.CreateCommand("SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = 'News'");
                //            cmd.Parameters.AddWithValue("@name", tableName);
                if (cmd.ExecuteScalar<int>() == 0)
                    _conn.CreateTable<News>();
            }
            catch (Exception ex)
            {
                IocContainer.Container.GetInstance<ILogger>().LogError("CreateDatabaseAndTables", ex.Message);
            }
        }
    }
}
