using WebWinkelIdentity.Core;

namespace WebWinkelIdentity.Data.Service.SpecificRepositories
{
    public class UOWBrandRepository : GenericRepository<Brand>
    {
        private readonly ApplicationDbContext dbContext;

        public UOWBrandRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}