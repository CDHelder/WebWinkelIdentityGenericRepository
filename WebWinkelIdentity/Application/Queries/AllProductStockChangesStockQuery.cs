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
    public class AllProductStockChangesStockQuery : IRequest<Result<List<ProductStockChange>>>
    {
    }

    public class AllProductStockChangesStockQueryHandler : IRequestHandler<AllProductStockChangesStockQuery, Result<List<ProductStockChange>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public AllProductStockChangesStockQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<List<ProductStockChange>>> Handle(AllProductStockChangesStockQuery request, CancellationToken cancellationToken)
        {
            var all = unitOfWork.ProductStockChangeRepository.GetAllProductStockChangesAndIncludes();
            return Task.FromResult(Result.Success(all));
        }
    }
}
