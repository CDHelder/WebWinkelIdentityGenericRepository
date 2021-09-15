using WebWinkelIdentity.Data.Service.Interfaces;
using WebWinkelIdentity.Data.Service.SpecificRepositories;

namespace WebWinkelIdentity.Data.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private UOWProductRepository productRepository;
        private UOWBrandRepository brandRepository;
        private UOWCategoryRepository categoryRepository;
        private UOWProductStockChangeRepository productStockChangeRepository;
        private UOWStoreRepository storeRepository;
        private UOWStoreEmployeeRepository storeEmployeeRepository;
        private UOWStoreProductRepository storeProductRepository;
        private UOWCustomerRepository customerRepository;
        private UOWEmployeeRepository employeeRepository;
        private UOWAddressRepository addressRepository;
        private UOWLoadStockChangeRepository loadStockChangeRepository;
        private UOWShipmentRepository shipmentRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            ProductRepository = productRepository;
            ProductStockChangeRepository = productStockChangeRepository;
            BrandRepository = brandRepository;
            CategoryRepository = categoryRepository;
            StoreRepository = storeRepository;
            StoreEmployeeRepository = storeEmployeeRepository;
            StoreProductRepository = storeProductRepository;
            CustomerRepository = customerRepository;
            EmployeeRepository = employeeRepository;
            AddressRepository = addressRepository;
            LoadStockChangeRepository = loadStockChangeRepository;
            ShipmentRepository = shipmentRepository;
        }

        public UOWProductRepository ProductRepository 
        {
            get => productRepository ?? new UOWProductRepository(_dbContext);
            private set => productRepository = value;
        }

        public UOWProductStockChangeRepository ProductStockChangeRepository
        {
            get => productStockChangeRepository ?? new UOWProductStockChangeRepository(_dbContext);
            private set => productStockChangeRepository = value;
        }

        public UOWBrandRepository BrandRepository
        {
            get => brandRepository ?? new UOWBrandRepository(_dbContext);
            private set => brandRepository = value;
        }

        public UOWCategoryRepository CategoryRepository
        {
            get => categoryRepository ?? new UOWCategoryRepository(_dbContext);
            private set => categoryRepository = value;
        }

        public UOWStoreRepository StoreRepository
        {
            get => storeRepository ?? new UOWStoreRepository(_dbContext);
            private set => storeRepository = value;
        }

        public UOWStoreEmployeeRepository StoreEmployeeRepository
        {
            get => storeEmployeeRepository ?? new UOWStoreEmployeeRepository(_dbContext);
            private set => storeEmployeeRepository = value;
        }

        public UOWStoreProductRepository StoreProductRepository
        {
            get => storeProductRepository ?? new UOWStoreProductRepository(_dbContext);
            private set => storeProductRepository = value;
        }

        public UOWCustomerRepository CustomerRepository
        {
            get => customerRepository ?? new UOWCustomerRepository(_dbContext);
            private set => customerRepository = value;
        }

        public UOWEmployeeRepository EmployeeRepository
        {
            get => employeeRepository ?? new UOWEmployeeRepository(_dbContext);
            private set => employeeRepository = value;
        }

        public UOWAddressRepository AddressRepository
        {
            get => addressRepository ?? new UOWAddressRepository(_dbContext);
            private set => addressRepository = value;
        }
        public UOWLoadStockChangeRepository LoadStockChangeRepository
        {
            get => loadStockChangeRepository ?? new UOWLoadStockChangeRepository(_dbContext);
            private set => loadStockChangeRepository = value;
        }
        public UOWShipmentRepository ShipmentRepository
        {
            get => shipmentRepository ?? new UOWShipmentRepository(_dbContext);
            private set => shipmentRepository = value;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public bool SaveChanges()
        {
            var rowsChanged = _dbContext.SaveChanges();
            if (rowsChanged > 0)
                return true;

            return false;
        }
    }
}
