using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Queries
{
    public record LoadStockChangeQuery(int Id) : IRequest<Result<LoadStockChange>>
    {
    }

    public class LoadStockChangeQueryHandler : IRequestHandler<LoadStockChangeQuery, Result<LoadStockChange>>
    {
        private readonly IUnitOfWork unitOfWork;

        public LoadStockChangeQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<LoadStockChange>> Handle(LoadStockChangeQuery request, CancellationToken cancellationToken)
        {
            var obj = unitOfWork.LoadStockChangeRepository.Get
                (
                filter: lsc => lsc.Id == request.Id,
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

            if (obj == null)
                return Task.FromResult(Result.Failure<LoadStockChange>($"Couldn't find product with id: {request.Id}"));
            if (obj.ProductStockChanges == null)
                return Task.FromResult(Result.Failure<LoadStockChange>($"Couldn't find associated product changes with loadstockid: {request.Id}"));

            return Task.FromResult(Result.Success(obj));
        }
    }
}
