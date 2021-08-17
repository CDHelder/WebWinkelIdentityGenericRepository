using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;
using WebWinkelIdentity.Web.Application.Commands;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.Logistics.Pages
{
    public class ConfirmRemoveStockModel : PageModel
    {
        private readonly IMediator mediator;

        public ConfirmRemoveStockModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty]
        public List<StoreProduct> StoreProducts { get; set; }
        [BindProperty]
        public List<int> AllTextDataList { get; set; }
        [BindProperty]
        public int PostStoreId { get; set; }

        [TempData]
        public int StoreId { get; set; }
        [TempData]
        public int SuccesStoreId { get; set; }
        [TempData]
        public string FormResult { get; set; }
        [TempData]
        public string AllTextData { get; set; }

        public IActionResult OnGet()
        {
            var AllTextDataArray = AllTextData.Split("\n");
            Array.Sort(AllTextDataArray);

            AllTextDataList = AllTextDataArray.Select(x => int.Parse(x)).ToList();
            StoreProducts = mediator.Send(new AllStoreProductsQuery(AllTextDataList, StoreId)).Result.Value;
            PostStoreId = StoreId;

            return Page();
        }

        public IActionResult OnPost()
        {
            //TODO: Check if mediator works correctly
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier.ToString());
            var result = mediator.Send(new UpdateAllStocksAndCreateAllProductStockChangesCommand(StoreProducts, AllTextDataList, userId, false));

            if (result.Result.IsFailure)
            {
                FormResult = result.Result.Error;
            }

            AllTextData = string.Join("\n", AllTextDataList);
            SuccesStoreId = PostStoreId;
            return RedirectToPage("/SuccesfullyRemovedStock");
        }
    }
}
