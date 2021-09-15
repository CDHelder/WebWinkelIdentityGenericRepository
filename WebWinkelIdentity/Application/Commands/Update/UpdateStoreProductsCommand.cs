using CSharpFunctionalExtensions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Commands
{
    public record UpdateStoreProductsCommand(List<StoreProduct> StoreProducts) : IRequest<Result>
    {
    }

    public class UpdateStoreProductsCommandHandler : IRequestHandler<UpdateStoreProductsCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateStoreProductsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(UpdateStoreProductsCommand request, CancellationToken cancellationToken)
        {
            unitOfWork.StoreProductRepository.Update(request.StoreProducts);

            var result = unitOfWork.SaveChanges();
            if (result == true)
                return Task.FromResult(Result.Success());

            var productNames = string.Join(", ", request.StoreProducts.Select(sp => sp.Product.Name).ToList());
            var productIds = string.Join(", ", request.StoreProducts.Select(sp => sp.ProductId).ToList());
            var errorMessage = $"Products {productNames} with ids: {productIds} couldn't be created and saved in the database";

            return Task.FromResult(Result.Failure(errorMessage));
        }   
    }
}
