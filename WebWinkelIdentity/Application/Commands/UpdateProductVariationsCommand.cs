using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Commands
{
    public record UpdateProductVariationsCommand(Product Product, List<Product> Products) : IRequest<bool>
    {
    }

    public class UpdateProductVariationsCommandHandler : IRequestHandler<UpdateProductVariationsCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateProductVariationsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(UpdateProductVariationsCommand request, CancellationToken cancellationToken)
        {
            unitOfWork.ProductRepository.UpdateProductProperties(request.Product, request.Products);

            var result = unitOfWork.SaveChanges();
            return Task.FromResult(result);
        }
    }
}
