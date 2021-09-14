using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.Shipments.Pages
{
    [Authorize(Roles = "Admin,Employee")]
    public class HistoryIndexModel : PageModel
    {
        private readonly IMediator mediator;

        public HistoryIndexModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public List<Shipment> Shipment { get; set; }
        public string FormResult { get; set; }

        public IActionResult OnGet()
        {
            var result = mediator.Send(new AllShipmentQuery(true)).Result;

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
