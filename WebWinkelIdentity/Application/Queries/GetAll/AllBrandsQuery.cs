using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Queries
{
    public class AllBrandsQuery : IRequest<Result<List<Brand>>>
    {
    }

    public class AllBrandsQueryHandler : IRequestHandler<AllBrandsQuery, Result<List<Brand>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public AllBrandsQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<List<Brand>>> Handle(AllBrandsQuery request, CancellationToken cancellationToken)
        {
            var all = unitOfWork.BrandRepository.GetAll(
                orderBy: a => a.OrderBy(p => p.Name),
                include: a => a.Include(p => p.Products).Include(p => p.Supplier)
                );

            if (all == null)
                return Task.FromResult(Result.Failure<List<Brand>>("Couldn't find any Brands"));

            return Task.FromResult(Result.Success(all));
        }
    }
}
