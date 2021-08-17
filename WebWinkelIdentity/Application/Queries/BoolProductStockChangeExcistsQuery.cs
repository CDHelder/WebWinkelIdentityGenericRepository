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
    public record BoolProductStockChangeExcistsQuery(string[] List, int SelectedStoreId) : IRequest<Result>
    {
    }

    public class BoolProductStockChangeExcistsQueryHandler : IRequestHandler<BoolProductStockChangeExcistsQuery, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public BoolProductStockChangeExcistsQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(BoolProductStockChangeExcistsQuery request, CancellationToken cancellationToken)
        {
            var intList = request.List.Select(x => int.Parse(x)).ToList();
            var productIds = string.Join(", ", intList.Select(i => i.ToString()).ToArray());
            foreach (var id in unitOfWork.StoreProductRepository.GetAllStoreProducts(intList, request.SelectedStoreId))
            {
                var enteredStockChange = intList.Where(adat => adat == id.ProductId).Count();
                var currentStock = id.Quantity;
                if (currentStock < enteredStockChange)
                {
                    var errorMessage = $"Error: Cant delete products with ids: {productIds} " +
                        $"because current stock is smaller then entered change";

                    return Task.FromResult(Result.Failure(errorMessage));
                }
            }

            return Task.FromResult(Result.Success());
        }
    }
}
