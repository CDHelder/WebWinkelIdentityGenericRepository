using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.Logistics.Pages
{
    [Authorize(Roles = "Admin,Employee")]
    public class RemoveStockModel : PageModel
    {
        private readonly IMediator mediator;

        public RemoveStockModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty]
        public string AllText { get; set; }
        [BindProperty]
        public string SelectedStoreId { get; set; }
        public List<SelectListItem> AllStores { get; set; }

        [TempData]
        public int StoreId { get; set; }
        [TempData]
        public string FormResult { get; set; }
        [TempData]
        public string AllTextData { get; set; }

        public IActionResult OnGet()
        {
            AllText = null;
            AllStores = mediator.Send(new AllStoresSelectListItemsQuery()).Result.Value;

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (AllText == null)
            {
                FormResult = "Please enter a product id";
                return Page();
            }
            if (SelectedStoreId == null)
            {
                FormResult = "Please select a store";
                return Page();
            }

            AllText = AllText.Replace("\r", "");
            var list = AllText.Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToArray();

            var result = mediator.Send(new BoolProductsAndStoreExcistValidationQuery(list, SelectedStoreId));

            if (result.Result.IsFailure)
            {
                FormResult = result.Result.Error;
                return Page();
            }

            var storeId = int.Parse(SelectedStoreId);
            var productStockChangeResult = await mediator.Send(new BoolProductStockChangeExcistsQuery(list, storeId));

            if(productStockChangeResult.IsFailure)
            {
                FormResult = productStockChangeResult.Error;
                return Page();
            }

            AllTextData = AllText;
            StoreId = int.Parse(SelectedStoreId);

            return RedirectToPage("/ConfirmRemoveStock");

        }
    }
}
