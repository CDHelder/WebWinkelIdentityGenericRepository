using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Commands
{
    //TODO: Implement Alle Commands
    public record DeleteProductVariationsCommand(int Id) : IRequest<bool>
    {

    }
    public class ProductDeleteCommandHandler : IRequestHandler<DeleteProductVariationsCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductDeleteCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<bool> Handle(DeleteProductVariationsCommand request, CancellationToken cancellationToken)
        {
            var product = unitOfWork.ProductRepository.GetById(request.Id);
            var productVariations = unitOfWork.ProductRepository.GetProductVariations(product);
            unitOfWork.ProductRepository.Delete(productVariations);

            var result = unitOfWork.SaveChanges();
            return Task.FromResult(result);
        }
    }
}
