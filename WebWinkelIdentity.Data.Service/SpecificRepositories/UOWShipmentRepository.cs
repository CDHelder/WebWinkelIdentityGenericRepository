using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWinkelIdentity.Core.StoreEntities;

namespace WebWinkelIdentity.Data.Service.SpecificRepositories
{
    public class UOWShipmentRepository : GenericRepository<Shipment>
    {
        private readonly ApplicationDbContext dbContext;

        public UOWShipmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Shipment> GetAllShipmentsAndIncludes(bool isDelivered)
        {
            var list = dbContext.Shipments
                .Include(s => s.LoadStockChange)
                .ThenInclude(psc => psc.AssociatedUser)
                .Include(s => s.LoadStockChange)
                .ThenInclude(lsc => lsc.ProductStockChanges)
                .ThenInclude(psc => psc.StoreProduct)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.Brand)
                .Include(s => s.LoadStockChange)
                .ThenInclude(lsc => lsc.ProductStockChanges)
                .ThenInclude(psc => psc.StoreProduct)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.Category)
                .Include(s => s.LoadStockChange)
                .ThenInclude(lsc => lsc.ProductStockChanges)
                .ThenInclude(psc => psc.StoreProduct)
                .ThenInclude(p => p.Store)
                .ThenInclude(p => p.Address)
                .Include(s => s.LoadStockChange)
                .ThenInclude(lsc => lsc.ProductStockChanges)
                .ThenInclude(psc => psc.StoreProduct)
                .ThenInclude(p => p.Store)
                .Include(s => s.EndLocationStore)
                .ThenInclude(es => es.Address)
                .Where(s => s.Delivered == isDelivered)
                .OrderByDescending(p => p.LoadStockChange.DateChanged)
                .ToList();

            foreach (var item in list)
            {
                item.LoadStockChange.ProductStockChanges.OrderBy(psc => psc.StoreProduct.Store.Address.City);
            }

            return list;
        }

        public Shipment GetShipmentAndIncludes(int id)
        {
            return dbContext.Shipments
                .Include(s => s.LoadStockChange)
                .ThenInclude(psc => psc.AssociatedUser)
                .Include(s => s.LoadStockChange)
                .ThenInclude(lsc => lsc.ProductStockChanges)
                .ThenInclude(psc => psc.StoreProduct)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.Brand)
                .Include(s => s.LoadStockChange)
                .ThenInclude(lsc => lsc.ProductStockChanges)
                .ThenInclude(psc => psc.StoreProduct)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.Category)
                .Include(s => s.LoadStockChange)
                .ThenInclude(lsc => lsc.ProductStockChanges)
                .ThenInclude(psc => psc.StoreProduct)
                .ThenInclude(p => p.Store)
                .ThenInclude(p => p.Address)
                .Include(s => s.LoadStockChange)
                .ThenInclude(lsc => lsc.ProductStockChanges)
                .ThenInclude(psc => psc.StoreProduct)
                .ThenInclude(p => p.Store)
                .Include(s => s.EndLocationStore)
                .ThenInclude(es => es.Address)
                .Include(s => s.UserThatConfirmed)
                .Where(s => s.Id == id)
                .FirstOrDefault();
        }

        public List<Shipment> GetAllShipmentsAndIncludes(bool isDelivered, List<int> ids)
        {
            var list = dbContext.Shipments
                .Include(s => s.LoadStockChange)
                .ThenInclude(psc => psc.AssociatedUser)
                .Include(s => s.LoadStockChange)
                .ThenInclude(lsc => lsc.ProductStockChanges)
                .ThenInclude(psc => psc.StoreProduct)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.Brand)
                .Include(s => s.LoadStockChange)
                .ThenInclude(lsc => lsc.ProductStockChanges)
                .ThenInclude(psc => psc.StoreProduct)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.Category)
                .Include(s => s.LoadStockChange)
                .ThenInclude(lsc => lsc.ProductStockChanges)
                .ThenInclude(psc => psc.StoreProduct)
                .ThenInclude(p => p.Store)
                .ThenInclude(p => p.Address)
                .Include(s => s.LoadStockChange)
                .ThenInclude(lsc => lsc.ProductStockChanges)
                .ThenInclude(psc => psc.StoreProduct)
                .ThenInclude(p => p.Store)
                .Include(s => s.EndLocationStore)
                .ThenInclude(es => es.Address)
                .Where(s => s.Delivered == isDelivered && ids.Contains(s.Id))
                .OrderByDescending(p => p.LoadStockChange.DateChanged)
                .ToList();

            foreach (var item in list)
            {
                item.LoadStockChange.ProductStockChanges.OrderBy(psc => psc.StoreProduct.Store.Address.City);
            }

            return list;
        }
    }
}
