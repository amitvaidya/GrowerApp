using GrowerApp.Interfaces;
using GrowerApp.Model;
using SQLite;

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
            //check and create News table.
            var cmd = _conn.CreateCommand("SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = 'News'");
//            cmd.Parameters.AddWithValue("@name", tableName);
            if (cmd.ExecuteScalar<int>() == 0)
                _conn.CreateTable<News>();
        }
    }
}
