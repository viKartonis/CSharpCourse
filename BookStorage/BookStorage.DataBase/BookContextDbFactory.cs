namespace BookStorage.DataBase
{
    public sealed class BookContextDbFactory
    {
        private readonly string _connectionString;

        public BookContextDbFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BookContext GetContext()
        {
            return new BookContext(BookContextDbFactoryDesignTime.GetSqlServerOptions(_connectionString));
        }
    }
}