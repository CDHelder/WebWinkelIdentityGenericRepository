using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Queries
{
    public class AllLoadStockChangesStockQuery : IRequest<Result<List<LoadStockChange>>>
    {
    }

    public class AllLoadStockChangesStockQueryHandler : IRequestHandler<AllLoadStockChangesStockQuery, Result<List<LoadStockChange>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public AllLoadStockChangesStockQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<List<LoadStockChange>>> Handle(AllLoadStockChangesStockQuery request, CancellationToken cancellationToken)
        {
            var all = unitOfWork.LoadStockChangeRepository.GetAll
                (
                include: lsc => lsc
                .Include(a => a.ProductStockChanges)
                .ThenInclude(psc => psc.StoreProduct)
                .ThenInclude(sp => sp.Product)
                .Include(a => a.ProductStockChanges)
                .ThenInclude(psc => psc.StoreProduct)
                .ThenInclude(sp => sp.Store)
                .ThenInclude(s => s.Address)
                .Include(a => a.ProductStockChanges)
                .ThenInclude(psc => psc.StoreProduct)
                .ThenInclude(sp => sp.Product)
                .ThenInclude(p => p.Brand)
                .Include(a => a.ProductStockChanges)
                .ThenInclude(psc => psc.StoreProduct)
                .ThenInclude(sp => sp.Product)
                .ThenInclude(p => p.Category)
                .Include(a => a.AssociatedUser)
                );

            return Task.FromResult(Result.Success(all));
        }
    }
}
