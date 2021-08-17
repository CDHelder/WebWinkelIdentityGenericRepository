using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Queries
{
    public record BoolProductsAndStoreExcistValidationQuery(IEnumerable<string> IdsList, string StoreId) : IRequest<ProductsAndStoreExcistRepsonse>
    {
    }

    public record ProductsAndStoreExcistRepsonse(bool AllProductsAndStoreExcist, List<int> NotExcistingProductIds = null, int StoreId = 0, string ErrorMessage = null)
    {
    }

    public class BoolProductsAndStoreExcistValidationQueryHandler : IRequestHandler<BoolProductsAndStoreExcistValidationQuery, ProductsAndStoreExcistRepsonse>
    {
        private readonly IUnitOfWork unitOfWork;

        public BoolProductsAndStoreExcistValidationQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<ProductsAndStoreExcistRepsonse> Handle(BoolProductsAndStoreExcistValidationQuery request, CancellationToken cancellationToken)
        {
            //Store Validation
            var store = unitOfWork.StoreRepository.GetById(int.Parse(request.StoreId));

            if (store == null)
            {
                var productsAndStoreExcistRepsonse = new ProductsAndStoreExcistRepsonse(false, StoreId: int.Parse(request.StoreId),
                    ErrorMessage: $"Error: Couldn't find store with id: {request.StoreId}");
                return Task.FromResult(productsAndStoreExcistRepsonse);
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

                var productsAndStoreExcistRepsonse = new ProductsAndStoreExcistRepsonse(false, notExcistingProductIds, int.Parse(request.StoreId),
                    ErrorMessage: $"Error: Couldnt find product with ids:{notExcistingProductIds} in the database");
                return Task.FromResult(productsAndStoreExcistRepsonse);
            }

            //Return true if All Validation Passes
            return Task.FromResult(new ProductsAndStoreExcistRepsonse(true));
        }
    }
}
