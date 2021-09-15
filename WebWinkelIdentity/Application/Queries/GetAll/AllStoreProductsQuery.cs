using CSharpFunctionalExtensions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Queries
{
    public record AllStoreProductsQuery(List<int> AllProductIds, int StoreId) : IRequest<Result<List<StoreProduct>>>
    {
    }

    public class AllStoreProductsQueryHandler : IRequestHandler<AllStoreProductsQuery, Result<List<StoreProduct>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public AllStoreProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<List<StoreProduct>>> Handle(AllStoreProductsQuery request, CancellationToken cancellationToken)
        {
            var storeProducts = unitOfWork.StoreProductRepository.GetAllStoreProducts(request.AllProductIds, request.StoreId);

            return Task.FromResult(Result.Success(storeProducts));
        }
    }
}
