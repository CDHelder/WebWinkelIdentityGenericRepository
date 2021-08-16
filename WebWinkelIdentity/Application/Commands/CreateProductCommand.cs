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
    public record CreateProductCommand(Product Product) : IRequest<bool>
    {

    }

    public class ProductCreateCommandHandler : IRequestHandler<CreateProductCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductCreateCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            unitOfWork.ProductRepository.Create(request.Product);

            var result = unitOfWork.SaveChanges();
            return Task.FromResult(result);
        }
    }
}
