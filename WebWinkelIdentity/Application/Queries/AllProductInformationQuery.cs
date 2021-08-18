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
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Application.Queries
{
    public record AllProductInformationQuery(int Id) : IRequest<Result<AllProductInformation>>
    {

    }

    public class AllProductInformationQueryHandler : IRequestHandler<AllProductInformationQuery, Result<AllProductInformation>>
    {
        private readonly IUnitOfWork unitOfWork;

        public AllProductInformationQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<AllProductInformation>> Handle(AllProductInformationQuery request, CancellationToken cancellationToken)
        {
            var product = unitOfWork.ProductRepository.GetById(request.Id);
            var productVariations = unitOfWork.ProductRepository.GetProductVariations(product);
            var productStocks = unitOfWork.StoreProductRepository.GetAllStoreProducts(productVariations);

            var AllInfo = new AllProductInformation
            {
                Product = product,
                ProductVariations = productVariations,
                ProductStocks = productStocks,
                Stores = unitOfWork.StoreRepository.GetAll(include: store => store.Include(s => s.Address))
            };

            return Task.FromResult(Result.Success(AllInfo));
        }
    }

    public record AllProductInformation(Product Product = null, List<Product> ProductVariations = null, List<StoreProduct> ProductStocks = null, List<Store> Stores = null)
    {

    }
}
