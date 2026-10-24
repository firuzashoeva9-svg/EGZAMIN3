using Npgsql;
namespace Infrastructure.Data;

public static class Database
{
    private static string connectionString = "Host=localhost; Port=5432; Database=MiniShopp; Username=postgres; Password=1234";
    public static NpgsqlConnection Connection()
    {
        return new NpgsqlConnection(connectionString);
    }
}