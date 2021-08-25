using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Queries
{
    public record ProductStockChangeAndCurrentStoreProductQuery(int PSCId) : IRequest<Result<ProductStockChange>>
    {
    }

    public class ProductStockChangeAndCurrentStoreProductQueryHandler : IRequestHandler<ProductStockChangeAndCurrentStoreProductQuery, Result<ProductStockChange>>
    {
        private readonly IUnitOfWork unitOfWork;
        public ProductStockChangeAndCurrentStoreProductQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<ProductStockChange>> Handle(ProductStockChangeAndCurrentStoreProductQuery request, CancellationToken cancellationToken)
        {
            var PSC = unitOfWork.ProductStockChangeRepository.Get(
                filter: psc => psc.Id == request.PSCId,
                include: psc => psc.Include(p => p.StoreProduct)
                .ThenInclude(sp => sp.Product)
                .ThenInclude(p => p.Brand)
                .Include(p => p.StoreProduct)
                .ThenInclude(sp => sp.Product)
                .ThenInclude(p => p.Category)
                .Include(p => p.StoreProduct)
                .ThenInclude(sp => sp.Store)
                .ThenInclude(p => p.Address)
                //TODO: Verander
                //.Include(p => p.AssociatedUser)
                );

            return Task.FromResult(Result.Success(PSC));
        }
    }
}
