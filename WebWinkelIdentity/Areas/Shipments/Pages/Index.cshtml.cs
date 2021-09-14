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
    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public List<Shipment> Shipment { get;set; }
        public string FormResult { get; set; }

        public IActionResult OnGet()
        {
            var result = mediator.Send(new AllShipmentQuery(false)).Result;

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
