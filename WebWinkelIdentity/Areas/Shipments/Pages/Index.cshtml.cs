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

namespace WebWinkelIdentity.Web.Areas.Shipments.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IList<Shipment> Shipment { get;set; }

        public void OnGet()
        {
            //Shipment = 
        }
    }
}
