using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.Logistics.Pages
{
    public class SuccesfullyRemovedStockModel : PageModel
    {
        private readonly IMediator mediator;

        public SuccesfullyRemovedStockModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty]
        public List<StoreProduct> StoreProducts { get; set; }

        [TempData]
        public string AllTextData { get; set; }
        [TempData]
        public int SuccesStoreId { get; set; }

        public List<int> AllTextDataList { get; set; }

        public IActionResult OnGet()
        {
            var AllTextDataArray = AllTextData.Split("\n");
            Array.Sort(AllTextDataArray);

            AllTextDataList = AllTextDataArray.Select(x => int.Parse(x)).ToList();
            StoreProducts = mediator.Send(new AllStoreProductsQuery(AllTextDataList, SuccesStoreId)).Result.Value;

            return Page();
        }
    }
}
