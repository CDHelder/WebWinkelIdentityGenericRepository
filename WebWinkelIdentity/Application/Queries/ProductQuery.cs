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
    public record ProductQuery(int Id) : IRequest<Result<Product>>
    {

    }

    public class ProductQueryHandler : IRequestHandler<ProductQuery, Result<Product>>
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<Result<Product>> Handle(ProductQuery request, CancellationToken cancellationToken)
        {
            //var product = unitOfWork.ProductRepository.GetById(request.Id);

            var product = unitOfWork.ProductRepository.Get(
            filter: p => p.Id == request.Id,
            include: product => product
                .Include(p => p.Brand)
                .Include(p => p.Category));

           

            return Task.FromResult(Result.Success(product));
        }
    }
}
