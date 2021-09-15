using CSharpFunctionalExtensions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Queries
{
    public class AllProductStockChangesOrganizedQuery : IRequest<Result<List<LoadStockChange>>>
    {
    }

    public class AllProductStockChangesOrganizedQueryHandler : IRequestHandler<AllProductStockChangesOrganizedQuery, Result<List<LoadStockChange>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public AllProductStockChangesOrganizedQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<List<LoadStockChange>>> Handle(AllProductStockChangesOrganizedQuery request, CancellationToken cancellationToken)
        {
            var allLSC = unitOfWork.LoadStockChangeRepository.GetAllLoadStockChangesAndIncludes();

            return Task.FromResult(Result.Success(allLSC));
        }
    }
}
