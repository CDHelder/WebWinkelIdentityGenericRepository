using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.Logistics.Pages
{
    [Authorize(Roles = "Admin,Employee")]
    public class AllStockChangesModel : PageModel
    {
        private readonly IMediator mediator;

        public AllStockChangesModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public List<LoadStockChange> LoadStockChanges { get;set; }

        public void OnGetAsync()
        {
            LoadStockChanges = mediator.Send(new AllLoadStockChangesStockQuery()).Result.Value;
        }
    }
}
