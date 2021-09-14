using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWinkelIdentity.Core.StoreEntities;
using MediatR;
using WebWinkelIdentity.Web.Application.Queries;
using WebWinkelIdentity.Web.Application.Commands;
using Microsoft.AspNetCore.Authorization;

namespace WebWinkelIdentity.Web.Areas.StoresManagement.Pages
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IMediator mediator;

        public EditModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty]
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

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = mediator.Send(new UpdateStoreCommand(Store)).Result;

            if (result.IsSuccess)
            {
                return LocalRedirect($"/StoresManagement/Details?id={Store.Id}");
            }

            return Page();
        }
    }
}
