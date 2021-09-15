using System;
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
        public UOWShipmentRepository ShipmentRepository { get; }
        bool SaveChanges();
    }
}
