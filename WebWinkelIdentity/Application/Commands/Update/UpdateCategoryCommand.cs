using CSharpFunctionalExtensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Commands
{
    public record UpdateCategoryCommand(Category Category) : IRequest<Result>
    {
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.Category == null)
                return Task.FromResult(Result.Failure("Can't update a object with no value"));

            unitOfWork.CategoryRepository.Update(request.Category);

            if (unitOfWork.SaveChanges() == false)
                return Task.FromResult(Result.Failure($"Couldn't save changes to category: {request.Category}"));

            return Task.FromResult(Result.Success());
        }
    }
}
