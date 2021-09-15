using CSharpFunctionalExtensions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Commands
{
    public record DeleteProductVariationsCommand(int Id) : IRequest<Result>
    {

    }
    public class ProductDeleteCommandHandler : IRequestHandler<DeleteProductVariationsCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductDeleteCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<Result> Handle(DeleteProductVariationsCommand request, CancellationToken cancellationToken)
        {
            var product = unitOfWork.ProductRepository.GetById(request.Id);
            var productVariations = unitOfWork.ProductRepository.GetProductVariations(product);
            unitOfWork.ProductRepository.Delete(productVariations);

            var result = unitOfWork.SaveChanges();
            if (result == true)
                return Task.FromResult(Result.Success());

            var stringProductSizes = string.Join(", ", productVariations.Select(pv => pv.Size).ToList());
            return Task.FromResult(Result.Failure($"Coudn't delete all products variations with sizes: {stringProductSizes}"));
        }
    }
}
