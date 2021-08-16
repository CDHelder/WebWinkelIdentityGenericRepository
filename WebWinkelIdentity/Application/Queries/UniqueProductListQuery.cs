using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Queries
{
    public class UniqueProductListQuery : IRequest<List<Product>>
    {

    }
    public class UniqueProductListQueryHandler : IRequestHandler<UniqueProductListQuery, List<Product>>
    {
        private readonly IUnitOfWork unitOfWork;

        public UniqueProductListQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<List<Product>> Handle(UniqueProductListQuery request, CancellationToken cancellationToken)
        {
            var products = unitOfWork.ProductRepository.GetUniqueListProducts();
            return Task.FromResult(products);
        }
    }
}
