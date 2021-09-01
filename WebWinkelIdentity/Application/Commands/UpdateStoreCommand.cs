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
    public record UpdateStoreCommand(Store Store) : IRequest<Result>
    {
    }

    public class UpdateStoreCommandHandler : IRequestHandler<UpdateStoreCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateStoreCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
        {
            unitOfWork.StoreRepository.Update(request.Store);

            if (unitOfWork.SaveChanges() == false)
            {
                return Task.FromResult(Result.Failure("Couldn't save store in database"));
            }

            return Task.FromResult(Result.Success());
        }
    }
}
