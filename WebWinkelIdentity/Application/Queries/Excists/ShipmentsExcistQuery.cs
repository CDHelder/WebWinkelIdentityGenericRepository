using CSharpFunctionalExtensions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Queries
{
    public record ShipmentsExcistQuery(string[] Ids) : IRequest<Result>
    {
    }

    public class ShipmentsExcistQueryHandler : IRequestHandler<ShipmentsExcistQuery, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public ShipmentsExcistQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(ShipmentsExcistQuery request, CancellationToken cancellationToken)
        {
            var intIds = request.Ids.Select(x => int.Parse(x)).ToList();
            var notExcistingIds = new List<int>();

            foreach (var Id in intIds)
            {
                var shipment = unitOfWork.ShipmentRepository.GetById(Id);
                if (shipment == null)
                {
                    notExcistingIds.Add(Id);
                }
            }

            if (notExcistingIds.Count() > 0)
            {
                var error = string.Join(", ", notExcistingIds);
                return Task.FromResult(Result.Failure($"Couldn't find shipments with ids: {error}"));
            }

            return Task.FromResult(Result.Success());
        }
    }
}
