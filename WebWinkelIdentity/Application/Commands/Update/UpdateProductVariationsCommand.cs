using CSharpFunctionalExtensions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Commands
{
    public record UpdateProductVariationsCommand(Product Product, List<Product> Products) : IRequest<Result>
    {
    }

    public class UpdateProductVariationsCommandHandler : IRequestHandler<UpdateProductVariationsCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateProductVariationsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(UpdateProductVariationsCommand request, CancellationToken cancellationToken)
        {
            unitOfWork.ProductRepository.UpdateProductProperties(request.Product, request.Products);

            var result = unitOfWork.SaveChanges();
            if (result == true)
                return Task.FromResult(Result.Success());

            var errorMessage = $"Couldn't update product variations of product: {request.Product.Name} with id: {request.Product.Id}";
            return Task.FromResult(Result.Failure(errorMessage));
        }
    }
}
