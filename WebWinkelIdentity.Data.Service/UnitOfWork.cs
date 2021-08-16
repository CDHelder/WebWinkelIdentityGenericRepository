using WebWinkelIdentity.Core.StoreEntities;
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
        }

        //TODO: Refactor
        public UOWProductRepository ProductRepository 
        {
            get => productRepository ?? new UOWProductRepository(_dbContext);

            private set => productRepository = value;
        }

        public UOWProductStockChangeRepository ProductStockChangeRepository
        {
            get
            {
                if (this.productStockChangeRepository == null)
                {
                    this.productStockChangeRepository = new UOWProductStockChangeRepository(_dbContext);
                }
                return productStockChangeRepository;
            }
            private set
            {
                this.productStockChangeRepository = value;
            }
        }

        public UOWBrandRepository BrandRepository
        {
            get
            {
                if (this.brandRepository == null)
                {
                    this.brandRepository = new UOWBrandRepository(_dbContext);
                }
                return brandRepository;
            }
            private set
            {
                this.brandRepository = value;
            }
        }

        public UOWCategoryRepository CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new UOWCategoryRepository(_dbContext);
                }
                return categoryRepository;
            }
            private set
            {
                this.categoryRepository = value;
            }
        }

        public UOWStoreRepository StoreRepository
        {
            get
            {
                if (this.storeRepository == null)
                {
                    this.storeRepository = new UOWStoreRepository(_dbContext);
                }
                return storeRepository;
            }
            private set
            {
                this.storeRepository = value;
            }
        }

        public UOWStoreEmployeeRepository StoreEmployeeRepository
        {
            get
            {
                if (this.storeEmployeeRepository == null)
                {
                    this.storeEmployeeRepository = new UOWStoreEmployeeRepository(_dbContext);
                }
                return storeEmployeeRepository;
            }
            private set
            {
                this.storeEmployeeRepository = value;
            }
        }

        public UOWStoreProductRepository StoreProductRepository
        {
            get
            {
                if (this.storeProductRepository == null)
                {
                    this.storeProductRepository = new UOWStoreProductRepository(_dbContext);
                }
                return storeProductRepository;
            }
            private set
            {
                this.storeProductRepository = value;
            }
        }

        public UOWCustomerRepository CustomerRepository
        {
            get
            {
                if (this.customerRepository == null)
                {
                    this.customerRepository = new UOWCustomerRepository(_dbContext);
                }
                return customerRepository;
            }
            private set
            {
                this.customerRepository = value;
            }
        }

        public UOWEmployeeRepository EmployeeRepository
        {
            get
            {
                if (this.employeeRepository == null)
                {
                    this.employeeRepository = new UOWEmployeeRepository(_dbContext);
                }
                return employeeRepository;
            }
            private set
            {
                this.employeeRepository = value;
            }
        }

        public UOWAddressRepository AddressRepository
        {
            get
            {
                if (this.addressRepository == null)
                {
                    this.addressRepository = new UOWAddressRepository(_dbContext);
                }
                return addressRepository;
            }
            private set
            {
                this.addressRepository = value;
            }
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
