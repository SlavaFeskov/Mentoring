using Task.DB;

namespace Task.Services
{
    public static class NorthwindDbContextProvider
    {
        private static Northwind _dbContext;

        public static Northwind Get()
        {
            return _dbContext ?? (_dbContext = new Northwind());
        }
    }
}