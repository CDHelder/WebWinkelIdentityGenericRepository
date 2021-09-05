using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWinkelIdentity.Core.StoreEntities;

namespace WebWinkelIdentity.Web.Areas.Shipments.Pages
{
    public class ConfirmDeliveryShipmentModel : PageModel
    {
        private readonly IMediator mediator;

        public ConfirmDeliveryShipmentModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public List<Shipment> Shipment { get; set; }
        public string FormResult { get; set; }

        public IActionResult OnGet(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //TODO: Maak AllShipmentsQuery met List<int> = null zodat ie een lijst met == ids kan meegeven
            //var result = mediator.Send(new ShipmentQuery(id)).Result;

            //if (result.IsFailure)
            //{
            //    FormResult = result.Error;
            //    return Page();
            //}

            //Shipment = result.Value;

            return Page();
        }

        public IActionResult OnPost()
        {
            //TODO: Maak Mediator voor het implementeren van alle Shipments zijn bezorgd
            return RedirectToPage("./HistoryIndex");
        }
    }
}
