using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebWinkelIdentity.Core.StoreEntities;

namespace WebWinkelIdentity.Data.Service.SpecificRepositories
{
    public class UOWProductRepository : GenericRepository<Product>
    {
        private readonly ApplicationDbContext _dbContext;

        public UOWProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<Product> GetProductVariations(Product product)
        {
            return _dbContext.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Where(p =>
                    p.BrandId == product.BrandId &&
                    p.CategoryId == product.CategoryId &&
                    p.Color == product.Color &&
                    p.Fabric == product.Fabric &&
                    p.Name == product.Name &&
                    p.Price == product.Price)
                .ToList()
                .GroupBy(p => new { p.Size })
                .Select(p => p.First())
                .ToList();
        }

        public void UpdateProductProperties(Product product, List<Product> products)
        {
            foreach (var prod in products)
            {
                prod.BrandId = product.BrandId;
                prod.CategoryId = product.CategoryId;
                prod.Color = product.Color;
                prod.Description = product.Description;
                prod.Fabric = product.Fabric;
                prod.Name = product.Name;
                prod.Price = product.Price;
            }

            Update(products);
        }

        public List<Product> GetUniqueListProducts()
        {
            return _dbContext.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .ToList()
                .GroupBy(p => new { p.Name, p.Price, p.Color, p.Fabric, p.BrandId, p.CategoryId })
                .Select(p => p.First())
                .ToList();
        }
    }
}
