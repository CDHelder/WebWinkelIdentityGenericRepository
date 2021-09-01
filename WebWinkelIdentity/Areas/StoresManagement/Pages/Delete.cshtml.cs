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
using WebWinkelIdentity.Web.Application.Commands;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.StoresManagement.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator mediator;

        public DeleteModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty]
        public Store Store { get; set; }
        [BindProperty]
        public List<Product> Products { get; set; }

        public IActionResult OnGet(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Store = mediator.Send(new StoreQuery(id)).Result.Value;
            Products = mediator.Send(new StoreUniqueProductListQuery(id)).Result.Value;

            if (Store == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = mediator.Send(new DeleteStoreCommand(id)).Result;

            if (result.IsSuccess)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
