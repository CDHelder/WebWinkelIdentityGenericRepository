using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebWinkelIdentity.Core.StoreEntities;

namespace WebWinkelIdentity.Data.Service.SpecificRepositories
{
    public class UOWLoadStockChangeRepository : GenericRepository<LoadStockChange>
    {
        private readonly ApplicationDbContext dbContext;

        public UOWLoadStockChangeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<LoadStockChange> GetAllLoadStockChangesAndIncludes()
        {
            var list = dbContext.LoadStockChanges
                .Include(psc => psc.AssociatedUser)
                .Include(lsc => lsc.ProductStockChanges)
                .ThenInclude(psc => psc.StoreProduct)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.Brand)
                .Include(lsc => lsc.ProductStockChanges)
                .ThenInclude(psc => psc.StoreProduct)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.Category)
                .Include(lsc => lsc.ProductStockChanges)
                .ThenInclude(psc => psc.StoreProduct)
                .ThenInclude(p => p.Store)
                .ThenInclude(p => p.Address)
                .Include(lsc => lsc.ProductStockChanges)
                .ThenInclude(psc => psc.StoreProduct)
                .ThenInclude(p => p.Store)
                .OrderByDescending(p => p.DateChanged)
                .ToList();

            foreach (var lsc in list)
            {
                lsc.ProductStockChanges = lsc.ProductStockChanges.OrderBy(psc => psc.StoreProduct.Store.Address.City).ToList();
            }

            return list;
        }
    }
}
