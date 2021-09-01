using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Web.Application.Commands;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.Shipments.Pages
{
    public class ConfirmCreateModel : PageModel
    {
        private readonly IMediator mediator;

        public ConfirmCreateModel(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [TempData]
        public string AllTextData { get; set; }
        [TempData]
        public int SelectedStartLocationStoreId { get; set; }
        [TempData]
        public int SelectedDeliveryLocationStoreId { get; set; }

        [BindProperty]
        public List<StoreProduct> StoreProducts { get; set; }
        [BindProperty]
        public List<int> AllTextDataList { get; set; }
        [BindProperty]
        public string FormResult { get; set; }

        public IActionResult OnGet()
        {
            var AllTextDataArray = AllTextData.Split("\n");
            Array.Sort(AllTextDataArray);
            AllTextDataList = AllTextDataArray.Select(x => int.Parse(x)).ToList();

            StoreProducts = mediator.Send(new AllStoreProductsQuery(AllTextDataList, SelectedStartLocationStoreId)).Result.Value;

            return Page();
        }

        public IActionResult OnPost()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier.ToString());
            var result = mediator.Send(new UpdateAllStocksAndCreateAllProductStockChangesCommand(StoreProducts, AllTextDataList, userId, false, true, SelectedDeliveryLocationStoreId));

            if (result.Result.IsFailure)
            {
                FormResult = result.Result.Error;
            }

            return LocalRedirect("/Shipments/Details?={}");
        }
    }
}
