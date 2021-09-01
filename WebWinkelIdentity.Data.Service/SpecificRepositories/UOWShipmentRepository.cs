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

    }
}
