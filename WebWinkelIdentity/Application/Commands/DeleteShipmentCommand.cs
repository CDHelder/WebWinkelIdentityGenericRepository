using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Commands
{
    public record DeleteShipmentCommand(int Id) : IRequest<Result>
    {
    }

    public class DeleteShipmentCommandHandler : IRequestHandler<DeleteShipmentCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteShipmentCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(DeleteShipmentCommand request, CancellationToken cancellationToken)
        {
            var shipment = unitOfWork.ShipmentRepository.GetById(request.Id);

            if (shipment == null)
            {
                return Task.FromResult(Result.Failure($"Couldn't find shipment with id: {request.Id}"));
            }

            unitOfWork.ShipmentRepository.Delete(shipment);

            if (unitOfWork.SaveChanges() == false)
            {
                return Task.FromResult(Result.Failure($"Couldn't delete shipment"));
            }

            return Task.FromResult(Result.Success());
        } 
    }
}
