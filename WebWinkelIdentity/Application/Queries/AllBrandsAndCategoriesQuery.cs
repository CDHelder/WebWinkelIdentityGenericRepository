using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Queries
{
    public class AllBrandsAndCategoriesQuery : IRequest<Result<AllBrandsAndCategories>>
    {

    }

    public class AllBrandsAndCategoriesHandler : IRequestHandler<AllBrandsAndCategoriesQuery, Result<AllBrandsAndCategories>>
    {
        private readonly IUnitOfWork unitOfWork;

        public AllBrandsAndCategoriesHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<AllBrandsAndCategories>> Handle(AllBrandsAndCategoriesQuery request, CancellationToken cancellationToken)
        {
            var allBrandsAndCategories = new AllBrandsAndCategories
                (
                unitOfWork.CategoryRepository.GetAll(), 
                unitOfWork.BrandRepository.GetAll()
                );

            return Task.FromResult(Result.Success(allBrandsAndCategories));
        }

    }

    public record AllBrandsAndCategories(List<Category> Categories, List<Brand> Brands)
    {

    }
}
