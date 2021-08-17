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
    public record UpdateAllStocksAndCreateAllProductStockChangesCommand(List<StoreProduct> StoreProducts, List<int> AllProductIds, string UserId, bool AddStock) : IRequest<Result>
    {
    }

    public class UpdateAllStocksAndCreateAllProductStockChangesCommandHandler : IRequestHandler<UpdateAllStocksAndCreateAllProductStockChangesCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateAllStocksAndCreateAllProductStockChangesCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(UpdateAllStocksAndCreateAllProductStockChangesCommand request, CancellationToken cancellationToken)
        {
            List<int> notSaveableProductIds = new();
            List<int> notLoggableProductIds = new();
            foreach (var storeProduct in request.StoreProducts)
            {
                var changeQuantity = request.AllProductIds.Where(x => x == storeProduct.ProductId).Count();
                if (request.AddStock == true)
                {
                    storeProduct.Quantity += changeQuantity;
                }
                else if (request.AddStock == false)
                {
                    storeProduct.Quantity -= changeQuantity;

                }

                unitOfWork.StoreProductRepository.Update(storeProduct);
                if (unitOfWork.SaveChanges() == false)
                {
                    notSaveableProductIds.Add(storeProduct.ProductId);
                }

                if (CreateAndSaveProductStockChanges(storeProduct, changeQuantity, request.UserId, request.AddStock) == false)
                {
                    notLoggableProductIds.Add(storeProduct.ProductId);
                }
            }

            //Validation
            if (notSaveableProductIds.Count() > 0)
            {
                var notSaveProdIds = string.Join(", ", notSaveableProductIds.Select(i => i.ToString()).ToArray());
                var productErrorMessage = $"Error: Couldnt save changes to product with id:{notSaveProdIds}";
                return Task.FromResult(Result.Failure(productErrorMessage));
            }
            else if (notLoggableProductIds.Count() > 0)
            {
                var notLogProdIds = string.Join(", ", notLoggableProductIds.Select(i => i.ToString()).ToArray());
                var productErrorMessage = $"Error: Couldnt log the changes made to product with id:{notLogProdIds}";
                return Task.FromResult(Result.Failure(productErrorMessage));
            }

            return Task.FromResult(Result.Success());
        }

        private bool CreateAndSaveProductStockChanges(StoreProduct storeProduct, int changeQuantity, string userId, bool addStock)
        {
            if (addStock == false)
                changeQuantity = -changeQuantity;

            ProductStockChange PSC = new ProductStockChange
            {
                UserId = userId,
                DateChanged = DateTime.Now,
                StoreProductId = storeProduct.Id,
                StockChange = changeQuantity
            };

            unitOfWork.ProductStockChangeRepository.Create(PSC);
            if (unitOfWork.SaveChanges() == false)
            {
                return false;
            }

            return true;
        }
    }
}
