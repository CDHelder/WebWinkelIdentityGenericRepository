using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.StoresManagement.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IList<Store> Store { get;set; }

        public void OnGet()
        {
            Store = mediator.Send(new AllStoresAndIncludesQuery()).Result.Value;
        }
    }
}
