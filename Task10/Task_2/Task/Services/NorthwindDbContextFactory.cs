using Task.DB;

namespace Task.Services
{
    public static class NorthwindDbContextFactory
    {
        private static Northwind _dbContext;

        public static Northwind Create()
        {
            return _dbContext ?? (_dbContext = new Northwind());
        }
    }
}