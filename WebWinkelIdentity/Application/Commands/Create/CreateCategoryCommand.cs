using CSharpFunctionalExtensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Commands
{
    public record CreateCategoryCommand(Category Category) : IRequest<Result>
    {
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.Category == null)
                return Task.FromResult(Result.Failure("Please enter a brand to save"));

            unitOfWork.CategoryRepository.Create(request.Category);

            if (unitOfWork.SaveChanges() == false)
                return Task.FromResult(Result.Failure($"Couldn't save changes for {request.Category.Name}"));

            return Task.FromResult(Result.Success());
        }
    }
}
