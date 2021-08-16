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
    public class AllBrandsAndCategoriesQuery : IRequest<AllBrandsAndCategories>
    {

    }

    public class AllBrandsAndCategoriesHandler : IRequestHandler<AllBrandsAndCategoriesQuery, AllBrandsAndCategories>
    {
        private readonly IUnitOfWork unitOfWork;

        public AllBrandsAndCategoriesHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<AllBrandsAndCategories> Handle(AllBrandsAndCategoriesQuery request, CancellationToken cancellationToken)
        {
            var allBrandsAndCategories = new AllBrandsAndCategories
                (
                unitOfWork.CategoryRepository.GetAll(), 
                unitOfWork.BrandRepository.GetAll()
                );

            return Task.FromResult(allBrandsAndCategories);
        }

    }

    public record AllBrandsAndCategories(List<Category> Categories, List<Brand> Brands)
    {

    }
}
