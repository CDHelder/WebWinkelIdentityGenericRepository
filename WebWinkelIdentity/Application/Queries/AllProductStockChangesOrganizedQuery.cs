using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Application.Queries
{
    public class AllProductStockChangesOrganizedQuery : IRequest<Result<List<List<ProductStockChange>>>>
    {
    }

    public class AllProductStockChangesOrganizedQueryHandler : IRequestHandler<AllProductStockChangesOrganizedQuery, Result<List<List<ProductStockChange>>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public AllProductStockChangesOrganizedQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //Create List of ProductStockChange where each PSC has identical DateTime value all combined in a List
        //Pak alle, Sort hem via DateTime Property, foreach, als datetime hetzelfde is voeg toe aan List, zo niet voeg hele List toe aan de main List??
        public Task<Result<List<List<ProductStockChange>>>> Handle(AllProductStockChangesOrganizedQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
