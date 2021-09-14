using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Commands
{
    public record DeleteBrandCommand(int Id) : IRequest<Result>
    {
    }

    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteBrandCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = unitOfWork.BrandRepository.GetById(request.Id);

            if (brand == null)
            {
                return Task.FromResult(Result.Failure($"Couldn't find brand with id: {request.Id}"));
            }

            unitOfWork.BrandRepository.Delete(brand);

            if (unitOfWork.SaveChanges() == false)
            {
                return Task.FromResult(Result.Failure($"Couldn't delete brand: {brand.Name}"));
            }

            return Task.FromResult(Result.Success());
        }
    }
}
