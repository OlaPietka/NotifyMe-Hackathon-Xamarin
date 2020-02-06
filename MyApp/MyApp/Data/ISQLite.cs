using SQLite;

namespace MyApp.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
