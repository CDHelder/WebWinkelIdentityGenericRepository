using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWinkelIdentity.Core;
using WebWinkelIdentity.Web.Application.Commands;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.Brands.Pages
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly IMediator mediator;

        public DeleteModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty]
        public Brand Brand { get; set; }
        public string FormResult { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = mediator.Send(new GetBrandQuery(id)).Result;

            if (result.Value == null)
            {
                return NotFound();
            }

            Brand = result.Value;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = mediator.Send(new DeleteBrandCommand(id)).Result;

            if (result.IsFailure)
            {
                FormResult = result.Error;
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
