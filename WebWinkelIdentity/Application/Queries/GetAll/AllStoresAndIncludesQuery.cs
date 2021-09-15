using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Queries
{
    public class AllStoresAndIncludesQuery : IRequest<Result<List<Store>>>
    {
    }

    public class AllStoresAndIncludesQueryHandler : IRequestHandler<AllStoresAndIncludesQuery, Result<List<Store>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public AllStoresAndIncludesQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<List<Store>>> Handle(AllStoresAndIncludesQuery request, CancellationToken cancellationToken)
        {
            var stores = unitOfWork.StoreRepository.GetAll(
                include: s => s
                .Include(sp => sp.WeekOpeningTimes)
                .Include(sp => sp.StoreEmployees)
                .ThenInclude(se => se.Employee)
                .Include(sp => sp.Address)
                );

            if (stores == null)
                return Task.FromResult(Result.Failure<List<Store>>("Couldn't find any stores"));

            return Task.FromResult(Result.Success(stores));
        }
    }
}
