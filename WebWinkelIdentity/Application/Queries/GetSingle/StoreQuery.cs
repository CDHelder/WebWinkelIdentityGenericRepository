using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Queries
{
    public record StoreQuery(int Id) : IRequest<Result<Store>>
    {
    }

    public class StoreQueryHandler : IRequestHandler<StoreQuery, Result<Store>>
    {
        private readonly IUnitOfWork unitOfWork;

        public StoreQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<Store>> Handle(StoreQuery request, CancellationToken cancellationToken)
        {
            var store = unitOfWork.StoreRepository.Get
                (
                filter: s => s.Id == request.Id,
                include: s => s
                .Include(sp => sp.WeekOpeningTimes)
                .Include(sp => sp.StoreEmployees)
                .ThenInclude(se => se.Employee)
                .Include(sp => sp.Address)
                );

            if (store == null)
                return Task.FromResult(Result.Failure<Store>($"Couldn't find store with id: {request.Id}"));

            return Task.FromResult(Result.Success(store));
        }
    }
}
