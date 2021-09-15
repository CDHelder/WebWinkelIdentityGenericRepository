using CSharpFunctionalExtensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Commands
{
    public record DeleteStoreCommand(int Id) : IRequest<Result>
    {
    }

    public class DeleteStoreCommandHandler : IRequestHandler<DeleteStoreCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteStoreCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
        {
            var store = unitOfWork.StoreRepository.GetById(request.Id);

            if (store == null)
                return Task.FromResult(Result.Failure($"Couldn't find store with id: {request.Id}"));

            unitOfWork.StoreRepository.Delete(store);

            if (unitOfWork.SaveChanges() == false)
                return Task.FromResult(Result.Failure($"Couldn't delete store with id: {request.Id}"));

            return Task.FromResult(Result.Success());
        }
    }
}
