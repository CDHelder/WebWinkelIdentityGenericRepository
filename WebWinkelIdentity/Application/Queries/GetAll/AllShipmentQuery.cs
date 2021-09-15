using CSharpFunctionalExtensions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Queries
{
    public record AllShipmentQuery(bool IsDelivered, List<int> Ids = null) : IRequest<Result<List<Shipment>>>
    {
    }

    public class AllShipmentQueryHandler : IRequestHandler<AllShipmentQuery, Result<List<Shipment>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public AllShipmentQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<List<Shipment>>> Handle(AllShipmentQuery request, CancellationToken cancellationToken)
        {
            var shipslmao = new List<Shipment>();

            if (request.Ids == null)
                shipslmao = unitOfWork.ShipmentRepository.GetAllShipmentsAndIncludes(request.IsDelivered);
            else if(request.Ids != null)
                shipslmao = unitOfWork.ShipmentRepository.GetAllShipmentsAndIncludes(request.IsDelivered, request.Ids);

            if (shipslmao == null)
                return Task.FromResult(Result.Failure<List<Shipment>>("Couldn't find any shipments"));

            return Task.FromResult(Result.Success(shipslmao));
        }
    }
}
