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
    public record DeleteCategoryCommand(int Id) : IRequest<Result>
    {
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = unitOfWork.CategoryRepository.GetById(request.Id);

            if (category == null)
            {
                return Task.FromResult(Result.Failure($"Couldn't find category with id: {request.Id}"));
            }

            unitOfWork.CategoryRepository.Delete(category);

            if (unitOfWork.SaveChanges() == false)
            {
                return Task.FromResult(Result.Failure($"Couldn't save changes to category: {category.Name}"));
            }

            return Task.FromResult(Result.Success());
        }
    }
}
