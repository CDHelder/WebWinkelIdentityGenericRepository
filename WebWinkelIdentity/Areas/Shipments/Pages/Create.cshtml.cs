using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.Shipments.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IMediator mediator;

        public CreateModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IActionResult OnGet()
        {
            AllStores = mediator.Send(new AllStoresSelectListItemsQuery()).Result.Value;

            return Page();
        }

        [TempData]
        public string FormResult { get; set; }

        [BindProperty]
        public string AllText { get; set; }
        [BindProperty]
        public List<SelectListItem> AllStores { get; set; }
        [BindProperty]
        public int StartLocationStoreId { get; set; }
        [BindProperty]
        public int DeliveryLocationStoreId { get; set; }

        [TempData]
        public string AllTextData { get; set; }
        [TempData]
        public int SelectedStartLocationStoreId { get; set; }
        [TempData]
        public int SelectedDeliveryLocationStoreId { get; set; }

        //TODO: Check of werkt
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else if(StartLocationStoreId == DeliveryLocationStoreId)
            {
                FormResult = "Please make sure the start location and delivery location are not the same";
                return Page();
            }

            AllText = AllText.Replace("\r", "");
            var list = AllText.Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToArray();

            var result = mediator.Send(new BoolProductsAndStoreExcistValidationQuery(list, StartLocationStoreId.ToString()));

            if (result.Result.IsFailure)
            {
                FormResult = result.Result.Error;
                return Page();
            }

            var productStockChangeResult = mediator.Send(new BoolProductStockChangeExcistsQuery(list, StartLocationStoreId)).Result;

            if (productStockChangeResult.IsFailure)
            {
                FormResult = productStockChangeResult.Error;
                return Page();
            }

            AllTextData = string.Join("\n", list);
            SelectedStartLocationStoreId = StartLocationStoreId;
            SelectedDeliveryLocationStoreId = DeliveryLocationStoreId;

            return RedirectToPage("./ConfirmCreate");
        }
    }
}
