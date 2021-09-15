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
    public class AllCategoriesQuery : IRequest<Result<List<Category>>>
    {
    }

    public class AllCategoriesQueryHandler : IRequestHandler<AllCategoriesQuery, Result<List<Category>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public AllCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<List<Category>>> Handle(AllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var all = unitOfWork.CategoryRepository.GetAll(
                orderBy: a => a.OrderBy(s => s.Name),
                include: a => a.Include(s => s.Products)
                );

            return Task.FromResult(Result.Success(all));
        }
    }
}
