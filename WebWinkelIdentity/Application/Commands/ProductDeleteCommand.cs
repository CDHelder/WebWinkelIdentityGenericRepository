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
    public record ProductDeleteCommand(int Id) : IRequest<bool>
    {

    }
    public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductDeleteCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<bool> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
        {
            var product = unitOfWork.ProductRepository.GetById(request.Id);
            unitOfWork.ProductRepository.Delete(product);

            var result = unitOfWork.SaveChanges();
            return Task.FromResult(result);
        }
    }
}
