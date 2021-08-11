using WebWinkelIdentity.Core;

namespace WebWinkelIdentity.Data.Service.SpecificRepositories
{
    public class UOWEmployeeRepository : GenericRepository<Employee>
    {
        private readonly ApplicationDbContext dbContext;

        public UOWEmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}