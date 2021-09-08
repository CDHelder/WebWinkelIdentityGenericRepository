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
    public class ConfirmDeliveryShipmentModel : PageModel
    {
        private readonly IMediator mediator;

        public ConfirmDeliveryShipmentModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public List<Shipment> Shipments { get; set; }

        [TempData]
        public string FormResult { get; set; }
        [BindProperty]
        public string Ids { get; set; }

        public IActionResult OnGet(string ids)
        {
            if (ids == null)
            {
                return NotFound();
            }

            var intIds = ids.Split(", ").Select(Int32.Parse).ToList();

            var result = mediator.Send(new AllShipmentQuery(false, intIds)).Result;

            if (result.IsFailure)
            {
                FormResult = result.Error;
                return Page();
            }

            Shipments = result.Value;
            Ids = ids;

            return Page();
        }

        public IActionResult OnPost(string ids)
        {
            //TODO: Maak Mediator voor het implementeren van alle Shipments zijn bezorgd

            var intIds = ids.Split(", ").Select(Int32.Parse).ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier.ToString());

            var result = mediator.Send(new ShipmentsDeliveryCommand(intIds, userId)).Result;

            if (result.IsFailure)
            {
                FormResult = result.Error;
                return Page();
            }

            return RedirectToPage("./HistoryIndex");
        }
    }
}
