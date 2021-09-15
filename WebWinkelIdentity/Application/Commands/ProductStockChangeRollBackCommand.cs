using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Commands
{
    public record ProductStockChangeRollBackCommand(int Id) : IRequest<Result>
    {
    }

    public class ProductStockChangeRollBackCommandHandler : IRequestHandler<ProductStockChangeRollBackCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMediator mediator;

        public ProductStockChangeRollBackCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            this.unitOfWork = unitOfWork;
            this.mediator = mediator;
        }

        public Task<Result> Handle(ProductStockChangeRollBackCommand request, CancellationToken cancellationToken)
        {
            var PSC = unitOfWork.ProductStockChangeRepository.Get(
                filter: psc => psc.Id == request.Id,
                include: psc => psc.Include(p => p.StoreProduct)
                .ThenInclude(sp => sp.Product)
                .ThenInclude(p => p.Brand)
                .Include(p => p.StoreProduct));

            var newStockValue = PSC.StoreProduct.Quantity + PSC.StockChange;
            if (newStockValue < 0)
            {
                var errorMessage = $"Rollback value: {PSC.StoreProduct.Quantity} + {PSC.StockChange} = {newStockValue}, can't make stock negative";
                return Task.FromResult(Result.Failure(errorMessage));
            }

            PSC.StoreProduct.Quantity = newStockValue;
            unitOfWork.StoreProductRepository.Update(PSC.StoreProduct);
            if (unitOfWork.SaveChanges() == false)
            {
                var errorMessage = $"Couldnt save Stock changes made to {PSC.StoreProduct.Product.Name} from {PSC.StoreProduct.Quantity} to {newStockValue}";
                return Task.FromResult(Result.Failure(errorMessage));
            }

            var LSC = unitOfWork.LoadStockChangeRepository.Get(filter: lsc => lsc.ProductStockChanges.Any(x => x.Id == request.Id));
            if (LSC.ProductStockChanges.Count() == 1)
            {
                unitOfWork.LoadStockChangeRepository.Delete(LSC);
            }

            unitOfWork.ProductStockChangeRepository.Delete(PSC);
            if (unitOfWork.SaveChanges() == false)
            {
                var errorMessage = $"Couldnt remove ProductStockChange log information from database";
                return Task.FromResult(Result.Failure(errorMessage));
            }

            return Task.FromResult(Result.Success());
        }
    }
}
