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
    public record ShipmentsDeliveryCommand(List<int> Ids, string UserId) : IRequest<Result>
    {
    }

    public class ShipmentsDeliveryCommandHandler : IRequestHandler<ShipmentsDeliveryCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public ShipmentsDeliveryCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(ShipmentsDeliveryCommand request, CancellationToken cancellationToken)
        {
            if (request.Ids == null)
            {
                return Task.FromResult(Result.Failure("To confirm delivery please give the shipment ids"));
            }

            var shipments = unitOfWork.ShipmentRepository.GetAllShipmentsAndIncludes(false, request.Ids);

            foreach (var shipment in shipments)
            {
                LoadStockChange LSC = new LoadStockChange();
                LSC.UserId = request.UserId;
                LSC.DateChanged = DateTime.Now;
                LSC.ProductStockChanges = new();
                LSC.ShipmentId = shipment.Id;

                foreach (var PSC in shipment.LoadStockChange.ProductStockChanges)
                {
                    var deliveryStoreProduct = unitOfWork.StoreProductRepository.Get(filter: a => a.ProductId == PSC.StoreProduct.ProductId && a.StoreId == shipment.StoreId);
                    var stockChange = PSC.StockChange * -1;

                    deliveryStoreProduct.Quantity += stockChange;

                    unitOfWork.StoreProductRepository.Update(deliveryStoreProduct);

                    if (unitOfWork.SaveChanges() == true)
                    {
                        LSC.ProductStockChanges.Add(
                            new ProductStockChange
                            {
                                StoreProductId = deliveryStoreProduct.Id,
                                StockChange = stockChange
                            });
                    }
                }

                unitOfWork.LoadStockChangeRepository.Create(LSC);
                if (unitOfWork.SaveChanges() == false)
                {
                    return Task.FromResult(Result.Failure("Couldn't save LoadStockChange"));
                }

                shipment.DeliveredTime = DateTime.Now;
                shipment.Delivered = true;
                shipment.UserId = request.UserId;

                unitOfWork.ShipmentRepository.Update(shipment);
                if (unitOfWork.SaveChanges() == false)
                {
                    return Task.FromResult(Result.Failure("Couldn't save shipment changes"));
                }
            }

            return Task.FromResult(Result.Success());
        }
    }
}
