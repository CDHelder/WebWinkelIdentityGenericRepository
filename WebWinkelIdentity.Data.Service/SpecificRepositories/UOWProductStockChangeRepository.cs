using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public List<ProductStockChange> GetAllProductStockChangesAndIncludes()
        {
            return dbContext.ProductStockChanges
                .Include(psc => psc.StoreProduct)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.Brand)
                .Include(psc => psc.StoreProduct)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.Category)
                .Include(psc => psc.StoreProduct)
                .ThenInclude(p => p.Store)
                .ThenInclude(p => p.Address)
                .Include(psc => psc.StoreProduct)
                .ThenInclude(p => p.Store)
                .Include(psc => psc.AssociatedUser)
                .OrderByDescending(p => p.Id)
                .ToList();
        }
    }
}