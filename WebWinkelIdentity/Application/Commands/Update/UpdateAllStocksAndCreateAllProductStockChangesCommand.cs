using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Commands
{
    public record UpdateAllStocksAndCreateAllProductStockChangesCommand(List<StoreProduct> StoreProducts, List<int> AllProductIds, string UserId, bool AddStock, bool IsShipment, int DeliveryLocationStoreId = 0) : IRequest<Result<int>>
    {
    }

    public class UpdateAllStocksAndCreateAllProductStockChangesCommandHandler : IRequestHandler<UpdateAllStocksAndCreateAllProductStockChangesCommand, Result<int>>
    {
        private readonly IUnitOfWork unitOfWork;
        private LoadStockChange LSC;

        public UpdateAllStocksAndCreateAllProductStockChangesCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.LSC = new LoadStockChange();
            this.LSC.ProductStockChanges = new();
        }

        public Task<Result<int>> Handle(UpdateAllStocksAndCreateAllProductStockChangesCommand request, CancellationToken cancellationToken)
        {
            List<int> notSaveableProductIds = new();
            List<int> notLoggableProductIds = new();

            LSC.UserId = request.UserId;
            LSC.DateChanged = DateTime.Now;

            foreach (var storeProduct in request.StoreProducts)
            {
                var changeQuantity = request.AllProductIds.Where(x => x == storeProduct.ProductId).Count();
                if (request.AddStock == true)
                    storeProduct.Quantity += changeQuantity;
                else if (request.AddStock == false)
                    storeProduct.Quantity -= changeQuantity;

                unitOfWork.StoreProductRepository.Update(storeProduct);
                if (unitOfWork.SaveChanges() == false)
                    notSaveableProductIds.Add(storeProduct.ProductId);

                if (CreateAndSaveProductStockChanges(storeProduct, changeQuantity, request.UserId, request.AddStock) == false)
                    notLoggableProductIds.Add(storeProduct.ProductId);
            }

            //Validation
            if (notSaveableProductIds.Count() > 0)
            {
                var notSaveProdIds = string.Join(", ", notSaveableProductIds.Select(i => i.ToString()).ToArray());
                var productErrorMessage = $"Error: Couldnt save changes to product with id:{notSaveProdIds}";
                return Task.FromResult(Result.Failure<int>(productErrorMessage));
            }
            else if (notLoggableProductIds.Count() > 0)
            {
                var notLogProdIds = string.Join(", ", notLoggableProductIds.Select(i => i.ToString()).ToArray());
                var productErrorMessage = $"Error: Couldnt log the changes made to product with id:{notLogProdIds}";
                return Task.FromResult(Result.Failure<int>(productErrorMessage));
            }

            unitOfWork.LoadStockChangeRepository.Create(LSC);

            Shipment shipment = new();
            if (request.IsShipment == true && request.DeliveryLocationStoreId != 0)
            {
                shipment = new Shipment { Delivered = false, DeliveredTime = null, LoadStockChange = LSC, StoreId = request.DeliveryLocationStoreId };
                unitOfWork.ShipmentRepository.Create(shipment);
            }

            if (unitOfWork.SaveChanges() == false)
                return Task.FromResult(Result.Failure<int>($"No changes were saved in the Database"));

            if (request.IsShipment == true && request.DeliveryLocationStoreId != 0)
                return Task.FromResult(Result.Success(shipment.Id));
            else
                return Task.FromResult(Result.Success(LSC.Id));
        }

        private bool CreateAndSaveProductStockChanges(StoreProduct storeProduct, int changeQuantity, string userId, bool addStock)
        {
            if (addStock == false)
                changeQuantity = -changeQuantity;

            ProductStockChange PSC = new ProductStockChange
            {
                StoreProductId = storeProduct.Id,
                StockChange = changeQuantity
            };

            LSC.ProductStockChanges.Add(PSC);

            if (LSC.ProductStockChanges.Contains(PSC) == true)
                return true;

            return false;
        }
    }
}
