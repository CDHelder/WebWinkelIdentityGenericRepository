using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Commands
{
    public record DeleteLoadStockChangeCommand(int Id) : IRequest<Result>
    {
    }

    public class DeleteLoadStockChangeCommandHandler : IRequestHandler<DeleteLoadStockChangeCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteLoadStockChangeCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(DeleteLoadStockChangeCommand request, CancellationToken cancellationToken)
        {
            var LSC = unitOfWork.LoadStockChangeRepository.Get
                (
                filter: lsc => lsc.Id == request.Id,
                include: lsc => lsc.Include(p => p.ProductStockChanges)
                );

            if (LSC == null)
                return Task.FromResult(Result.Failure($"Couldn't find LoadStockChange with id: {request.Id}"));

            unitOfWork.ProductStockChangeRepository.Delete(LSC.ProductStockChanges);
            unitOfWork.LoadStockChangeRepository.Delete(LSC);

            if (unitOfWork.SaveChanges() == false)
                return Task.FromResult(Result.Failure($"Couldn't delete LoadStockChange with id: {request.Id}"));

            return Task.FromResult(Result.Success());
        }
    }
}
