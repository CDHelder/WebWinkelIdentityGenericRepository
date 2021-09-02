using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebWinkelIdentity.Web.Areas.Shipments.Pages
{
    public class DeliveryShipmentModel : PageModel
    {
    {
        private readonly IMediator mediator;

        public DeliveryShipmentModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty]
        public string AllText { get; set; }

        [TempData]
        public string FormResult { get; set; }
        [TempData]
        public string AllTextData { get; set; }

        public IActionResult OnGet()
        {
            AllText = null;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (AllText == null)
            {
                FormResult = "Please enter a product id";
                return Page();
            }

            AllText = AllText.Replace("\r", "");
            var list = AllText.Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToArray();

            //TODO: Shipments excists (Multiple)
            //var result = mediator.Send(new BoolProductsAndStoreExcistValidationQuery(list, SelectedStoreId));

            if (result.Result.IsFailure)
            {
                FormResult = result.Result.Error;
            }

            AllTextData = string.Join("\n", list);

            return RedirectToPage("/ConfirmDeliveryShipment");

        }
    }
}
