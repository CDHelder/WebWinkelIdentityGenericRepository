using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.Shipments.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IMediator mediator;

        public DetailsModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Shipment Shipment { get; set; }
        public string FormResult { get; set; }

        //TODO: Doe frontend van deze page (tip: gebruik LoadStockChange rontend) cuz models bijna hetzelfde
        public IActionResult OnGet(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = mediator.Send(new ShipmentQuery(id)).Result;

            if (result.IsFailure)
            {
                FormResult = result.Error;
                return Page();
            }

            Shipment = result.Value;

            return Page();
        }
    }
}
