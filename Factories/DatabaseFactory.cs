using OutseraApiTest.Repositories;

namespace OutseraApiTest.Factories
{
    public class DatabaseFactory
    {
        public enum DatabaseType
        {
            LiteDB
        }

        public static IMovieRepository CreateMovieRepository(
            DatabaseType type,
            string connectionString)
        {
            return type switch
            {
                DatabaseType.LiteDB => new LiteDBMovieRepository(connectionString),
                _ => throw new ArgumentException("Tipo de banco de dados não suportado")
            };
        }
    }
}
