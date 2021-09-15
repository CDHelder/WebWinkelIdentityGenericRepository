using CSharpFunctionalExtensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Commands
{
    public record CreateBrandCommand(Brand Brand) : IRequest<Result>
    {
    }

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateBrandCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            if (request.Brand == null)
                return Task.FromResult(Result.Failure("Please enter a brand to save"));

            unitOfWork.BrandRepository.Create(request.Brand);

            if (unitOfWork.SaveChanges() == false)
                return Task.FromResult(Result.Failure($"Couldn't save changes for {request.Brand.Name}"));

            return Task.FromResult(Result.Success());
        }
    }
}
