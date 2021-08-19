using CSharpFunctionalExtensions;
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
    public record CreateProductCommand(Product Product) : IRequest<Result<int>>
    {

    }

    public class ProductCreateCommandHandler : IRequestHandler<CreateProductCommand, Result<int>>
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductCreateCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productsSizes = request.Product.Size.Trim().Split(",");
            if (productsSizes.Count() > 1)
            {
                var productVariations = new List<Product>();
                foreach (var size in productsSizes)
                {
                    var newProduct = new Product
                    {
                        BrandId = request.Product.BrandId,
                        CategoryId = request.Product.CategoryId,
                        Color = request.Product.Color,
                        Description = request.Product.Description,
                        Fabric = request.Product.Fabric,
                        Name = request.Product.Name,
                        Price = request.Product.Price,
                        Size = size
                    };
                    productVariations.Add(newProduct);
                }
                unitOfWork.ProductRepository.Create(productVariations);
            }
            else
            {
                unitOfWork.ProductRepository.Create(request.Product);
            }

            var result = unitOfWork.SaveChanges();
            if (result == true)
            {
                var allProductVariations = unitOfWork.ProductRepository.GetProductVariations(request.Product);
                var allStores = unitOfWork.StoreRepository.GetAll();

                var storeProductVariations = new List<StoreProduct>();
                foreach (var variation in allProductVariations)
                {
                    foreach (var store in allStores)
                    {
                        var newStoreProduct = new StoreProduct
                        {
                            InTransport = false,
                            ProductId = variation.Id,
                            Quantity = 0,
                            StoreId = store.Id
                        };
                        storeProductVariations.Add(newStoreProduct);
                    }
                }
                unitOfWork.StoreProductRepository.Create(storeProductVariations);
                var SPResult = unitOfWork.SaveChanges();
                if (SPResult == true)
                {
                    var firstProductId = allProductVariations.First().Id;
                    return Task.FromResult(Result.Success(firstProductId));
                }
            }

            var errorMessage = $"Product {request.Product.Name} with id: {request.Product.Id} couldn't be created and saved in the database";
            return Task.FromResult(Result.Failure<int>(errorMessage));
        }
    }
}
