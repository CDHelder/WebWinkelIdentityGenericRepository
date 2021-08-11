using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebWinkelIdentity.Core.StoreEntities;

namespace WebWinkelIdentity.Data.Service.SpecificRepositories
{
    public class UOWStoreProductRepository : GenericRepository<StoreProduct>
    {
        private readonly ApplicationDbContext dbContext;

        public UOWStoreProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<StoreProduct> GetAllStoreProducts(List<Product> products)
        {
            List<StoreProduct> storeProducts = new();

            foreach (var product in products)
            {
                storeProducts.AddRange(dbContext.StoreProducts
                .Include(sp => sp.Store)
                .Include(sp => sp.Product)
                .Where(sp => sp.ProductId == product.Id)
                .ToList()
                .GroupBy(p => new { p.StoreId, p.ProductId })
                .Select(sp => sp.First())
                .ToList());
            }

            return storeProducts;
        }

        public List<StoreProduct> GetAllStoreProducts(List<int> productIds, int storeId)
        {
            var distinctProductIds = productIds.Distinct();
            return dbContext.StoreProducts
                .Include(p => p.Product)
                .ThenInclude(p => p.Brand)
                .Include(p => p.Product)
                .ThenInclude(p => p.Category)
                .Include(p => p.Store)
                .ThenInclude(s => s.Address)
                .Where(p => distinctProductIds.Contains(p.ProductId) && p.StoreId == storeId)
                .ToList();
        }
    }
}