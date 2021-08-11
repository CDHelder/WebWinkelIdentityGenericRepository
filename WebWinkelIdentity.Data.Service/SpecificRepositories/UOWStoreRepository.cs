using WebWinkelIdentity.Core.StoreEntities;

namespace WebWinkelIdentity.Data.Service.SpecificRepositories
{
    public class UOWStoreRepository : GenericRepository<Store>
    {
        private readonly ApplicationDbContext dbContext;

        public UOWStoreRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}