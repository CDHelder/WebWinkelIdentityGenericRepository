using CSharpFunctionalExtensions;
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
    public record StoreUniqueProductListQuery(int Id) : IRequest<Result<List<Product>>>
    {
    }

    public class StoreUniqueProductListQueryHandler : IRequestHandler<StoreUniqueProductListQuery, Result<List<Product>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public StoreUniqueProductListQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<List<Product>>> Handle(StoreUniqueProductListQuery request, CancellationToken cancellationToken)
        {
            var products = unitOfWork.ProductRepository.GetStoreUniqueListProducts(request.Id);

            if (products == null)
            {
                return Task.FromResult(Result.Failure<List<Product>>($"Couldn't find any products that are related to store with id: {request.Id}"));
            }

            return Task.FromResult(Result.Success(products));
        }
    }
}
