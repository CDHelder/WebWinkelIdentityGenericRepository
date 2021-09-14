using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.Shipments.Pages
{
    [Authorize(Roles = "Admin,Employee")]
    public class DeliveryShipmentModel : PageModel
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

            var result = mediator.Send(new ShipmentsExcistQuery(list));

            if (result.Result.IsFailure)
            {
                FormResult = result.Result.Error;
                return Page();
            }

            AllTextData = string.Join(",", list);

            return LocalRedirect($"/Shipments/ConfirmDeliveryShipment?ids={AllTextData}");

        }
    }
}
