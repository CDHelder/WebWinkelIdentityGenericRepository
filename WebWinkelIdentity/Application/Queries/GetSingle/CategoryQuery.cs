using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Queries
{
    public record CategoryQuery(int Id) : IRequest<Result<Category>>
    {
    }

    public class CategoryQueryHandler : IRequestHandler<CategoryQuery, Result<Category>>
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<Category>> Handle(CategoryQuery request, CancellationToken cancellationToken)
        {
            var category = unitOfWork.CategoryRepository.Get(
                filter: a => a.Id == request.Id,
                include: a => a.Include(p => p.Products)
                );

            if (category == null)
                return Task.FromResult(Result.Failure<Category>($"Couldn't find category with id: {request.Id}"));

            return Task.FromResult(Result.Success(category));
        }
    }
}
