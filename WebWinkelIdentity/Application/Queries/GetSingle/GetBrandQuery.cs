using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Queries
{
    public record GetBrandQuery(int Id) : IRequest<Result<Brand>>
    {
    }

    public class GetBrandQueryHandler : IRequestHandler<GetBrandQuery, Result<Brand>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetBrandQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<Brand>> Handle(GetBrandQuery request, CancellationToken cancellationToken)
        {
            var brand = unitOfWork.BrandRepository.Get(
                filter: a => a.Id == request.Id,
                include: a => a.Include(s => s.Products).Include(s => s.Products)
                );

            if (brand == null)
                return Task.FromResult(Result.Failure<Brand>($"Couldn't find brand with id: {request.Id}"));

            return Task.FromResult(Result.Success(brand));
        }
    }
}
