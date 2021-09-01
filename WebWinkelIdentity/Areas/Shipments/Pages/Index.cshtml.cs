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
