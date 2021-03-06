using System.Collections.Generic;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.Logistics.Pages
{
    [Authorize(Roles = "Admin,Employee")]
    public class AddStockModel : PageModel
    {
        private readonly IMediator mediator;

        public AddStockModel(IMediator mediator)
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

        public IActionResult OnPost()
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
            }

            AllTextData = string.Join("\n", list);
            StoreId = int.Parse(SelectedStoreId);

            return RedirectToPage("/ConfirmAddStock");

        }
    }
}
