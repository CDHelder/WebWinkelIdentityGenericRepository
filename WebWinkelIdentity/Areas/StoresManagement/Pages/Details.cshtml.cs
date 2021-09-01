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

namespace WebWinkelIdentity.Web.Areas.StoresManagement.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IMediator mediator;

        public DetailsModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Store Store { get; set; }

        public IActionResult OnGet(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Store = mediator.Send(new StoreQuery(id)).Result.Value;

            if (Store == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
