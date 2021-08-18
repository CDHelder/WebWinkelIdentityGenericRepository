using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Commands
{
    public record CreateAllProductStockChangesCommand(List<StoreProduct> StoreProducts, Dictionary<int,int> BeforeChangeStockValues, string UserId) : IRequest<Result>
    {
    }

    public class CreateAllProductStockChangesCommandHandler : IRequestHandler<CreateAllProductStockChangesCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateAllProductStockChangesCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(CreateAllProductStockChangesCommand request, CancellationToken cancellationToken)
        {
            foreach (var storeProduct in request.StoreProducts)
            {
                if (storeProduct.Quantity - request.BeforeChangeStockValues[storeProduct.Id] != 0)
                {
                    var PSC = new ProductStockChange
                    {
                        UserId = request.UserId,
                        DateChanged = DateTime.Now,
                        StockChange = storeProduct.Quantity - request.BeforeChangeStockValues[storeProduct.Id],
                        StoreProductId = storeProduct.Id
                    };

                    unitOfWork.ProductStockChangeRepository.Create(PSC);
                }
            }

            var result = unitOfWork.SaveChanges();
            if (result == true)
                return Task.FromResult(Result.Success());

            var productIds = string.Join(", ", request.StoreProducts.Select(sp => sp.ProductId).ToList());
            return Task.FromResult(Result.Failure($"Couldn't log the changes made to products with ids: {productIds}"));

        }
    }
}
