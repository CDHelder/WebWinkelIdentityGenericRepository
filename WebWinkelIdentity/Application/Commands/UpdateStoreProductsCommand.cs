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
    public record UpdateStoreProductsCommand(List<StoreProduct> StoreProducts) : IRequest<bool>
    {
    }

    public class UpdateStoreProductsCommandHandler : IRequestHandler<UpdateStoreProductsCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateStoreProductsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(UpdateStoreProductsCommand request, CancellationToken cancellationToken)
        {
            unitOfWork.StoreProductRepository.Update(request.StoreProducts);

            var result = unitOfWork.SaveChanges();
            return Task.FromResult(result);
        }
    }
}
