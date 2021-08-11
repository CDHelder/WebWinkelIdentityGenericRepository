using WebWinkelIdentity.Core;

namespace WebWinkelIdentity.Data.Service.SpecificRepositories
{
    public class UOWCategoryRepository : GenericRepository<Category>
    {
        private readonly ApplicationDbContext dbContext;

        public UOWCategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}