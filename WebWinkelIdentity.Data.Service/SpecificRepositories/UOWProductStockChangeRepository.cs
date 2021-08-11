using WebWinkelIdentity.Core.StoreEntities;

namespace WebWinkelIdentity.Data.Service.SpecificRepositories
{
    public class UOWProductStockChangeRepository : GenericRepository<ProductStockChange>
    {
        private readonly ApplicationDbContext dbContext;

        public UOWProductStockChangeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}