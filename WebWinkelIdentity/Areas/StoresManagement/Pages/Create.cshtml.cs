using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data;
using WebWinkelIdentity.Web.Application.Commands;

namespace WebWinkelIdentity.Web.Areas.StoresManagement.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IMediator mediator;

        public CreateModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Store Store { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = mediator.Send(new CreateStoreCommand(Store)).Result;

            if (result.IsSuccess)
            {
                return RedirectToPage("./Index");

            }

            return Page();
        }
    }
}
