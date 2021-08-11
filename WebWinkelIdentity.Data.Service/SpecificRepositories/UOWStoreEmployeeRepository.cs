using WebWinkelIdentity.Core.StoreEntities;

namespace WebWinkelIdentity.Data.Service.SpecificRepositories
{
    public class UOWStoreEmployeeRepository : GenericRepository<StoreEmployee>
    {
        private readonly ApplicationDbContext dbContext;

        public UOWStoreEmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}