using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWinkelIdentity.Core.StoreEntities;

namespace WebWinkelIdentity.Web.Areas.Logistics.Pages
{
    public class MultipleRollbacksIndexModel : PageModel
    {
        private readonly IMediator mediator;

        public MultipleRollbacksIndexModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public List<List<ProductStockChange>> ProductStockChanges { get; set; }
        public void OnGet()
        {
            ProductStockChanges = mediator.Send(new AllProductStockChangesOrganizedQuery)
        }
    }
}
