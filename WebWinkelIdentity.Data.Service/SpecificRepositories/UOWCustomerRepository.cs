using WebWinkelIdentity.Core;

namespace WebWinkelIdentity.Data.Service.SpecificRepositories
{
    public class UOWCustomerRepository : GenericRepository<Customer>
    {
        private readonly ApplicationDbContext dbContext;

        public UOWCustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}