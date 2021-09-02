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
    public record ShipmentQuery(int Id) : IRequest<Result<Shipment>>
    {
    }

    public class ShipmentQueryHandler : IRequestHandler<ShipmentQuery, Result<Shipment>>
    {
        private readonly IUnitOfWork unitOfWork;

        public ShipmentQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<Shipment>> Handle(ShipmentQuery request, CancellationToken cancellationToken)
        {
            var shipment = unitOfWork.ShipmentRepository.GetShipmentAndIncludes(request.Id);

            if (shipment == null)
            {
                return Task.FromResult(Result.Failure<Shipment>($"Couldn't find shipment with id: {request.Id}"));
            }

            return Task.FromResult(Result.Success(shipment));
        }
    }
}
