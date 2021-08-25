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
        //TODO: Check of deze goed werkt
        public Task<Result<List<List<ProductStockChange>>>> Handle(AllProductStockChangesOrganizedQuery request, CancellationToken cancellationToken)
        {
            ////TODO: Verander
            //var allPSC = unitOfWork.ProductStockChangeRepository.GetAllProductStockChangesAndIncludes().OrderByDescending(psc => psc.DateChanged).ToList();

            //var lastPSCId = allPSC.Last().Id;
            //string dateTime = allPSC.First().DateChanged.ToString();
            //var listListPSC = new List<List<ProductStockChange>>();
            //var listPSC = new List<ProductStockChange>();
            //foreach (var PSC in allPSC)
            //{
            //    if (PSC.DateChanged.ToString() == dateTime || listPSC.Count() == 0)
            //    {
            //        listPSC.Add(PSC);
            //    }
            //    else
            //    {
            //        dateTime = PSC.DateChanged.ToString();
            //        listListPSC.Add(listPSC);
            //        if (PSC.Id == lastPSCId)
            //        {
            //            listPSC = new();
            //            listPSC.Add(PSC);
            //            listListPSC.Add(listPSC);
            //        }
            //        else
            //        {
            //            listPSC = new();
            //            listPSC.Add(PSC);
            //        }
            //    }
            //}

            //var count = listListPSC.SelectMany(psc => psc).Count();
            //if (count != allPSC.Count())
            //{
            //    return Task.FromResult(Result.Failure<List<List<ProductStockChange>>>("Couldnt find all ProductStockChanges"));
            //}

            //return Task.FromResult(Result.Success(listListPSC));
            List<List<ProductStockChange>> listListPSC = new();
            return Task.FromResult(Result.Success(listListPSC));

        }
    }
}
