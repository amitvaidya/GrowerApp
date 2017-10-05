using SQLite;

namespace GrowerApp.Interfaces
{
    public interface IDbOperations
    {
        SQLiteConnection GetDbConnection();
    }
}