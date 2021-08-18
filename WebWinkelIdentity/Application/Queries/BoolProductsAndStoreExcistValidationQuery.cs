using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Queries
{
    public record BoolProductsAndStoreExcistValidationQuery(IEnumerable<string> IdsList, string StoreId) : IRequest<Result>
    {
    }

    public class BoolProductsAndStoreExcistValidationQueryHandler : IRequestHandler<BoolProductsAndStoreExcistValidationQuery, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public BoolProductsAndStoreExcistValidationQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(BoolProductsAndStoreExcistValidationQuery request, CancellationToken cancellationToken)
        {
            //Store Validation
            var store = unitOfWork.StoreRepository.GetById(int.Parse(request.StoreId));

            if (store == null)
            { 
                var errorMessage = $"Error: Couldn't find store with id: {request.StoreId}";
                return Task.FromResult(Result.Failure(errorMessage));
            }

            //ProductIds Validation
            var notExcistingProductIds = new List<int>();

            var distinctList = request.IdsList.Distinct();
            foreach (var productId in distinctList)
            {
                if (productId == "")
                {
                    notExcistingProductIds.Add(int.Parse(productId));
                    continue;
                }

                var product = unitOfWork.ProductRepository.GetById(int.Parse(productId));
                if (product == null)
                {
                    notExcistingProductIds.Add(int.Parse(productId));
                    continue;
                }
            }

            if (notExcistingProductIds.Count > 0)
            {
                var productIds = string.Join(", ", notExcistingProductIds.Select(i => i.ToString()).ToArray());
                var errorMessage = $"Error: Couldnt find product with ids: {productIds} in the database";
                return Task.FromResult(Result.Failure(errorMessage));
            }

            //Return true if All Validation Passes
            return Task.FromResult(Result.Success());
        }
    }
}
