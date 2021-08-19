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
                    productVariations.Add(new Product 
                    { 
                        BrandId = request.Product.BrandId,
                        CategoryId = request.Product.CategoryId,
                        Color = request.Product.Color,
                        Description = request.Product.Description,
                        Fabric = request.Product.Fabric,
                        Name = request.Product.Name,
                        Price = request.Product.Price,
                        Size = size
                    });
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
                //Pak het id van de eerste product dat is aangemaakt uit database
                var firstProductId = unitOfWork.ProductRepository.GetProductVariations(request.Product).First().Id;
                return Task.FromResult(Result.Success(firstProductId));
            }

            var errorMessage = $"Product {request.Product.Name} with id: {request.Product.Id} couldn't be created and saved in the database";
            return Task.FromResult(Result.Failure<int>(errorMessage));
        }
    }
}
