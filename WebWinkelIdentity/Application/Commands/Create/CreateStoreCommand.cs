using CSharpFunctionalExtensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Commands
{
    public record CreateStoreCommand(Store Store) : IRequest<Result>
    {
    }

    public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateStoreCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            unitOfWork.StoreRepository.Create(request.Store);

            if (unitOfWork.SaveChanges() == false)
                return Task.FromResult(Result.Failure("Couldn't create store"));

            return Task.FromResult(Result.Success());
        }
    }
}
