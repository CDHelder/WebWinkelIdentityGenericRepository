using CSharpFunctionalExtensions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Queries
{
    public class UniqueProductListQuery : IRequest<Result<List<Product>>>
    {
    }

    public class UniqueProductListQueryHandler : IRequestHandler<UniqueProductListQuery, Result<List<Product>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public UniqueProductListQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<List<Product>>> Handle(UniqueProductListQuery request, CancellationToken cancellationToken)
        {
            var products = unitOfWork.ProductRepository.GetUniqueListProducts();

            return Task.FromResult(Result.Success(products));
        }
    }
}
