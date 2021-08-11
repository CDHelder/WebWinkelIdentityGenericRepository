using WebWinkelIdentity.Core;

namespace WebWinkelIdentity.Data.Service.SpecificRepositories
{
    public class UOWAddressRepository : GenericRepository<Address>
    {
        private readonly ApplicationDbContext dbContext;

        public UOWAddressRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}