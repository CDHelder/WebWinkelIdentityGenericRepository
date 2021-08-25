using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWinkelIdentity.Core;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.SpecificRepositories;

namespace WebWinkelIdentity.Data.Service.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public UOWProductRepository ProductRepository { get; }
        public UOWProductStockChangeRepository ProductStockChangeRepository { get; }
        public UOWBrandRepository BrandRepository { get; }
        public UOWCategoryRepository CategoryRepository { get; }
        public UOWStoreRepository StoreRepository { get; }
        public UOWStoreEmployeeRepository StoreEmployeeRepository { get; }
        public UOWStoreProductRepository StoreProductRepository { get; }
        public UOWCustomerRepository CustomerRepository { get; }
        public UOWEmployeeRepository EmployeeRepository { get; }
        public UOWAddressRepository AddressRepository { get; }
        public UOWLoadStockChangeRepository LoadStockChangeRepository { get; }
        bool SaveChanges();
    }
}
