using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Queries
{
    public class AllStoresSelectListItemsQuery : IRequest<Result<List<SelectListItem>>>
    {
    }

    public class AllStoresSelectListItemsQueryHandler : IRequestHandler<AllStoresSelectListItemsQuery, Result<List<SelectListItem>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public AllStoresSelectListItemsQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result<List<SelectListItem>>> Handle(AllStoresSelectListItemsQuery request, CancellationToken cancellationToken)
        {
            var allStores = unitOfWork.StoreRepository.GetAll(
                include: store => store
                    .Include(s => s.Address))
                .Select(s =>
            new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Address.City
            }).ToList();

            return Task.FromResult(Result.Success(allStores));
        }
    }
}
