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
        public string StartLocationCity { get; set; }
        [BindProperty]
        public string DeliveryLocationCity { get; set; }
        [BindProperty]
        public int DeliveryStoreId { get; set; }
        [BindProperty]
        public string FormResult { get; set; }

        [BindProperty]
        public bool IsShipmentPossible { get; set; }

        public IActionResult OnGet()
        {
            var AllTextDataArray = AllTextData.Split("\n");
            Array.Sort(AllTextDataArray);
            AllTextDataList = AllTextDataArray.Select(x => int.Parse(x)).ToList();

            StoreProducts = mediator.Send(new AllStoreProductsQuery(AllTextDataList, SelectedStartLocationStoreId)).Result.Value;
            StartLocationCity = mediator.Send(new StoreQuery(SelectedStartLocationStoreId)).Result.Value.Address.City;
            DeliveryLocationCity = mediator.Send(new StoreQuery(SelectedDeliveryLocationStoreId)).Result.Value.Address.City;

            DeliveryStoreId = SelectedDeliveryLocationStoreId;

            return Page();
        }

        public IActionResult OnPost()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier.ToString());
            var result = mediator.Send(new UpdateAllStocksAndCreateAllProductStockChangesCommand(StoreProducts, AllTextDataList, userId, false, true, DeliveryStoreId)).Result;

            if (result.IsFailure)
            {
                FormResult = result.Error;
            }

            return LocalRedirect($"/Shipments/Details?id={result.Value}");
        }
    }
}
