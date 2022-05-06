namespace SportsStore.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        private StoreDbContext _dbContext;

        public EFStoreRepository(StoreDbContext ctx)
        {
            _dbContext = ctx;
        }

        public IQueryable<Product> Products => _dbContext.Products;
    }
}
